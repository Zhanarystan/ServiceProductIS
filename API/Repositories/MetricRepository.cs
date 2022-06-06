using API.Interfaces;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories
{
    public class MetricRepository : IMetricRepository
    {
        private readonly DataContext _context;
        
        
        public MetricRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Metric> GetMetric(int id) 
        {
            return await _context.Metrics.Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}