using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateExample.Models;

namespace RealEstateExample.ViewModels
{
    public class ListingEditViewModel
    {
        public Listing Listing { get; set; }
       // public Listing.States State { get; set; }
        public ListingScheduleType ListingScheduleType { get; set; }
        public List<Realtor> Realtors { get; set; }
    }

}
