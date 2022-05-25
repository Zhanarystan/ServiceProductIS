namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string KzName { get; set; }
        public string RuName { get; set; }
        public bool IsCustom { get; set; }
        public string Description { get; set; }
        public string Metric { get; set; }
        public string Manufacturer { get; set; }
    }
}