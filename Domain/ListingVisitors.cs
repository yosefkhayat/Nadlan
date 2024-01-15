using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// This class performs a difinition to a joint table to Listing and visitor.
    /// </summary>
    public class ListingVisitors
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ListingId { get; set; }
        public Listing Listing { get; set; }
        public bool IsCreator { get; set; }
    }

}
