using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace API.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataContext _context;
        
    
        public ManufacturerRepository(DataContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<Manufacturer>> GetManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetManufacturer(int id)
        {
            return await _context.Manufacturers.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Manufacturer> GetManufacturerByName(string name)
        {
            return await _context.Manufacturers.Where(m => m.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Manufacturer> CreateManufacturer(Manufacturer manufacturer)
        {
            await _context.Manufacturers.AddAsync(manufacturer);
            await _context.SaveChangesAsync();
            return manufacturer;
        }

        public async Task<int> RemoveManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturers.Remove(manufacturer);
            return await _context.SaveChangesAsync();
        }

        public async Task<Manufacturer> UpdateManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturers.Update(manufacturer);
            await _context.SaveChangesAsync();
            return manufacturer;
        }
    }
}