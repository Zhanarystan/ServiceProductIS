using API.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Core;
using API.Models;
using API.DTOs;

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
        public async Task<Result<Manufacturer>> GetManufacturer(int id)
        {
            return Result<Manufacturer>.Success(await _manufacturerRepository.GetManufacturer(id));
        }
        public async Task<Result<Manufacturer>> CreateManufacturer(ManufacturerCreateDto dto)
        {  
            var manufacturer = new Manufacturer
            {
                Name = dto.Name, 
                Description = dto.Description
            };
            manufacturer = await _manufacturerRepository.CreateManufacturer(manufacturer);
            return Result<Manufacturer>.Success(manufacturer); 
        }
        public async Task<Result<int>> RemoveManufacturer(int id)
        {
            var manufacturer = await _manufacturerRepository.GetManufacturer(id);
            if(await _manufacturerRepository.RemoveManufacturer(manufacturer) > 0)
                return Result<int>.Success(id);
            return Result<int>.Failure(new List<string>() {"Данные не удалены"});
        }
        public async Task<Result<Manufacturer>> UpdateManufacturer(int id, ManufacturerCreateDto dto)
        {
            var manufacturer = await _manufacturerRepository.GetManufacturer(id);
            manufacturer.Name = dto.Name;
            manufacturer.Description = dto.Description;
            return Result<Manufacturer>.Success(await _manufacturerRepository.UpdateManufacturer(manufacturer));
        }
    }
}