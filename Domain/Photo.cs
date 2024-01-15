using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// This class performs a difinition to a table photo.
    /// </summary>
    public class Photo
    {
        public string Id { get; set; }
        public string Url{ get; set; }
        public bool IsMain{ get; set; }

    }
}
