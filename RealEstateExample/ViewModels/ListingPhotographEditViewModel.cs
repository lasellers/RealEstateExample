﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //SelectListItem
using RealEstateExample.Models;

namespace RealEstateExample.ViewModels
{
    public class ListingPhotographEditViewModel
    {
        public ListingPhotograph ListingPhotograph { get; set; }
        public Listing Listing { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }

        // listings for dopdown selection
        public List<Listing> Listings { get; set; }
        public IEnumerable<SelectListItem> SelectListListings { get; set; }
    }

}
