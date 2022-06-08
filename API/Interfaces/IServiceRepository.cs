using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<ServiceDto>> GetServices();
        Task<Service> CreateService(Service service);
        Task<int> CreateServiceList(List<Service> services);
        Task<Service> GetService(int id);
        Task<Service> UpdateService(Service service);
        Task<int> RemoveService(Service service);
        Task<Service> GetServiceByName(string name);
        Task<IEnumerable<ServiceDto>> GetServicesByNameMatching(string queryString);
    }
}