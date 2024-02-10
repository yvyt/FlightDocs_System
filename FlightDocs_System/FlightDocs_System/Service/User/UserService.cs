using FlightDocs_System.Data;
using FlightDocs_System.Model;
using Microsoft.AspNetCore.Identity;

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
                    Message="Login user is null",
                    IsSuccess=false
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

            }
        }
    }
}
