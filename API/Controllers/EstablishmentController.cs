using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using API.Core;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;

namespace API.Controllers
{
    [AllowAnonymous]
    public class EstablishmentController : BaseApiController
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly DataContext _context;
        
        public EstablishmentController(IEstablishmentService establishmentService, DataContext context)
        {
            _establishmentService = establishmentService;
            _context = context;
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

        [HttpPost("createEstablishmentProducts")]
        public async Task<ActionResult<int>> CreateEstablishmentProducts(List<EstablishmentProductCreateDto> list)
        {
            var eps = new List<EstablishmentProduct>();
            foreach(var l in list)
            {
                eps.Add
                (
                    new EstablishmentProduct
                    {
                        ProductId = l.ProductId,
                        EstablishmentId = l.EstablishmentId,
                        Price = l.Price,
                        Amount = l.Amount
                    }
                );
            }
            _context.EstablishmentProduct.AddRange(eps);
            return await _context.SaveChangesAsync(); 
        }

        // [AllowAnonymous]
        // [HttpPost("GenerateEsimates")]
        // public async Task<ActionResult<IEnumerable<EstablishmentService>>> GetEstablishmentsByService(int serviceId, double lat, double lon)
        // {
        //     return HandleResult(await _establishmentService.GetEstablishmentsByService(serviceId, lat, lon));
        // }
    }
}