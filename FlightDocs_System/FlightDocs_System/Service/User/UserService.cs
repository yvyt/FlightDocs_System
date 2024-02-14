using FlightDocs_System.Data;
using FlightDocs_System.Model;
using Microsoft.AspNetCore.Identity;
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
    }
}
