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
        Task<bool> UpdateEstablishment(Establishment establishment);
        Task<bool> UpdateProduct(EstablishmentProduct product);
        Task<EstablishmentProduct> GetEstablishmentProduct(int productId, int establishmentId);
        Task<int> CreateEstablishmentProductList(List<EstablishmentProduct> list);
    }
}