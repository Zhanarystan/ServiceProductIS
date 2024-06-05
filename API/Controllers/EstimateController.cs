using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using API.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using System.Linq;
using System;

namespace API.Controllers
{
    public class EstimateController : BaseApiController
    {
        private readonly IEstimateService _estimateService;
        private readonly DataContext _dataContext;
        
        public EstimateController(IEstimateService estimateService, DataContext dataContext)
        {
            _estimateService = estimateService;
            _dataContext = dataContext;
        }

        [Authorize(Roles = "establishment_admin, establishment_seller")] 
        [HttpPost]
        public async Task<ActionResult<IEnumerable<EstablishmentProduct>>> CreateEstimate(EstimateCreateDto dto)
        {
            return HandleResult(await _estimateService.CreateEstimate(dto));
        }

        [AllowAnonymous]
        [HttpPost("GenerateRandom")]
        public async Task<string> GenerateData() 
        {
            var random = new Random();
            var redudantProductIds = new List<int>() { 1, 2, 3 }; 
            var products = _dataContext
                            .EstablishmentProduct
                            .Where(ep => ep.EstablishmentId == 5 && !redudantProductIds.Contains(ep.ProductId))
                            .ToList();

            // var startDate = new DateTime(2023, 10, 1);
            // var endDate = new DateTime(2024, 5, 11);
            var startDate = new DateTime(2024, 5, 1);
            var endDate = new DateTime(2024, 5, 31);

            var date = startDate;
            int establishmentId = 5;
            while (date <= endDate)
            {
                var numberOfEntries = random.Next(200, 300); // Случайное количество записей от 200 до 400

                for (int i = 0; i < numberOfEntries; i++)
                {
                    int k = random.Next(1, 5);
                    var localProducts = new List<EstablishmentProduct>(products);

                    var estimateProducts = CreateRandomEstimateProducts(localProducts, k);

                    var estimate = new EstimateCreateDto
                    {
                        TotalSum = estimateProducts.Sum(ep => ep.TotalSum),
                        CreatedById = "3317f107-cb25-4496-978a-6640c11b739d",
                        EstablishmentId = establishmentId,
                        CreatedAt = date.AddDays(random.NextDouble()), // Случайное время в течение дня
                        Products = estimateProducts,
                        Services = new List<EstimateServiceCreateDto>()
                    };
                    // Добавление других свойств сущности, если необходимо
                    await _estimateService.CreateEstimate(estimate);
                    // context.MyEntities.Add(myEntity);
                }

                // await context.SaveChangesAsync();
                Console.WriteLine($"Added {numberOfEntries} entries for {date.ToShortDateString()}");
                date = date.AddDays(1); // Переход к следующему дню
            }

            return "Ok";
        }

        public static List<EstimateProductCreateDto> CreateRandomEstimateProducts(List<EstablishmentProduct> products, int k)
        {
            Random random = new Random();
            List<EstimateProductCreateDto> estimateProducts = new List<EstimateProductCreateDto>();

            for (int i = 0; i < k; i++)
            {
                int index = random.Next(0, products.Count);

                var product = products[index];
                
                var estimateProduct = new EstimateProductCreateDto
                {
                    Price = product.Price,
                    Amount = 1,
                    TotalSum = product.Price,
                    ProductId = product.ProductId,
                    Metric = "тг/шт"
                };

                estimateProducts.Add(estimateProduct);

                products.RemoveAt(index);
            }

            return estimateProducts;
        }
    }
}