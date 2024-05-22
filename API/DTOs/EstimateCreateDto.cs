using System;
using System.Collections.Generic;
using API.Models;

namespace API.DTOs
{
    public class EstimateCreateDto
    {
        public double TotalSum { get; set; }
        public string CreatedById { get; set; }
        public int EstablishmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<EstimateProductCreateDto> Products { get; set; } = new List<EstimateProductCreateDto>();
        public ICollection<EstimateServiceCreateDto> Services { get; set; } = new List<EstimateServiceCreateDto>();
    }
}