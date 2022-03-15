using System;
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
        public DateTime StartWorkingTime { get; set; }
        public DateTime EndWorkingTime { get; set; }
        public bool IsOpen { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}