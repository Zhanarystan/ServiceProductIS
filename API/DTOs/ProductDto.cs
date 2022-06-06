namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public string Description { get; set; }
        public string Metric { get; set; }
        public int MetricId { get; set; }
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }
    }
}