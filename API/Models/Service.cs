using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace API.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public Metric Metric { get; set; }
        public int MetricId { get; set; }
        public ICollection<EstablishmentService> Establishments { get; set; } = new List<EstablishmentService>();

        public string NormalizeName()
        {
            Regex rgx = new Regex("[^a-zA-Z0-9А-Яа-яәӘіІңҢғҒүҮұҰқҚөӨһҺ]");
            NormalizedName = rgx.Replace(Name, "").ToLower();
            return NormalizedName;
        }
    }
}