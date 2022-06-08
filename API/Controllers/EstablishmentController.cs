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
    [AllowAnonymous]
    public class EstablishmentController : BaseApiController
    {
        private readonly IEstablishmentService _establishmentService;
        
        public EstablishmentController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService;
        }

        [AllowAnonymous]
        [HttpGet("GetEstablishmentsByProduct")]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> GetEstablishmentsByProduct(int productId, double lat, double lon)
        {
            return HandleResult(await _establishmentService.GetEstablishmentsByProduct(productId, lat, lon));
        }

        [AllowAnonymous]
        [HttpGet("GetEstablishmentsByService")]
        public async Task<ActionResult<IEnumerable<EstablishmentService>>> GetEstablishmentsByService(int serviceId, double lat, double lon)
        {
            return HandleResult(await _establishmentService.GetEstablishmentsByService(serviceId, lat, lon));
        }

        [Authorize]
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

        [HttpPut("updateProduct")]
        public async Task<ActionResult<Result<EstablishmentDto>>> UpdateProduct(EstablishmentProductDto productDto) 
        {
            return HandleResult(await _establishmentService.UpdateProduct(productDto));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<EstablishmentDto>> GetEstablishment(int id)
        {
            return Ok(await _establishmentService.GetEstablishment(id));
        }

        [HttpPost("uploadPhoto/{id}")]
        [Authorize(Roles = "establishment_admin")]
        public async Task<ActionResult<EstablishmentDto>> UploadPhoto(int id, IFormFile file)
        {
            return HandleResult(await _establishmentService.UpdatePhoto(id, file));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "establishment_admin, system_admin")]
        public async Task<ActionResult<EstablishmentCreateDto>> UpdateEstablishment(int id, EstablishmentCreateDto establishment) 
        {
            return HandleResult(await _establishmentService.UpdateEstablishment(id, establishment));
        }
    }
}