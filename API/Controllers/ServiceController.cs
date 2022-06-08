using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Core;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ServiceController : BaseApiController
    {
        private readonly IServiceService _serviceService;
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository, IServiceService serviceService)
        {
            _serviceRepository = serviceRepository;
            _serviceService = serviceService;
        }

        [HttpGet]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            return HandleResult(await _serviceService.GetServices());
        }

        [HttpPost]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<ServiceCreateDto>>> CreateService(ServiceCreateDto dto)
        {
            return HandleResult(await _serviceService.CreateService(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ServiceDto>>> GetService(int id)
        {
            return HandleResult(await _serviceService.GetService(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<ServiceDto>>> UpdateService(int id, ServiceDto dto)
        {  
            return HandleResult(await _serviceService.UpdateService(id, dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<int>>> DeleteService(int id)
        {
            return HandleResult(await _serviceService.RemoveService(id));
        }

        [HttpGet("GetServicesByQuery")]
        public async Task<IEnumerable<ServiceDto>> GetServicesByQuery(string queryString)
        {
            var services = await _serviceRepository.GetServicesByNameMatching(queryString);
            return services;
        }

        [HttpPost("createFromCsv")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<IEnumerable<ServiceDto>>>> CreateServicesFromCsv(IFormFile file)
        {
            return HandleResult(await _serviceService.CreateServicesFromCsv(file));
        }
    }
}