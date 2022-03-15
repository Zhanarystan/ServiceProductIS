using System.Collections.Generic;

namespace API.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IataCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
    }
}