namespace API.Models
{
    public class EstablishmentProduct
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; } 
        public bool IsPresent { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}