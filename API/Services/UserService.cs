using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Core;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IEstablishmentRepository _establishmentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IList<string> _modelErrors;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, 
            UserManager<AppUser> userManager, IEstablishmentRepository establishmentRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userManager = userManager;
            _establishmentRepository = establishmentRepository;
            _modelErrors = new List<string>();
        }

        public async Task<Result<IEnumerable<AppUserDto>>> GetUsersAtEstablishment()
        {
            var currentUser = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            
            var users = await _userRepository.GetUsersAtEstablishment((int)currentUser.EstablishmentId);
            var usersDto = new List<AppUserDto>();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersDto.Add(
                    new AppUserDto 
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email,
                        IIN = user.IIN,
                        Roles = roles
                    }
                );
            } 
            return Result<IEnumerable<AppUserDto>>.Success(usersDto);
        }

        public async Task<Result<AppUserDto>> GetUser(string id)
        {
            var user = await _userRepository.GetUser(id);
            var establishment = await _establishmentRepository.GetEstablishment((int)user.EstablishmentId);
            var userDto = new AppUserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                IIN = user.IIN,
                EstablishmentId = (int)establishment.Id,
                Establishment = establishment.Name,
                Day = user.Birthdate.Day,
                Month = user.Birthdate.Month,
                Year = user.Birthdate.Year, 
                Roles = await _userManager.GetRolesAsync(user)
            };
            return Result<AppUserDto>.Success(userDto);
        }

        public async Task<Result<RegisterDto>> CreateUser(RegisterDto userDto)
        {
            if(! await ValidateUser(userDto)) 
                return Result<RegisterDto>.Failure(_modelErrors);
            
            var user = new AppUser
            {
                Email = userDto.Email,
                UserName = userDto.Username,
                FirstName = userDto.FirstName,
                SecondName = userDto.SecondName,
                EstablishmentId = userDto.EstablishmentId,
                IIN = userDto.IIN,
                Birthdate = new DateTime((int)userDto.Year, (int)userDto.Month, (int)userDto.Day)
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if(result.Succeeded)
            {
                foreach(var role in userDto.Roles)
                    await _userManager.AddToRoleAsync(user, role.ToUpper());
                userDto.Password = "";
                return Result<RegisterDto>.Success(userDto);
            } 

            return Result<RegisterDto>.Failure(new List<string>(){"Пользователь не создан"});
        }

        public async Task<Result<IEnumerable<AppUserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var usersDto = new List<AppUserDto>();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersDto.Add(
                    new AppUserDto 
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Email = user.Email,
                        IIN = user.IIN,
                        Roles = roles
                    }
                );
            } 
            return Result<IEnumerable<AppUserDto>>.Success(usersDto);
        }

        public async Task<Result<AppUserDto>> UpdateUser(AppUserDto dto)
        {
            return Result<AppUserDto>.Success(dto);
            // var user = await _userRepository.GetUser(dto.Id);
            // if(!String.IsNullOrEmpty(dto.Password)) {
                
            // }
        }

        private async Task<bool> ValidateUser(RegisterDto userDto)
        {
            if(await _userManager.Users.AnyAsync(x => x.Email == userDto.Email))
                _modelErrors.Add("Пользователь с таким email уже существует");
            if(await _userManager.Users.AnyAsync(x => x.UserName == userDto.Username))
                _modelErrors.Add("Пользователь с таким именем уже существует");
            
            string dateString = (userDto.Day < 10 ? "0" + userDto.Day : "" + userDto.Day) + 
                "/" + (userDto.Month < 10 ? "0" + userDto.Month : "" + userDto.Month) + 
                "/" + userDto.Year;
            bool dateValidity = DateTime.TryParseExact(dateString, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d);
            
            if(!dateValidity)
                _modelErrors.Add("Неверный значение для даты");
            
            return _modelErrors.Count == 0;
        }

        private string GetModelErrors()
        {
            string errors = "";
            foreach(var err in _modelErrors) 
                errors += err + "\n";
            return errors; 
        }
    }
}