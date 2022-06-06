using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using API.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EstimateController : BaseApiController
    {
        private readonly IEstimateService _estimateService;
        
        public EstimateController(IEstimateService estimateService)
        {
            _estimateService = estimateService;
        }

        [Authorize(Roles = "establishment_admin, establishment_seller")] 
        [HttpPost]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> CreateEstimate(EstimateCreateDto dto)
        {
            return HandleResult(await _estimateService.CreateEstimate(dto));
        }
    }
}