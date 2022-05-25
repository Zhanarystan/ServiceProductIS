using System.Collections.Generic;

namespace API.DTOs
{
    public class UserDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public int? EstablishmentId { get; set; }
        public IList<string> Roles { get; set; }
    }
}