using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// This class performs a difinition to a table Listnig.
    /// </summary>
    public class Listing
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public int Price { get; set; }
        public int Area { get; set; }
        public DateTime DateTime { get; set; }

    }
}
