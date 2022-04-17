using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IEstablishmentRepository
    {
        Task<EstablishmentDto> GetEstablishment(int id);
        Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId); 
    }
}