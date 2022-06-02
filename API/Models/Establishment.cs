using System;
using System.Collections.Generic;
using API.Models;

namespace API.Models
{
    public class Establishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankCardNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string StartWorkingTime { get; set; }
        public string EndWorkingTime { get; set; }
        public bool IsOpen { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string PhotoPublicId { get; set; } 
        public string PhotoUrl { get; set; }     
        public ICollection<EstablishmentProduct> Products { get; set; } = new List<EstablishmentProduct>();
        public ICollection<EstablishmentService> Services { get; set; } = new List<EstablishmentService>();
    }
}