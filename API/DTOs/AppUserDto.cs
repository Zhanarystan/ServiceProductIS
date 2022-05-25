using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string IIN { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public IList<string> Roles { get; set; }
    }
}