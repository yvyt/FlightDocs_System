using FlightDocs_System.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FlightDocs_System.Service.User
{
    public interface IUserService
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(string id);
        Task<ResponseModel> Login(UserLogin user);
        Task<ResponseModel> Register(string role, UserRegister user);
        Task<UserDTO> GetUserByToken(string token);
    }
}
