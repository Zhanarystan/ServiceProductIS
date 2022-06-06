using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Core;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetProducts();
        Task<Result<ProductCreateDto>> CreateProduct(ProductCreateDto dto);
        Task<Result<ProductDto>> GetProduct(int id);
        Task<Result<ProductDto>> UpdateProduct(int id, ProductDto dto);
        Task<Result<int>> RemoveProduct(int id);
    }
}