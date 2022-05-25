using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetProductsByQuery")]
        public async Task<IEnumerable<ProductDto>> GetProductsByQuery(string queryString)
        {
            var products = await _productRepository.GetProductsByNameMatching(queryString);
            return products;
        }
    }
}