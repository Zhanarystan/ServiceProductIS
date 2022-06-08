using System.Threading.Tasks;
using API.Models;
using API.Core;
using API.DTOs;
using API.Interfaces;
using System.Collections.Generic;

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
            return Result<IEnumerable<Metric>>.Success(await _metricRepository.GetMetrics());
        }
        
        public async Task<Result<Metric>> GetMetric(int id)
        {
            return Result<Metric>.Success(await _metricRepository.GetMetric(id));
        }
        
        public async Task<Result<Metric>> CreateMetric(MetricCreateDto dto)
        {
            var metric = new Metric
            {
                Name = dto.Name
            };
            return Result<Metric>.Success(await _metricRepository.CreateMetric(metric));
        }
        
        public async Task<Result<Metric>> UpdateMetric(int id, MetricCreateDto dto)
        {
            var metric = await _metricRepository.GetMetric(id);
            metric.Name = dto.Name;
            metric = await _metricRepository.UpdateMetric(metric);
            return Result<Metric>.Success(metric);
        }
        
        public async Task<Result<int>> RemoveMetric(int id)
        {
            var metric = await _metricRepository.GetMetric(id);
            return Result<int>.Success(await _metricRepository.RemoveMetric(metric));
        }
    }
}