﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ListingVisitors
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ListingId { get; set; }
        public Listing Listing { get; set; }
        public bool IsCreator { get; set; }
    }

}
