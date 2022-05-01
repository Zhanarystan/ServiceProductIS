using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces 
{
    public interface IEstablishmentService
    {
        Task<EstablishmentDto> GetEstablishment(int id);
        Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId, double userLat, double userLon); 
    }
}