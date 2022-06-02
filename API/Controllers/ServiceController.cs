using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ServiceController : BaseApiController
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet("GetServicesByQuery")]
        public async Task<IEnumerable<ServiceDto>> GetServicesByQuery(string queryString)
        {
            var services = await _serviceRepository.GetServicesByNameMatching(queryString);
            return services;
        }
    }
}