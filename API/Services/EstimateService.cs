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

namespace API.Services
{
    public class EstimateService : IEstimateService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEstimateRepository _repository;
        private readonly IEstablishmentRepository _establishmentRepository;
        private readonly IList<string> _modelErrors;


        public EstimateService(IEstimateRepository repository, IEstablishmentRepository establishmentRepository, 
            UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _establishmentRepository = establishmentRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _modelErrors = new List<string>();
        }

        public async Task<Result<EstimateDto>> CreateEstimate(EstimateCreateDto dto)
        {
            var user = await GetCurrentUser();
            var estimate = new Estimate
            {
                TotalSum = dto.TotalSum,
                CreatedById = user.Id,
                EstablishmentId = dto.EstablishmentId, 
                CreatedAt = DateTime.Now
            };
            estimate = await _repository.CreateEstimate(estimate);
            bool productsCreated = false;
            bool servicesCreated = false;

            if(dto.Products.Count > 0) 
            {
                var products = await BuildEstimateProductList(dto.Products, estimate);
                productsCreated = await _repository.CreateEstimateProductList(products);
            }
            if(dto.Services.Count > 0) 
            {
                var services = BuildEstimateServiceList(dto.Services, estimate);
                servicesCreated = await _repository.CreateEstimateServiceList(services);
            }
                
            return Result<EstimateDto>.Success(BuildEstimateDto(dto, estimate.Id));
        }

        private async Task<List<EstimateProduct>> BuildEstimateProductList(ICollection<EstimateProductCreateDto> list, Estimate estimate) 
        {
            var products = new List<EstimateProduct>();
            foreach (var p in list)
            {
                var ep = await _establishmentRepository.GetEstablishmentProduct(p.ProductId, estimate.EstablishmentId);
                ep.Amount -= p.Amount;
                await _establishmentRepository.UpdateProduct(ep);
                products.Add(                                                                                                                                                                                                                                                                               
                    new EstimateProduct
                    {
                        Price = p.Price,
                        Amount = p.Amount,
                        TotalSum = p.TotalSum,
                        ProductId = p.ProductId,
                        EstimateId = estimate.Id
                    }
                );
            }
            return products;
        }

        private List<API.Models.EstimateService> BuildEstimateServiceList(ICollection<EstimateServiceCreateDto> list, Estimate estimate)
        {
            var services = new List<API.Models.EstimateService>();
            foreach (var s in list)
            {
                services.Add(
                    new API.Models.EstimateService
                    {
                        Price = s.Price,
                        Amount = s.Amount,
                        TotalSum = s.TotalSum,
                        ServiceId = s.ServiceId,
                        EstimateId = estimate.Id
                    }
                );
            }
            return services;
        }

        private EstimateDto BuildEstimateDto(EstimateCreateDto createDto, int estimateId)
        {
            return new EstimateDto
            {
                Id = estimateId,
                TotalSum = createDto.TotalSum,
                UserId = createDto.CreatedById,
                EstablishmentId = createDto.EstablishmentId,
            };
        }

        private async Task<AppUser> GetCurrentUser()
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
        }
    }
}  