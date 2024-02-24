using FlightDocs_System.Model;

namespace FlightDocs_System.Service.RolePermissions
{
    public interface IRolePermissionService
    {
        Task<ResponseModel> AddRolePermission(string roleId, List<string> permissions);
        Task<ResponseModel> GetDetails(string roleId);
        Task<ResponseModel> UpdateRolePermission(string roleId, List<string> permissions);
    }
}
