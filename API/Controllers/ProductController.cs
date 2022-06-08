using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        public ProductController(IProductRepository productRepository, IProductService productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "system_admin,establishment_admin")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return HandleResult(await _productService.GetProducts());
        }

        [HttpPost]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<ProductCreateDto>>> CreateProduct(ProductCreateDto dto)
        {
            return HandleResult(await _productService.CreateProduct(dto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ProductDto>>> GetProduct(int id)
        {
            return HandleResult(await _productService.GetProduct(id));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<ProductDto>>> UpdateProduct(int id, ProductDto dto)
        {  
            return HandleResult(await _productService.UpdateProduct(id, dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<int>>> DeleteProduct(int id)
        {
            return HandleResult(await _productService.RemoveProduct(id));
        }

        [HttpGet("GetProductsByQuery")]
        public async Task<IEnumerable<ProductDto>> GetProductsByQuery(string queryString)
        {
            var products = await _productRepository.GetProductsByNameMatching(queryString);
            return products;
        }

        [HttpPost("createFromCsv")]
        [Authorize(Roles = "system_admin")]
        public async Task<ActionResult<Result<IEnumerable<ProductDto>>>> CreateProductsFromCsv(IFormFile file)
        {
            return HandleResult(await _productService.CreateProductsFromCsv(file));
        }
    }
}