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
    public class EstablishmentController : BaseApiController
    {
        private readonly IEstablishmentService _establishmentService;
        
        public EstablishmentController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService;
        }

        [Authorize(Roles = "establishment_admin, establishment_seller")] 
        [HttpPost]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> GetEstablishmentsByProduct(int productId, double lat, double lon)
        {
            return HandleResult(await _establishmentService.GetEstablishmentsByProduct(productId, lat, lon));
        }
    }
}