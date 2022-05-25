using System;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int? EstablishmentId { get; set; }
        public string IIN { get; set; }
        public DateTime Birthdate { get; set; }
    }
}