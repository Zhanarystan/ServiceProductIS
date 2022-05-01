using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
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

        [HttpGet("GetEstablishmentsByProduct")]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> GetEstablishmentsByProduct(int productId, double lat, double lon)
        {
            return Ok(await _establishmentService.GetEstablishmentsByProduct(productId, lat, lon));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstablishmentDto>> GetEstablishment(int id)
        {
            return Ok(await _establishmentService.GetEstablishment(id));
        }
    }
}