using FlightDocs_System.Data;
using FlightDocs_System.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<ResponseModel> GetDetails(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return new ResponseModel
                {
                    Message = $"Not exist role with id={roleId}",
                    IsSuccess = false
                };
            }
            var permissions= await _context.rolePermissions.Where(x=>x.RoleId==roleId).ToListAsync();
            if (permissions.Count <= 0)
            {
                return new ResponseModel()
                {
                    Message=$"Role is not have any permission",
                    IsSuccess=false
                };
            }
            List<string> permissionName = new List<string>();
            foreach(var permission in permissions)
            {
                var per= await _context.Permissions.FirstOrDefaultAsync(x=>x.Id==permission.PermissionId);
                if (per == null)
                {
                    return new ResponseModel()
                    {
                        Message = $"Not exist permission with id={permission.Id}",
                        IsSuccess = false
                    };
                }
                permissionName.Add(per.PermissionName);
            }
            var rpDTO = new RolePermissionDTO
            {
                RoleName = role.Name,
                PermissionName = permissionName
            };
            return new ResponseModel
            {
                Message="Get detail successful",
                IsSuccess=true,
                Data=rpDTO
            };
        }

        public async Task<ResponseModel> UpdateRolePermission(string roleId, List<string> permissions)
        {
            try
            {
                var r = await _roleManager.FindByIdAsync(roleId);
                if (r == null)
                {
                    return new ResponseModel
                    {
                        Message = $"Not exist role with id={roleId}",
                        IsSuccess = false
                    };
                }
                var permissionsOfRole = await _context.rolePermissions.Where(x => x.RoleId == roleId).ToListAsync();
                if (permissionsOfRole.Count > 0)
                {
                    _context.rolePermissions.RemoveRange(permissionsOfRole);

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
                int numberChange = await _context.SaveChangesAsync();
                if (numberChange <= 0)
                {
                    return new ResponseModel
                    {
                        Message = "Error when update role permission",
                        IsSuccess = false
                    };
                }
                return new ResponseModel
                {
                    Message = "Update role permission successful",
                    IsSuccess = true,
                    Data = rolePermissions
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Message = $"Error when update role permission: {ex.Message}",
                    IsSuccess = false
                };
            }
            
        }
    }
}
