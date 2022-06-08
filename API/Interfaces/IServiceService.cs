using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Core;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IServiceService
    {
        Task<Result<IEnumerable<ServiceDto>>> GetServices();
        Task<Result<ServiceCreateDto>> CreateService(ServiceCreateDto dto);
        Task<Result<ServiceDto>> GetService(int id);
        Task<Result<ServiceDto>> UpdateService(int id, ServiceDto dto);
        Task<Result<int>> RemoveService(int id);
        Task<Result<IEnumerable<ServiceDto>>> CreateServicesFromCsv(IFormFile file);
    }
}