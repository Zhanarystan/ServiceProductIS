namespace API.DTOs
{
    public class EstimateProductDto 
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double TotalSum { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int EstimateId { get; set; }
    }
}