using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Core;
using API.Models;
using API.Photos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.IO;


namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMetricRepository _metricRepository;
        
        
        public ProductService
        (
            IProductRepository productRepository, 
            IManufacturerRepository manufacturerRepository,
            IMetricRepository MetricRepository    
        )
        {
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;
            _metricRepository = MetricRepository;
        }
        
        public async Task<Result<IEnumerable<ProductDto>>> GetProducts() 
        {
            return Result<IEnumerable<ProductDto>>.Success(await _productRepository.GetProducts());
        }

        public async Task<Result<ProductCreateDto>> CreateProduct(ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                BarCode = dto.BarCode,
                Description = dto.Description,
                ManufacturerId = dto.ManufacturerId,
                MetricId = dto.MetricId,
                IsCustom = dto.IsCustom
            }; 
            product = await _productRepository.CreateProduct(product);
            return Result<ProductCreateDto>.Success(dto);
        }

        public async Task<Result<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            var manufacturer = await _manufacturerRepository.GetManufacturer(product.ManufacturerId);
            var metric = await _metricRepository.GetMetric(product.MetricId);
            var dto = new ProductDto 
            {
                Id = product.Id,
                BarCode = product.BarCode,
                Name = product.Name,
                IsCustom = product.IsCustom,
                Description = product.Description,
                Metric = metric.Name,
                MetricId = metric.Id,
                Manufacturer = manufacturer.Name,
                ManufacturerId = manufacturer.Id
            };
            return Result<ProductDto>.Success(dto);
        }

        public async Task<Result<ProductDto>> UpdateProduct(int id, ProductDto dto)
        {
            var product = await _productRepository.GetProduct(id);
            product.Name = dto.Name;
            product.BarCode = dto.BarCode;
            product.IsCustom = dto.IsCustom;
            product.Description = dto.Description;
            product.MetricId = dto.MetricId;
            product.ManufacturerId = dto.ManufacturerId;
            await _productRepository.UpdateProduct(product);
            return Result<ProductDto>.Success(dto);
        }

        public async Task<Result<int>> RemoveProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            return Result<int>.Success(await _productRepository.RemoveProduct(product));
        }

        public async Task<Result<IEnumerable<ProductDto>>> CreateProductsFromCsv(IFormFile file)
        {
            var productList = new List<Product>();
            if(file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var reader = new StreamReader(stream);
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if(values[0] == "name" || String.IsNullOrEmpty(values[0])) continue;
                    if(await _productRepository.GetProductByName(values[0]) != null) continue;
                    var product = new Product();
                    product.Name = values[0];
                    product.BarCode = values[1];
                    if(!String.IsNullOrEmpty(values[2]))
                    {
                        var manufacturer = await _manufacturerRepository.GetManufacturerByName(values[2]);
                        if(manufacturer != null)
                            product.ManufacturerId = manufacturer.Id;
                    }
                    if(!String.IsNullOrEmpty(values[3]))
                    {
                        var metric = await _metricRepository.GetMetricByName(values[3]);
                        if(metric != null) 
                            product.MetricId = metric.Id;
                    }
                    productList.Add(product);
                }
            }
            if(await _productRepository.CreateProductList(productList) > 0)
                return Result<IEnumerable<ProductDto>>.Success(await _productRepository.GetProducts());
            
            return Result<IEnumerable<ProductDto>>.Failure(new List<string>() {"Продукты не созданы"});
        }
    }
}
