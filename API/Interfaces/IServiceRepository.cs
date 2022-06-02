using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<ServiceDto>> GetServicesByNameMatching(string queryString);
    }
}