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
    }
}