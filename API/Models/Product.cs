using System.Collections.Generic;

namespace API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string KzName { get; set; }
        public string RuName { get; set; }
        public bool IsCustom { get; set; }
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public Metric Metric { get; set; }
        public int MetricId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ICollection<EstablishmentProduct> Establishments { get; set; } = new List<EstablishmentProduct>();
    }
}