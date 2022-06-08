using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using API.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ServiceRepository : IServiceRepository 
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ServiceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetServices()
        {
            return await _context.Services
                .AsQueryable()
                .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Service> CreateService(Service service)
        {   
            service.NormalizeName();
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<int> CreateServiceList(List<Service> services)
        {
            foreach(var s in services)
                s.NormalizeName();
            _context.Services.AddRange(services);
            return await _context.SaveChangesAsync();
        }

        public async Task<Service> GetService(int id)
        {
            return await _context.Services.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Service> UpdateService(Service service)
        {
            service.NormalizeName();
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<int> RemoveService(Service service)
        {
            _context.Services.Remove(service);
            return await _context.SaveChangesAsync();
        }

        public async Task<Service> GetServiceByName(string name)
        {
            return await _context.Services.Where(s => s.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ServiceDto>> GetServicesByNameMatching(string queryString)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9А-Яа-яәӘіІңҢғҒүҮұҰқҚөӨһҺ]");
            queryString = rgx.Replace(queryString, "").ToLower();
            return await _context.Services.Where(p => p.NormalizedName.StartsWith(queryString))
                .Include(p => p.Metric)
                .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}