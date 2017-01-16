using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateExample.Models
{
    public class ListingScheduleType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        public float? Cost { get; set; }
        [DisplayName("Discount Rate")]
        public int? DiscountRate { get; set; }
    }
}