using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using API.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ManufacturerController : BaseApiController
    {
        private readonly IManufacturerService _manufacturerService;
        
        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return HandleResult(await _manufacturerService.GetManufacturers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<Manufacturer>>> GetManufacturer(int id)
        {
            return HandleResult(await _manufacturerService.GetManufacturer(id));
        }

        [HttpPost]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<Manufacturer>>> CreateManufacturer(ManufacturerCreateDto dto)
        {
            return HandleResult(await _manufacturerService.CreateManufacturer(dto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<Manufacturer>>> UpdateManufacturer(int id, ManufacturerCreateDto dto)
        {
            return HandleResult(await _manufacturerService.UpdateManufacturer(id, dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<int>>> DeleteManufacturer(int id) 
        {
            return HandleResult(await _manufacturerService.RemoveManufacturer(id));
        }
    }
}