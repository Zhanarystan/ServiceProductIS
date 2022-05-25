using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;

namespace API.Services
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IEstablishmentRepository _repository;
        public EstablishmentService(IEstablishmentRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<EstablishmentListDto>> GetEstablishments() 
        {
            return await _repository.GetEstablishments();
        }

        public async Task<EstablishmentDto> GetEstablishment(int id)
        {
            return await _repository.GetEstablishment(id);
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
        
        public async Task<IEnumerable<EstablishmentProductDto>> GetEstablishmentsByProduct(int productId, double userLat, double userLon)
        {
            var establishments = await _repository.GetEstablishmentsByProduct(productId);
            List<EstablishmentProductDto> establishmentsList = new List<EstablishmentProductDto>();
            foreach(var es in establishments)
            {
                es.Distance = DistanceTo(es.Latitude, es.Longitude, userLat, userLon);
                establishmentsList.Add(es);
            }

            return establishmentsList.OrderBy(es => es.Distance).ToList();
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