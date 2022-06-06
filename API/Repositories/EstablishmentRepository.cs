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

        public async Task<IEnumerable<EstablishmentListDto>> GetEstablishments()
        {
            return await _context.Establishments.ProjectTo<EstablishmentListDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<EstablishmentDto> GetEstablishmentDto(int id)
        {
            return await _context.Establishments.Where(e => e.Id == id)
                .ProjectTo<EstablishmentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<Establishment> GetEstablishment(int id)
        {
            return await _context.Establishments.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateEstablishment(Establishment establishment)
        {
            _context.Establishments.Add(establishment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId)
        {
            return await _context.EstablishmentProduct
                    .Where(ep => ep.ProductId == productId)
                    .Include(ep => ep.Establishment)
                    .ProjectTo<EstablishmentProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<IEnumerable<EstablishmentServiceDto>> GetEstablishmentsByService(int serviceId)
        {
            return await _context.EstablishmentService
                .Where(es => es.ServiceId == serviceId)
                .Include(es => es.Establishment)
                .ProjectTo<EstablishmentServiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<EstablishmentProduct> GetEstablishmentProduct(int productId, int establishmentId)
        {
            return await _context.EstablishmentProduct
                .Where(ep => ep.ProductId == productId && ep.EstablishmentId == establishmentId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(EstablishmentProduct product)
        {
            _context.EstablishmentProduct.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEstablishment(Establishment establishment)
        {
            _context.Establishments.Update(establishment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}