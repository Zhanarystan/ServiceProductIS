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
        private readonly DataContext _context;
        
        public MetricController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Result<IEnumerable<Metric>>>> GetMetrics()
        {
            var metrics = await _context.Metrics.ToListAsync();
            return HandleResult(Result<IEnumerable<Metric>>.Success(metrics));
        }
    }
}