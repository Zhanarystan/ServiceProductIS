namespace API.DTOs
{
    public class EstimateServiceDto 
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double TotalSum { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int EstimateId { get; set; }
    }
}