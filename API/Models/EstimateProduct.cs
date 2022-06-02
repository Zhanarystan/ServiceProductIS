namespace API.Models
{
    public class EstimateProduct
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalSum { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; }
    }
}