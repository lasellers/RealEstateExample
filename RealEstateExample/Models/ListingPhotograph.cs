using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateExample.Models
{
    public class ListingPhotograph
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime? Created { get; set; }
        public String S3 { get; set; }
        public int? ListingId { get; set; }
      //  public int RealtorId { get; set; }
    }
}