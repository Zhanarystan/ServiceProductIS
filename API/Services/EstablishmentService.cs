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
namespace API.Services
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IEstablishmentRepository _repository;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IList<string> _modelErrors;
        public EstablishmentService(IEstablishmentRepository repository, IPhotoAccessor photoAccessor)
        {
            _repository = repository;
            _photoAccessor = photoAccessor;
            _modelErrors = new List<string>();
        }
        public async Task<IEnumerable<EstablishmentListDto>> GetEstablishments() 
        {
            return await _repository.GetEstablishments();
        }
        public async Task<EstablishmentDto> GetEstablishment(int id)
        {
            return await _repository.GetEstablishmentDto(id);
        }
        public async Task<bool> CreateEstablishment(EstablishmentCreateDto establishment)
        {
            var newEstablishment = new Establishment
            {
                Name = establishment.Name,
                BankCardNumber = establishment.BankCardNumber,
                Longitude = establishment.Longitude,
                Latitude = establishment.Latitude,
                StartWorkingTime = establishment.StartWorkingTime,
                EndWorkingTime = establishment.EndWorkingTime,
                Address = establishment.Address,
                IsOpen = false,
                CityId = establishment.CityId
            };
            return await _repository.CreateEstablishment(newEstablishment);
        }
        public async Task<Result<IEnumerable<EstablishmentProductDto>>> GetEstablishmentsByProduct(int productId, double userLat, double userLon)
        {
            var establishments = await _repository.GetEstablishmentsByProduct(productId);
            List<EstablishmentProductDto> establishmentsList = new List<EstablishmentProductDto>();
            foreach(var es in establishments)
            {
                es.Distance = DistanceTo(es.Latitude, es.Longitude, userLat, userLon);
                establishmentsList.Add(es);
            }
            return Result<IEnumerable<EstablishmentProductDto>>.Success(establishmentsList.OrderBy(es => es.Distance).ToList());
        }
        public async Task<Result<IEnumerable<EstablishmentServiceDto>>> GetEstablishmentsByService(int serviceId, double userLat, double userLon)
        {
            var establishments = await _repository.GetEstablishmentsByService(serviceId);
            List<EstablishmentServiceDto> establishmentsList = new List<EstablishmentServiceDto>();
            foreach(var es in establishments)
            {
                es.Distance = DistanceTo(es.Latitude, es.Longitude, userLat, userLon);
                establishmentsList.Add(es);
            }
            return Result<IEnumerable<EstablishmentServiceDto>>.Success(establishmentsList.OrderBy(es => es.Distance).ToList());
        }
        public async Task<Result<EstablishmentProductDto>> UpdateProduct(EstablishmentProductDto productDto)
        {
            var product = await _repository.GetEstablishmentProduct(productDto.ProductId, productDto.EstablishmentId);
            product.Price = productDto.Price;
            product.Amount = productDto.Amount;
            if(! await _repository.UpdateProduct(product))
                return Result<EstablishmentProductDto>.Failure(new List<string>() {"Продукт по id: " + productDto.ProductId + " не обновлено!"});
            return Result<EstablishmentProductDto>.Success(productDto);
        }
        public async Task<Result<EstablishmentDto>> UpdatePhoto(int establishmentId, IFormFile file)
        {
            var establishment = await _repository.GetEstablishment(establishmentId);
            string deletionConfirmation = "ok";
            if(establishment.PhotoPublicId != null || establishment.PhotoUrl != null )
                deletionConfirmation = await _photoAccessor.DeletePhoto(establishment.PhotoPublicId);
            if(deletionConfirmation == null)
                return Result<EstablishmentDto>.Failure(new List<string>() {"Фото заведении не изменено!"});
            var photoUploadResult = await _photoAccessor.AddPhoto(file);
            establishment.PhotoPublicId = photoUploadResult.PublicId;
            establishment.PhotoUrl = photoUploadResult.Url;
            if(! await _repository.UpdateEstablishment(establishment))
                return Result<EstablishmentDto>.Failure(new List<string>() {"Фото заведении не изменено!"});
            var establishmentDto = await _repository.GetEstablishmentDto(establishmentId);
            establishmentDto.PhotoUrl = establishment.PhotoUrl;
            return Result<EstablishmentDto>.Success(establishmentDto);
        }
        public async Task<Result<EstablishmentCreateDto>> UpdateEstablishment(int id, EstablishmentCreateDto dto)
        {
            var establishment = await _repository.GetEstablishment(id);
            if(!ValidateEstablishment(dto))
                return Result<EstablishmentCreateDto>.Failure(_modelErrors);
            establishment.Name = dto.Name;
            establishment.BankCardNumber = dto.BankCardNumber;
            establishment.Longitude = dto.Longitude;
            establishment.Latitude = dto.Latitude;
            establishment.StartWorkingTime = dto.StartWorkingTime;
            establishment.EndWorkingTime = dto.EndWorkingTime;
            establishment.Address = dto.Address;
            if(! await _repository.UpdateEstablishment(establishment))
                return Result<EstablishmentCreateDto>.Failure(new List<string>() {"Заведение не изменено!"});
            return Result<EstablishmentCreateDto>.Success(dto);
        }
        public async Task<Result<int>> CreateEstablishmentProductList(List<EstablishmentProductCreateDto> list)
        {
            return Result<int>.Success(1);
        }
        public bool ValidateEstablishment(EstablishmentCreateDto dto) 
        {
            if(String.IsNullOrEmpty(dto.Name))
                _modelErrors.Add("Наименование заведении не должно быть пустым");
            if(String.IsNullOrEmpty(dto.BankCardNumber))
                _modelErrors.Add("Номер банковской карты должно быть пустым");
            if(dto.BankCardNumber.Length != 16 || !dto.BankCardNumber.All(Char.IsDigit))
                _modelErrors.Add("Номер банковской карты должeн содержать 16 цифр");
            return _modelErrors.Count == 0;
        }
        private double DistanceTo(double lat1, double lon1, double lat2, double lon2) 
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist = 
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * 
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            dist = dist * 1609.34;
            return Math.Round(dist, 2);
        }
    }
}