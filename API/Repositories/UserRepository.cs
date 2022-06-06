using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<bool> CreateUser(RegisterDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUser(string id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAtEstablishment(int id)
        {
            return await _context.Users.Where(u => u.EstablishmentId == id).ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<bool> UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}