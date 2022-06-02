using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IEstablishmentRepository
    {
        Task<IEnumerable<EstablishmentListDto>> GetEstablishments();
        Task<EstablishmentDto> GetEstablishmentDto(int id);
        Task<Establishment> GetEstablishment(int id);
        Task<bool> CreateEstablishment(Establishment establishment);
        Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId); 
        Task<IEnumerable<EstablishmentServiceDto>> GetEstablishmentsByService(int serviceId);
        Task<EstablishmentProduct> GetEstablishmentProductByIdentifier(int productId, int establishmentId);
        Task<bool> UpdateEstablishment(Establishment establishment);
        Task<bool> UpdateProduct(EstablishmentProduct product);
    }
}