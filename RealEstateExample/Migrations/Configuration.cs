using RealEstateExample.Models;

namespace RealEstateExample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RealEstateExample.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RealEstateExample.Models.ApplicationDbContext context)
        {
            context.ListingScheduleTypes.AddOrUpdate(
           new ListingScheduleType() { Id = 1, Cost = 0, DiscountRate = 0 },
           new ListingScheduleType() { Id = 2, Cost = 100, DiscountRate = 10 },
           new ListingScheduleType() { Id = 3, Cost = 150, DiscountRate = 30 },
           new ListingScheduleType() { Id = 4, Cost = 2000, DiscountRate = 40 }
       );

            context.Listings.AddOrUpdate(
              //  p => p.Id,
new Listing() { Id = 1, Name = "House A" },
new Listing() { Id = 2, Name = "House B" },
new Listing() { Id = 3, Name = "House C" },
new Listing() { Id = 4, Name = "House D" }
);

            context.Realtors.AddOrUpdate(
               //   p => p.Id,
new Realtor() { Id = 1, Name = "Lewis Sellers", Description = "A Realtor", Phone = "555-555-5555", Address = "1313 MockingBird Lane"},
new Realtor() { Id = 2, Name = "John Locke", Description = "A Realtor 2", Phone = "555-555-5556", Address = "1314 MockingBird Lane" },
new Realtor() { Id = 3, Name = "Ricardo Jaffe", Description = "A Realtor 3", Phone = "555-555-5557", Address = "1315 MockingBird Lane" },
new Realtor() { Id = 4, Name = "Phillip Washington", Description = "A Realtor 4", Phone = "555-555-5558", Address = "1316 MockingBird Lane" }
);

        }
    }
}
