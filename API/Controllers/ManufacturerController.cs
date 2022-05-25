using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ManufacturerController : BaseApiController
    {
        private readonly IEstablishmentService _establishmentService;
        
        public ManufacturerController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService;
        }

        [HttpGet("GetEstablishmentsByProduct")]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> GetEstablishmentsByProduct(int productId, double lat, double lon)
        {
            return Ok(await _establishmentService.GetEstablishmentsByProduct(productId, lat, lon));
        }
    
        [HttpGet]
        public async Task<ActionResult<EstablishmentListDto>> GetEstablishments()
        {
            return Ok(await _establishmentService.GetEstablishments());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstablishment(EstablishmentCreateDto establishment)
        {
            return Ok(await _establishmentService.CreateEstablishment(establishment));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstablishmentDto>> GetEstablishment(int id)
        {
            return Ok(await _establishmentService.GetEstablishment(id));
        }
    }
}