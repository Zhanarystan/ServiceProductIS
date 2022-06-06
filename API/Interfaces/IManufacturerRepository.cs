using System.Collections.Generic;
using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetManufacturers();
        Task<Manufacturer> GetManufacturer(int id);
    }
}