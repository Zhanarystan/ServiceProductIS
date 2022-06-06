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
    }
}