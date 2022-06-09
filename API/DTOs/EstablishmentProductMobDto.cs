namespace API.DTOs
{
    public class EstablishmentProductMobDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; } 
        public bool IsPresent { get; set; }
        public int EstablishmentId { get; set; }
        public int ProductId { get; set; }
        
    }
}