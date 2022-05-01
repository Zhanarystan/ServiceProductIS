using System.Collections.Generic;
using System.Linq;
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
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EstablishmentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EstablishmentDto> GetEstablishment(int id)
        {
            return await _context.Establishments.Where(e => e.Id == id)
                .ProjectTo<EstablishmentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId)
        {
            return await _context.EstablishmentProduct
                    .Where(ep => ep.ProductId == productId)
                    .Include(ep => ep.Establishment)
                    .ProjectTo<EstablishmentProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}