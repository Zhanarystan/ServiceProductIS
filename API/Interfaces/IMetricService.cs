using System.Threading.Tasks;
using API.Models;
using API.Core;
using API.DTOs;

namespace API.Interfaces
{
    public interface IMetricService
    {
        Task<Result<IEnumerable<Metric>>> GetMetrics();
        Task<Result<Metric>> GetMetric(int id);
        Task<Result<Metric>> CreateMetric(MetricCreateDto dto);
        Task<Result<Metric>> UpdateMetric(int id, MetricCreateDto dto);
        Task<Result<int>> RemoveMetric(int id);
    }
}