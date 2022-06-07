using System.Collections.Generic;
using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetManufacturers();
        Task<Manufacturer> GetManufacturer(int id);
        Task<Manufacturer> CreateManufacturer(Manufacturer manufacturer);
        Task<int> RemoveManufacturer(Manufacturer manufacturer);
        Task<Manufacturer> GetManufacturerByName(string name);
        Task<Manufacturer> UpdateManufacturer(Manufacturer manufacturer);
    }
}