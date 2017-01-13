using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateExample.Models
{
    public class ListingScheduleType
    {
        public byte Id { get; set; }
        public float? Cost { get; set; }
        public int? DiscountRate { get; set; }
    }
}