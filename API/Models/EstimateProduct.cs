namespace API.Models
{
    public class EstimateProduct
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double TotalSum { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; }
    }
}