using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core;
using API.DTOs;
using Application.Core;

namespace API.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<AppUserDto>>> GetUsersAtEstablishment();
        Task<AppUserDto> GetUser(string id);
        Task<Result<RegisterDto>> CreateUser(RegisterDto user);
    }
}