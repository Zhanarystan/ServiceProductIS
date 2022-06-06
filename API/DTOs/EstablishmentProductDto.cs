using API.Models;

namespace API.DTOs
{
    public class EstablishmentProductDto
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
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Metric { get; set; }
        public double Distance { get; set; }
        public string ProductBarCode { get; set; }
    }
}