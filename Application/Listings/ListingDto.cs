using Application.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Listings
{
    public class ListingDto
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public int Price { get; set; }
        public int Area { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string CreatorUsername { get; set; }
        public bool IsCancelled { get; set; }
        public ICollection<VisitorDto> Visitors { get; set; }
    }
}
