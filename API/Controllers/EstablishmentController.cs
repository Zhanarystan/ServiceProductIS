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
        private readonly IEstablishmentRepository _establishmentRepository;
        
        public EstablishmentController(IEstablishmentRepository establishmentRepository)
        {
            _establishmentRepository = establishmentRepository;
        }

        [HttpGet("GetEstablishmentsByProduct")]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> GetEstablishmentsByProduct(int productId)
        {
            return Ok(await _establishmentRepository.GetEstablishmentsByProduct(productId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstablishmentDto>> GetEstablishment(int id)
        {
            return Ok(await _establishmentRepository.GetEstablishment(id));
        }
    }
}