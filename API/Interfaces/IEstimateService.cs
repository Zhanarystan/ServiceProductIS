using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Core;
using API.DTOs;
using API.Models;

namespace API.Interfaces 
{
    public interface IEstimateService
    {
        Task<Result<EstimateDto>> CreateEstimate(EstimateCreateDto dto);
    }
}