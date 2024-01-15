using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// This class performs a difinition to a table comment.
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public AppUser Author { get; set; }
        public Listing Listing { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
