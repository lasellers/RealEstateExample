using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RealEstateExample.Models
{
    public class Listing
    {
        public enum States
        {
            NotForSale,
            Closing,
            ForSale
        }

        public int Id { get; set; }

        [StringLength(120)]
        public String Name { get; set; }
        public String Description { get; set; }
        public String Address { get; set; }
        [StringLength(132)]
        public String Phone { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }
        public int? Cost { get; set; }
        public short? BuildYear { get; set; }
        public DateTime? Created { get; set; }

        [Required]
        public int RealtorId { get; set; }
        public States? State { get; set; }
        public ListingScheduleType ListingScheduleType { get; set; }
        public byte? ListingScheduleTypeId { get; set; } /* convention: ListingScheduleType.id foreign key */
        // public Realtors Realtor { get; set; }

    }
}