using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public static async void SeedData(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");
            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);
            }
            

            if (!userManager.Users.Any() && !context.Listings.Any())
            {
                
                    var adminUser = new AppUser
                    {
                        DisplayName = "Admin",
                        UserName = "admin@test.com",
                        Email = "admin@test.com",
                        Bio = "admin test"
                    };
                    var result = await userManager.CreateAsync(adminUser, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                
                var users = new List<AppUser>
                {
                    new AppUser 
                    { 
                        DisplayName = "Amir",
                        UserName = "amir",
                        Email = "amir@test.com",
                        Bio="this is a test" 
                    },
                    new AppUser 
                    { 
                        DisplayName = "Sandy",
                        UserName = "sandy",
                        Email = "sandy@test.com",
                        Bio="this is a test"
                    },
                    new AppUser 
                    { 
                        DisplayName = "Sam",
                        UserName = "sam",
                        Email = "sam@test.com",
                        Bio = "this is a test" 
                    },
                    new AppUser 
                    {
                        DisplayName = "Shani",
                        UserName = "shani",
                        Email = "shani@test.com",
                        Bio="this is a test" 
                    }
                };
                foreach(var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(user, userRole.Name);

                }

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
                        DateTime = DateTime.Now.AddDays(126),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[0],
                                IsCreator = true
                            }
                        }
 
                    },
                    new Listing
                    {

                        Address = "stela maris 12",
                        City = "Haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 1500000,
                        Area = 90,
                        DateTime = DateTime.Now.AddDays(156),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = true
                            }, 
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "denmark 18",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price =5000000,
                        Area = 150,
                        DateTime = DateTime.Now.AddDays(256),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[2],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "yaffo 84",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 500000,
                        Area = 50,
                        DateTime = DateTime.Now.AddDays(56),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[0],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[2],
                                IsCreator = false
                            },
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "galil 27",
                        City = "haifa",
                        Region = "North",
                        PostalCode = "3304895",
                        Price = 750000,
                        Area = 85,
                        DateTime = DateTime.Now.AddDays(16),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[0],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "yafenof 65",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "talmana 28",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[2],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "hashalom 30",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[2],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "benyhoda 11",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "tsalbanim 38",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "khory 48",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "kesaria 59",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[2],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "yodlamedperets 94",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "haneviem 4",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[2],
                                IsCreator = false
                            }
                        }
                    },
                    new Listing
                    {
                        Address = "yadlebanem 188",
                        City = "haifa",
                       Region = "North",
                        PostalCode = "3304895",
                        Price = 3000000,
                        Area = 75,
                        DateTime = DateTime.Now.AddDays(186),
                        Description="This is a good listing and have a lot of value",
                        Visitors = new List<ListingVisitors>
                        {
                            new ListingVisitors
                            {
                                AppUser = users[1],
                                IsCreator = true
                            },
                            new ListingVisitors
                            {
                                AppUser = users[3],
                                IsCreator = false
                            }
                        }
                    }
                };
                await context.Listings.AddRangeAsync(propertys);
                await context.SaveChangesAsync();
            }
        }
    }
}
