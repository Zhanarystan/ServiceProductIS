using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces 
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<EstablishmentListDto>> GetEstablishments();
        Task<EstablishmentDto> GetEstablishment(int id);
        Task<bool> CreateEstablishment(EstablishmentCreateDto establishment);
        Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId, double userLat, double userLon); 
    }
}