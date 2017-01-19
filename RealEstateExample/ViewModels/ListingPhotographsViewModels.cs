using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //SelectListItem
using RealEstateExample.Models;

namespace RealEstateExample.ViewModels
{
    public class ListingPhotographsViewModel
    {
        public List<ListingPhotograph> ListingPhotographs { get; set; }

        public IEnumerable<SelectListItem> SelectListListingPhotographs { get; set; }
    }

}
