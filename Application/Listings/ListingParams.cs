using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Listings
{
    public class ListingParams : PagingParams
    {
        public bool IsVisiting { get; set; }
        public bool IsCreator { get; set; }
    }
}
