using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// This class performs a seeding data to the Database 
    /// </summary>
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.Listings.Any())
            {
                var propertys = new List<Listing>
                {
                    new Listing
                    {
                        Address = "alnbe 127",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 1000000,
                        Area = 110,
                        DateTime = DateTime.Now.AddDays(126)
 
                    },
                    new Listing
                    {

                        Address = "stela maris 12",
                        City = "Haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 1500000,
                        Area = 90,
                        DateTime = DateTime.Now.AddDays(156)
                    },
                    new Listing
                    {
                        Address = "denmark 18",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price =5000000,
                        Area = 150,
                        DateTime = DateTime.Now.AddDays(256)
                        
                    },
                    new Listing
                    {
                        Address = "yaffo 84",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 500000,
                        Area = 50,
                        DateTime = DateTime.Now.AddDays(56)
                    },
                    new Listing
                    {
                        Address = "galil 127",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 750000,
                        Area = 85,
                        DateTime = DateTime.Now.AddDays(16) 
                    },
                    new Listing
                    {
                        Address = "alnbe 127",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186) 
                    }
                };
                context.Listings.AddRangeAsync(propertys);
                context.SaveChangesAsync();
            }
        }
    }
}
