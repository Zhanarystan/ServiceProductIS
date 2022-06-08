using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Core;
using API.DTOs;

namespace API.Interfaces 
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<EstablishmentListDto>> GetEstablishments();
        Task<EstablishmentDto> GetEstablishment(int id);
        Task<bool> CreateEstablishment(EstablishmentCreateDto establishment);
        Task<Result<IEnumerable<EstablishmentProductDto>>> GetEstablishmentsByProduct(int productId, double userLat, double userLon); 
        Task<Result<IEnumerable<EstablishmentServiceDto>>> GetEstablishmentsByService(int serviceId, double userLat, double userLon);
        Task<Result<EstablishmentProductDto>> UpdateProduct(EstablishmentProductDto productDto);
        Task<Result<EstablishmentDto>> UpdatePhoto(int establishmentId, IFormFile file);
        Task<Result<EstablishmentCreateDto>> UpdateEstablishment(int id, EstablishmentCreateDto dto);
        Task<Result<int>> CreateEstablishmentProductList(List<EstablishmentProductCreateDto> list);
    }
}