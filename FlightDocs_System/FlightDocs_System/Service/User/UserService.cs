using FlightDocs_System.Data;
using FlightDocs_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocs_System.Service.User
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _config;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<ResponseModel> Login(UserLogin user)
        {
            if (user == null)
            {
                return new ResponseModel
                {
                    Message = "Login user is null",
                    IsSuccess = false
                };
            }
            if (!user.Email.Contains("@vietjetair.com"))
            {
                return new ResponseModel
                {
                    Message = "Your email is not correct",
                    IsSuccess = false
                };
            }
            var userLogin = await _userManager.FindByEmailAsync(user.Email);
            if (userLogin != null && await _userManager.CheckPasswordAsync(userLogin, user.Password))
            {
                var authClaim = new List<Claim>
                {
                   new Claim("Email",user.Email),
                   new Claim(ClaimTypes.NameIdentifier,userLogin.Id),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
                var userRole = await _userManager.GetRolesAsync(userLogin);
                foreach (var role in userRole)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, role));
                    var roles = await _roleManager.FindByNameAsync(role);
                    var Rolepermissions = await _context.rolePermissions.Where(x => x.RoleId == roles.Id).ToListAsync();
                    foreach (var permission in Rolepermissions)
                    {
                        var p = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == permission.PermissionId);
                        authClaim.Add(new Claim("Permission", p.PermissionName));
                    }
                }

                var token = CreateToken(authClaim);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Message = new JwtSecurityTokenHandler().WriteToken(token),
                };
            }
            return new ResponseModel
            {
                Message = "Unauthorized",
                IsSuccess = false
            };
        }

        private SecurityToken CreateToken(List<Claim> authClaim)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                    issuer: _config["AuthSettings:Issuer"],
                    audience: _config["AuthSettings:Audience"],
                    claims: authClaim,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public async Task<ResponseModel> Register(string role, UserRegister user)
        {
            if (user == null)
            {
                throw new NullReferenceException("Register model is null");
            }
            if (!user.Email.Contains("@vietjetair.com"))
            {
                return new ResponseModel 
                { 
                    Message = "Email must contain @vietjetair.com", 
                    IsSuccess = false 
                };
            }
            if (user.Password != user.ConfirmPassword)
            {
                return new ResponseModel
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            }
            var identityUser = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email,
            };
            var checkRole = await _roleManager.RoleExistsAsync(role);
            if (checkRole)
            {
                var result = await _userManager.CreateAsync(identityUser, user.Password);

                if (!result.Succeeded)
                {
                    return new ResponseModel
                    {
                        Message = "User did not create",
                        IsSuccess = false,
                        Errors = result.Errors.Select(e => e.Description)
                    };
                }
                // add to role
                await _userManager.AddToRoleAsync(identityUser, role);

                return new ResponseModel
                {
                    Message = $"User created successFully",
                    IsSuccess = true,

                };

            }
            else
            {
                return new ResponseModel
                {
                    Message = "Role doesn't exitst",
                    IsSuccess = false,
                };
            }
        }

        public async Task<ResponseModel> GetAll()
        {
            var data = await _userManager.Users.ToListAsync();
            if (data == null)
            {
                return new ResponseModel
                {
                    Message = "User is empty",
                    IsSuccess = true
                };
            }
            List<UserDTO> users= new List<UserDTO>();
            foreach(var user in data)
            {
                var role = await _userManager.GetRolesAsync(user);
                var roleName = role.FirstOrDefault();

                UserDTO u = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role= roleName
                };
                users.Add(u);
            }
            return new ResponseModel
            {
                Message = "Get all users is successful",
                IsSuccess = true,
                Data = users,
            };
        }

        public async Task<ResponseModel> GetById(string id)
        {
            var user= await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return new ResponseModel
                {
                    Message=$"Not exist user with id={id}",
                    IsSuccess=false
                };
            }
            var role = await _userManager.GetRolesAsync(user);
            var roleName = role.FirstOrDefault();
            UserDTO u = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = roleName
            };
            return new ResponseModel
            {
                Message = "Find user is successful",
                IsSuccess = true,
                Data = u
            };
        }
        public async Task<UserDTO> GetUserByToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var decodedToken = DecodeJwtToken(token, key);

            var userId = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var email = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;
            var role = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roleName = await _userManager.GetRolesAsync(user);
                var firstRole = roleName.FirstOrDefault();

                return new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role= firstRole,
                };
            }
            return null;
        }
        private JwtSecurityToken DecodeJwtToken(string accessToken, SymmetricSecurityKey key)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,

                ValidateIssuer = true,
                ValidIssuer = _config["AuthSettings:Issuer"],

                ValidateAudience = true,
                ValidAudience = _config["AuthSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            return securityToken as JwtSecurityToken;
        }

        
    }
}
