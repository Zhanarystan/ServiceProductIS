namespace API.DTOs
{
    public class EstablishmentProductCreateDto
    {
        public int ProductId { get; set; }
        public int EstablishmentId { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
    }
}