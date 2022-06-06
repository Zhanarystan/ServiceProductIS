using System;
using System.Collections.Generic;

namespace API.DTOs
{
    
    public class AppUserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string IIN { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? Password { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? EstablishmentId { get; set; }
        public string Establishment { get; set; }    
        public IList<string> Roles { get; set; }
    }
}