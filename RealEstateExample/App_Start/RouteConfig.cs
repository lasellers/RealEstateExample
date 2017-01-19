using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealEstateExample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //
            routes.MapRoute(
                "Listings",
                "Listings",
                new { controller = "Listings", action = "Index" }
                );

            routes.MapRoute(
              "Listing",
              "Listing/{listingId}",
              new { controller = "Listings", action = "Details" },
              new { listingId = @"\d" }
              );

            //
            routes.MapRoute(
                "Realtors",
                "Realtors",
                new { controller = "Realtors", action = "Index" }
                );

            routes.MapRoute(
              "Realtor",
              "Realtor/{realtorId}",
              new { controller = "Realtors", action = "Details" },
              new { realtorId = @"\d" }
              );

            //
            routes.MapRoute(
             "ListingScheduleTypes",
             "ListingScheduleTypes",
             new { controller = "ListingScheduleTypes", action = "Index" }
            );

            routes.MapRoute(
              "ListingScheduleType",
              "ListingScheduleType/{typeId}",
              new { controller = "ListingScheduleTypes", action = "Details" },
              new { realtorId = @"\d" }
              );

            //
            routes.MapRoute(
              "ListingPhotographs",
              "ListingPhotographs",
              new { controller = "ListingPhotographs", action = "Index" }
              );

            routes.MapRoute(
              "ListingPhotograph",
              "ListingPhotograph/{photographId}",
              new { controller = "ListingPhotographs", action = "Details" },
              new { photographId = @"\d" }
              );

            //
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
