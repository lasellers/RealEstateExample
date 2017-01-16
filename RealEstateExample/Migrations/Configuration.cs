using System.Data.Entity.Validation;
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

        // update-database -targetMigration:0 -force
        // update-database -verbose -force
        protected override void Seed(RealEstateExample.Models.ApplicationDbContext context)
        {

            try
            {

                context.ListingScheduleTypes.AddOrUpdate(
                    new ListingScheduleType() { Id = 1, Cost = 0, DiscountRate = 0 },
                    new ListingScheduleType() { Id = 2, Cost = 100, DiscountRate = 10 },
                    new ListingScheduleType() { Id = 3, Cost = 150, DiscountRate = 30 },
                    new ListingScheduleType() { Id = 4, Cost = 2000, DiscountRate = 40 }
                );


                context.Realtors.AddOrUpdate(
                    //   p => p.Id,
                    new Realtor()
                    {
                        Id = 1,
                        Name = "Lewis Sellers",
                        Description = "A Realtor",
                        Phone = "555-555-5555",
                        Address = "1313 MockingBird Lane"
                    },
                    new Realtor()
                    {
                        Id = 2,
                        Name = "John Locke",
                        Description = "A Realtor 2",
                        Phone = "555-555-5556",
                        Address = "1314 MockingBird Lane"
                    },
                    new Realtor()
                    {
                        Id = 3,
                        Name = "Ricardo Jaffe",
                        Description = "A Realtor 3",
                        Phone = "555-555-5557",
                        Address = "1315 MockingBird Lane"
                    },
                    new Realtor()
                    {
                        Id = 4,
                        Name = "Phillip Washington",
                        Description = "A Realtor 4",
                        Phone = "555-555-5558",
                        Address = "1316 MockingBird Lane"
                    }
                );



                context.Listings.AddOrUpdate(
                    //  p => p.Id,
                    new Listing()
                    {
                        Id = 1,
                        Name = "House A",
                        RealtorId = 1,
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus sagittis porttitor neque, id condimentum lectus pretium sit amet. Mauris a urna ante. Ut cursus nunc tellus, eget malesuada orci mollis nec. Suspendisse in sapien elit. Praesent convallis ligula nunc, eu pellentesque odio semper sit amet. Suspendisse potenti.",
                        BuildYear = (short)1900,
                        Lat = 30.00f,
                        Lng = 80.00f,
                        Cost = 50000
                    },
                    new Listing()
                    {
                        Id = 2,
                        Name = "House B",
                        RealtorId = 1,
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus sagittis porttitor neque, id condimentum lectus pretium sit amet. Mauris a urna ante. Ut cursus nunc tellus, eget malesuada orci mollis nec. Suspendisse in sapien elit. Praesent convallis ligula nunc, eu pellentesque odio semper sit amet. Suspendisse potenti.",
                        BuildYear = (short)1967,
                        Lat = 30.00f,
                        Lng = 80.00f,
                        Cost = 50000
                    },
                    new Listing()
                    {
                        Id = 3,
                        Name = "House C",
                        RealtorId = 1,
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus sagittis porttitor neque, id condimentum lectus pretium sit amet. Mauris a urna ante. Ut cursus nunc tellus, eget malesuada orci mollis nec. Suspendisse in sapien elit. Praesent convallis ligula nunc, eu pellentesque odio semper sit amet. Suspendisse potenti.",
                        BuildYear = (short)1940,
                        Lat = 30.00f,
                        Lng = 80.00f,
                        Cost = 50000
                    },
                    new Listing()
                    {
                        Id = 4,
                        Name = "House D",
                        RealtorId = 2,
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus sagittis porttitor neque, id condimentum lectus pretium sit amet. Mauris a urna ante. Ut cursus nunc tellus, eget malesuada orci mollis nec. Suspendisse in sapien elit. Praesent convallis ligula nunc, eu pellentesque odio semper sit amet. Suspendisse potenti.",
                        BuildYear = (short)2009,
                        Lat = 30.00f,
                        Lng = 80.00f,
                        Cost = 50000
                    },
                    new Listing()
                    {
                        Id = 5,
                        Name = "House Delete Me",
                        RealtorId = 1,
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus sagittis porttitor neque, id condimentum lectus pretium sit amet. Mauris a urna ante. Ut cursus nunc tellus, eget malesuada orci mollis nec. Suspendisse in sapien elit. Praesent convallis ligula nunc, eu pellentesque odio semper sit amet. Suspendisse potenti.",
                        BuildYear = (short)2017,
                        Lat = 30.00f,
                        Lng = 80.00f,
                        Cost = 50000
                    }
                );


                //
                context.ListingPhotographs.AddOrUpdate(
        //   p => p.Id,
        new ListingPhotograph()
        {
            Id = 1,
            Name = "Pic 1",
            Description = "pic a 1",
            ListingId = 1,
            Created = Convert.ToDateTime("2009/2/2")
        },
        new ListingPhotograph()
        {
            Id = 2,
            Name = "Pic 2",
            Description = "pic a 2",
            ListingId = 1,
            Created = Convert.ToDateTime("2011/2/2")
        },
        new ListingPhotograph()
        {
            Id = 3,
            Name = "Pic 3",
            Description = "pic a 2",
            ListingId = 1,
            Created = Convert.ToDateTime("2012/2/2")
        },
        new ListingPhotograph()
        {
            Id = 4,
            Name = "Pic 4",
            Description = "pic a 2",
            ListingId = 1,
            Created = Convert.ToDateTime("2015/2/2")
        }

        );

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

    }
}

//  Created = Convert.ToDateTime("2009/2/2")
