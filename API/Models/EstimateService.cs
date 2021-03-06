namespace API.Models
{
    public class EstimateService
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double TotalSum { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; }
    }
}