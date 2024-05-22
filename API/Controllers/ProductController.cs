using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

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

        

        [HttpGet("parse")]
        public IActionResult ParseSmartphones()
        {
            string chromeDriverPath = @"path\to\chromedriver";

            var options = new ChromeOptions();
            options.AddArgument("--headless"); // Запуск в безголовом режиме

            using (var driver = new ChromeDriver(chromeDriverPath, options))
            {
                driver.Navigate().GoToUrl("https://www.wildberries.ru/catalog/0/search.aspx?search=смартфон");

                var smartphones = new List<Smartphone>();

                // Подождем, пока страница загрузится
                System.Threading.Thread.Sleep(5000);

                // Найдем все элементы с информацией о смартфонах
                var smartphoneElements = driver.FindElements(By.CssSelector(".product-card"));

                foreach (var element in smartphoneElements)
                {
                    try
                    {
                        var nameElement = element.FindElement(By.CssSelector(".goods-name"));
                        var priceElement = element.FindElement(By.CssSelector(".lower-price"));
                        var urlElement = element.FindElement(By.CssSelector("a"));

                        var smartphone = new Smartphone
                        {
                            Name = nameElement.Text,
                            Price = priceElement.Text,
                            Url = urlElement.GetAttribute("href")
                        };

                        smartphones.Add(smartphone);
                    }
                    catch (Exception e)
                    {
                        // Пропустим элемент, если не удалось найти нужные подэлементы
                        continue;
                    }
                }

                return Ok(smartphones);
            }
        }

    }
}

public class Smartphone
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string Url { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Price} - {Url}";
    }
}