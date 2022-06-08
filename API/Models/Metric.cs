using System.Collections.Generic;

namespace API.Models
{
    public class Metric
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}