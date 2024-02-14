﻿using FlightDocs_System.Model;

namespace FlightDocs_System.Service.User
{
    public interface IUserService
    {
        Task<ResponseModel> Login(UserLogin user);
        Task<ResponseModel> Register(string role, UserRegister user);
    }
}
