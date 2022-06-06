using System;
using System.Collections.Generic;
using API.DTOs;

namespace API.DTOs
{
    public class EstimateDto
    {
        public int Id { get; set; }
        public double TotalSum { get; set; }
        public string UserId { get; set; }
        public int EstablishmentId { get; set; }
        public ICollection<EstimateProductDto> Products { get; set; } = new List<EstimateProductDto>();
        public ICollection<EstimateServiceDto> Services { get; set; } = new List<EstimateServiceDto>();
    }
}