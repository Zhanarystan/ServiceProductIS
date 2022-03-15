namespace API.Models
{
    public class EstablishmentService
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public int ServiceId { get; set; }
        public Service Product { get; set; }
    }
}