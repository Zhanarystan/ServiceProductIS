namespace API.Models
{
    public class EstimateService
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalSum { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; }
    }
}