namespace API.DTOs
{
    public class EstimateServiceCreateDto
    {
        public double Price { get; set; }
        public double Amount { get; set; }
        public double TotalSum { get; set; }
        public int ServiceId { get; set; }
    }
}