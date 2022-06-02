using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
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