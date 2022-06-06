using API.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Core;
using API.Models;

namespace API.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        
        
        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<Result<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return Result<IEnumerable<Manufacturer>>.Success(await _manufacturerRepository.GetManufacturers());
        }
    }
}