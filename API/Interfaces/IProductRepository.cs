using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<ProductDto>> GetProductsByNameMatching(string queryString);
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProduct(int id);
        Task<Product> UpdateProduct(Product product);
        Task<int> RemoveProduct(int id);
    }
}