using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateExample.Models;

namespace RealEstateExample.ViewModels
{
    public class ListingsByRealtorViewModel
    {
        public Realtor Realtor { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
