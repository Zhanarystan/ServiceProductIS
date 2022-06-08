using System.Threading.Tasks;
using API.Models;
using API.DTOs;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IMetricRepository
    {
        Task<Metric> GetMetric(int id);
        Task<IEnumerable<Metric>> GetMetrics();
        Task<Metric> CreateMetric(Metric dto);
        Task<Metric> UpdateMetric(Metric metric);
        Task<int> RemoveMetric(Metric metric);
        Task<Metric> GetMetricByName(string name);
    }
}