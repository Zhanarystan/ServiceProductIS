using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IMetricRepository
    {
        Task<Metric> GetMetric(int id);
    }
}