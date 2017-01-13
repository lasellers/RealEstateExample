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

            routes.MapRoute(
                "Listings",
                "listings",
                new { controller = "Listings", action = "Index" }
                );

            routes.MapRoute(
              "Listing",
              "listing/{listingId}",
              new { controller = "Listings", action = "Details" },
              new { listingId = @"\d" }
              );

            routes.MapRoute(
                "Realtors",
                "realtors",
                new { controller = "Realtors", action = "Index" }
                );

            routes.MapRoute(
              "Realtor",
              "realtor/{realtorId}",
              new { controller = "Realtors", action = "Details" },
              new { realtorId = @"\d" }
              );

            routes.MapRoute(
          "ListingScheduleTypes",
          "listingScheduleTypes",
          new { controller = "ListingScheduleTypes", action = "Index" }
          );

            routes.MapRoute(
              "ListingScheduleType",
              "listingScheduleType/{typeId}",
              new { controller = "ListingScheduleTypes", action = "Details" },
              new { realtorId = @"\d" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
