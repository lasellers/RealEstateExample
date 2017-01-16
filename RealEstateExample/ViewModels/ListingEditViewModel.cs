using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //SelectListItem
using RealEstateExample.Models;

namespace RealEstateExample.ViewModels
{
    public class ListingEditViewModel
    {
        public Listing Listing { get; set; }

        public List<Realtor> Realtors { get; set; }
        public List<ListingScheduleType> ListingScheduleTypes { get; set; }
        public List<ListingPhotograph> ListingPhotographs { get; set; }

        public IEnumerable<SelectListItem> SelectListRealtors { get; set; }
        public IEnumerable<SelectListItem> SelectListListingScheduleTypes { get; set; }
        public IEnumerable<SelectListItem> SelectListListingPhotographs { get; set; }

        // public Listing.States State { get; set; }
        //public ListingScheduleType ListingScheduleType { get; set; }
    }

}
