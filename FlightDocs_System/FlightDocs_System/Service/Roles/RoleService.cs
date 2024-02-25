using FlightDocs_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs_System.Service.Roles
{
    public class RoleService : IRoleService
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ResponseModel> AddRole(RoleCreate role)
        {
            var checkRole = await _roleManager.RoleExistsAsync(role.Name);
            if (!checkRole)
            {
                var identityRole = new IdentityRole
                {
                    Name = role.Name
                };
                var result = await _roleManager.CreateAsync(identityRole);
                if (!result.Succeeded)
                {
                    return new ResponseModel
                    {
                        Message = "Role did not create",
                        IsSuccess = false,
                        Errors = result.Errors.Select(e => e.Description)
                    };
                }
                return new ResponseModel
                {
                    Message = "Create new role successful",
                    IsSuccess = true,
                    Data = identityRole
                };
            }
            return new ResponseModel
            {
                Message = "Role name has already exist",
                IsSuccess = false,
            };
        }

        public async Task<ResponseModel> EditRole(string id, RoleCreate role)
        {
            var r= await _roleManager.FindByIdAsync(id);
            if (r == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist role with id={id}",
                    IsSuccess = false,
                };
            }
            r.Name = role.Name; 
            var update=await _roleManager.UpdateAsync(r);
            if(!update.Succeeded)
            {
                return new ResponseModel
                {
                    Message = "Role did not update",
                    IsSuccess = false,
                    Errors = update.Errors.Select(e => e.Description)
                };
            }
            return new ResponseModel
            {
                Message = "Update successful",
                IsSuccess = true,
                Data = r
            };
        }

        public async Task<ResponseModel> GetAll()
        {
            var result = await _roleManager.Roles.ToListAsync();
            List<RoleDTO> roles = new List<RoleDTO>();
            foreach (var role in result)
            {
                var roleDTO = new RoleDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                var userCountWithRole = await _userManager.GetUsersInRoleAsync(role.Name);
                roleDTO.numberUser=userCountWithRole.Count;
                roles.Add(roleDTO);
            }
            return new ResponseModel
            {
                Message = "Get all role successfule",
                IsSuccess = true,
                Data= roles
            };
        }

        public async Task<ResponseModel> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist role with id={id}",
                    IsSuccess = false,
                };
            }
            var userWithRole = await _userManager.GetUsersInRoleAsync(role.Name);
            RoleDTO r = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
            };
            if (userWithRole.Count > 0)
            {
                List<UserDTO> users = new List<UserDTO>();
                foreach(var user in userWithRole)
                {
                    UserDTO u = new UserDTO
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Role = role.Name
                    };
                    users.Add(u);
                }
                r.numberUser=users.Count;
                r.Data=users;
            }
            else
            {
                r.numberUser = 0;
            }
            return new ResponseModel
            {
                Message = "Get detail role successful",
                IsSuccess = true,
                Data = r
            };
        }
    }
}
