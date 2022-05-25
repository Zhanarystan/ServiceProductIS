using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAtEstablishment(int id);
        Task<AppUser> GetUser(string id);
        Task<bool> CreateUser(RegisterDto user);
        Task<bool> UpdateUser();

    }
}