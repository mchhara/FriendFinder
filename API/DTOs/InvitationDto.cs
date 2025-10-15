using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class InvitationDto
    {
        public int Id { get; set; }
        [JsonPropertyName("userName")]
        public string Username { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public string PhotoUrl { get; set; }
        public string City { get; set; } 

    }
}