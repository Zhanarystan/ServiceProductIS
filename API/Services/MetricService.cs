using System.Threading.Tasks;
using API.Models;
using API.Core;
using API.DTOs;
using API.Interfaces;

namespace API.Services
{
    public class MetricService : IMetricService
    {
        private readonly IMetricRepository _metricRepository;


        public MetricService(IMetricRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public async Task<Result<IEnumerable<Metric>>> GetMetrics()
        {
                
        }
        
        public async Task<Result<Metric>> GetMetric(int id)
        {
            
        }
        
        public async Task<Result<Metric>> CreateMetric(MetricCreateDto dto)
        {
            
        }
        
        public async Task<Result<Metric>> UpdateMetric(int id, MetricCreateDto dto)
        {
            
        }
        
        public async Task<Result<int>> RemoveMetric(int id)
        {
            
        }
    }
}