using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class UserListingDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Price { get; set; }
        [JsonIgnore]
        public string CreatorUsername { get; set; }
    }
}
