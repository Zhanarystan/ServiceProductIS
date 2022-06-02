using System.Collections.Generic;

namespace API.DTOs
{
    public class EstablishmentDto
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
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<EstablishmentProductDto> Products { get; set; } = new List<EstablishmentProductDto>();
        public ICollection<EstablishmentServiceDto> Services { get; set; } = new List<EstablishmentServiceDto>();
   }
}