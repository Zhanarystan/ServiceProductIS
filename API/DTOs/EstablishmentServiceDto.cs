using API.Models;

namespace API.DTOs
{
    public class EstablishmentServiceDto
    {
        public int Id { get; set; }
        public int EstablishmentId { get; set; }
        public string EstablishmentName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string StartWorkingTime { get; set; }
        public string EndWorkingTime { get; set; }
        public bool IsOpen { get; set; }
        public string Address { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string Metric { get; set; }
        public double Distance { get; set; }
    }
}