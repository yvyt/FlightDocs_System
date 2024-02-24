using FlightDocs_System.Data;
using FlightDocs_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs_System.Service.RolePermissions
{
    public class RolePermissionService:IRolePermissionService
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;

        public RolePermissionService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<ResponseModel> AddRolePermission(string roleId, List<string> permissions)
        {
            try
            {
                var checkRole = await _roleManager.FindByIdAsync(roleId);
                if (checkRole == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist role with id={roleId}",
                        IsSuccess = false
                    };
                }
                List<RolePermission> rolePermissions = new List<RolePermission>();
                foreach (var permission in permissions)
                {
                    var checkPermission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == permission);
                    if (checkPermission == null)
                    {
                        return new ResponseModel
                        {
                            Message = $"Not exist permission with id={permission}",
                            IsSuccess = false
                        };
                        
                    }
                    var rp = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permission,
                    };
                    rolePermissions.Add(rp);
                }
                await _context.rolePermissions.AddRangeAsync(rolePermissions);
                int numberChange= await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = "Error when create role permission",
                        IsSuccess = false
                    };
                }
                return new ResponseModel {
                    Message = "Add role permission successful",
                    IsSuccess = true,
                    Data = rolePermissions
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when create role permission: {ex.Message}",
                    IsSuccess = false
                };
            }
        }
    }
}
