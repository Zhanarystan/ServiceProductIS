namespace API.Models
{
    public class EstablishmentProduct
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}