using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// This class performs a definition to the database.
    /// </summary>
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }   
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingVisitors> ListingVisters { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ListingVisitors>(x => x.HasKey(aa => new { aa.AppUserId, aa.ListingId }));

            builder.Entity<ListingVisitors>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.Listings)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ListingVisitors>()
                .HasOne(u => u.Listing)
                .WithMany(a => a.Visitors)
                .HasForeignKey(aa => aa.ListingId);



        }
    }
}
