using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Core;
using API.Models;
using API.Photos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.IO;
namespace API.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMetricRepository _metricRepository;
        public ServiceService(IServiceRepository serviceRepository, IMetricRepository metricRepository)
        {
            _serviceRepository = serviceRepository;
            _metricRepository = metricRepository;
        }
        public async Task<Result<IEnumerable<ServiceDto>>> GetServices()
        {
            return Result<IEnumerable<ServiceDto>>.Success(await _serviceRepository.GetServices());
        }
        public async Task<Result<ServiceCreateDto>> CreateService(ServiceCreateDto dto)
        {
            var service = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                MetricId = dto.MetricId
            }; 
            service = await _serviceRepository.CreateService(service);
            return Result<ServiceCreateDto>.Success(dto);
        }
        public async Task<Result<ServiceDto>> GetService(int id)
        {
            var service = await _serviceRepository.GetService(id);
            var metric = await _metricRepository.GetMetric(service.MetricId);
            var dto = new ServiceDto 
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Metric = metric.Name,
                MetricId = metric.Id,
            };
            return Result<ServiceDto>.Success(dto);
        }
        public async Task<Result<ServiceDto>> UpdateService(int id, ServiceDto dto)
        {
            var service = await _serviceRepository.GetService(id);
            service.Name = dto.Name;
            service.Description = dto.Description;
            service.MetricId = dto.MetricId;
            await _serviceRepository.UpdateService(service);
            return Result<ServiceDto>.Success(dto);
        }
        public async Task<Result<int>> RemoveService(int id)
        {
            var service = await _serviceRepository.GetService(id);
            return Result<int>.Success(await _serviceRepository.RemoveService(service));
        }
        public async Task<Result<IEnumerable<ServiceDto>>> CreateServicesFromCsv(IFormFile file)
        {
            var serviceList = new List<Service>();
            if(file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var reader = new StreamReader(stream);
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if(values[0] == "name" || String.IsNullOrEmpty(values[0])) continue;
                    if(await _serviceRepository.GetServiceByName(values[0]) != null) continue;
                    var service = new Service();
                    service.Name = values[0];
                    if(!String.IsNullOrEmpty(values[1]))
                    {
                        var metric = await _metricRepository.GetMetricByName(values[1]);
                        if(metric == null)
                            return Result<IEnumerable<ServiceDto>>.Failure(new List<string>() {"Сервисы не созданы"}); 
                        service.MetricId = metric.Id;
                    }
                    serviceList.Add(service);
                }
            }
            if(await _serviceRepository.CreateServiceList(serviceList) > 0)
                return Result<IEnumerable<ServiceDto>>.Success(await _serviceRepository.GetServices());
            return Result<IEnumerable<ServiceDto>>.Failure(new List<string>() {"Сервисы не созданы"});
        }
    }
}