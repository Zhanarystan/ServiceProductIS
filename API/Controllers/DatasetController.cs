using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Data;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class DatasetController : BaseApiController
{
    private readonly DataContext _context;

    public DatasetController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("sales-for-period/{establishmentId}")]
    public async Task<ActionResult<IEnumerable<DailySales>>> GetSalesForPeriod(int establishmentId, [FromQuery]Period period)
    {
        var estimates = _context
                            .Estimates
                            .Where(e => 
                                e.EstablishmentId == establishmentId &&
                                e.CreatedAt >= period.Start &&
                                e.CreatedAt <= period.End)
                            .ToList();

        var result = estimates
                        .GroupBy(e => e.CreatedAt.Date)
                        .Select(e => 
                            new DailySales 
                            { 
                                Date = e.Key, 
                                Sales = e.Sum(x => x.TotalSum) 
                            });

        return HandleResult(Result<IEnumerable<DailySales>>.Success(result));
    }
}