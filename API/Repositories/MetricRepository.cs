using API.Interfaces;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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

        public async Task<Metric> GetMetricByName(string name)
        {
            return await _context.Metrics.Where(m => m.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Metric>> GetMetrics()
        {
            return await _context.Metrics.ToListAsync();
        }
        
        public async Task<Metric> CreateMetric(Metric metric)
        {
            await _context.Metrics.AddAsync(metric);
            await _context.SaveChangesAsync();
            return metric;
        }
        
        public async Task<Metric> UpdateMetric(Metric metric)
        {
            _context.Metrics.Update(metric);
            await _context.SaveChangesAsync();
            return metric;
        }
        
        public async Task<int> RemoveMetric(Metric metric)
        {
            _context.Metrics.Remove(metric);
            return await _context.SaveChangesAsync();
        }
    }
}