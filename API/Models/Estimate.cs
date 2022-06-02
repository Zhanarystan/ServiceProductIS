namespace API.Models
{
    public class Estimate
    {
        public int Id { get; set; }
        public double TotalSum { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public AppUser CreatedBy { get; set; }
        public ICollection<EstimateProduct> Products { get; set; } = new List<EstimateProduct>();
        public ICollection<EstimateService> Services { get; set; } = new List<EstimateService>();
    }
}