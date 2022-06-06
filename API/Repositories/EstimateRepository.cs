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
    public class EstimateRepository : IEstimateRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public EstimateRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Estimate> CreateEstimate(Estimate estimate)
        {
            _context.Estimates.Add(estimate);
            await _context.SaveChangesAsync();
            return estimate;
        }

        public async Task<bool> CreateEstimateProductList(List<EstimateProduct> products)
        {
            await _context.EstimateProduct.AddRangeAsync(products);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateEstimateServiceList(List<EstimateService> services)
        {
            await _context.EstimateService.AddRangeAsync(services);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}