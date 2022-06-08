namespace API.DTOs
{
    public class EstimateProductCreateDto
    {
        public double Price { get; set; }
        public double Amount { get; set; }
        public double TotalSum { get; set; }
        public int ProductId { get; set; }
        public string Metric { get; set; }
    }
}