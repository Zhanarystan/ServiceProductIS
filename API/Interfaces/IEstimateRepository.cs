using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IEstimateRepository
    {
        Task<Estimate> CreateEstimate(Estimate estimate);
        Task<bool> CreateEstimateProductList(List<EstimateProduct> products);
        Task<bool> CreateEstimateServiceList(List<EstimateService> services);
    }
}