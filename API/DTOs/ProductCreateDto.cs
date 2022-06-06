namespace API.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public bool IsCustom { get; set; }
        public int ManufacturerId { get; set; }
        public int MetricId { get; set; }
    }
}
