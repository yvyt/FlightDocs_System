using FlightDocs_System.Model;

namespace FlightDocs_System.Service.Roles
{
    public interface IRoleService
    {
        Task<ResponseModel> AddRole(RoleCreate role);
        Task<ResponseModel> EditRole(string id, RoleCreate role);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(string id);
    }
}
