using System.Collections.Generic;

namespace API.DTOs
{
    public class EstablishmentListDto
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
   }
}