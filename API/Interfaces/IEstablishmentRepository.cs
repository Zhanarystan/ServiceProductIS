using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IEstablishmentRepository
    {
        Task<IEnumerable<EstablishmentListDto>> GetEstablishments();
        Task<EstablishmentDto> GetEstablishment(int id);
        Task<bool> CreateEstablishment(Establishment establishment);
        Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId); 
    }
}