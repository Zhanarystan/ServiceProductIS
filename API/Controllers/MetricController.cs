using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [AllowAnonymous]
    public class MetricController : BaseApiController
    {
        private readonly IMetricService _metricService;
        
        public MetricController(IMetricService metricService)
        {
            _metricService = metricService;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<Metric>>>> GetMetrics()
        {
            return HandleResult(await _metricService.GetMetrics());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<IEnumerable<Metric>>>> GetMetric(int id)
        {
            return HandleResult(await _metricService.GetMetric(id));
        }

        [HttpPost]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<Metric>>> CreateMetric(MetricCreateDto dto)
        {
            return HandleResult(await _metricService.CreateMetric(dto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<Metric>>> UpdateMetric(int id, MetricCreateDto dto)
        {
            return HandleResult(await _metricService.UpdateMetric(id, dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<int>>> DeleteMetric(int id)
        {
            return HandleResult(await _metricService.RemoveMetric(id));
        }
    }
}