using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Core;
using API.DTOs;
using API.Models;


namespace API.Interfaces
{
    public interface IManufacturerService
    {
        Task<Result<IEnumerable<Manufacturer>>> GetManufacturers();
        Task<Result<Manufacturer>> GetManufacturer(int id);
        Task<Result<Manufacturer>> CreateManufacturer(ManufacturerCreateDto dto);
        Task<Result<int>> RemoveManufacturer(int id);
        Task<Result<Manufacturer>> UpdateManufacturer(int id, ManufacturerCreateDto manufacturer);
    }
}