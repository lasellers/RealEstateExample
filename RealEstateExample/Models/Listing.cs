using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstateExample.Models
{
    public class Listing
    {
        public enum States
        {
            NotForSale=0,
            Closing,
            ForSale
        }

        public int Id { get; set; }

        [StringLength(120)]
        public String Name { get; set; }
        public String Description { get; set; }
        [StringLength(220)]
        public String Address { get; set; }
        [StringLength(132)]
        public String Phone { get; set; }

        [DisplayName("Latitude")]
        public float? Lat { get; set; }
        [DisplayName("Longitude")]
        public float? Lng { get; set; }

        public int? Cost { get; set; }

        [DisplayName("Build Year")]
        public short? BuildYear { get; set; }
        public DateTime? Created { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a state")]
        public States? State { get; set; }

        [Required]
        [DisplayName("Realtor #")]
        public int RealtorId { get; set; } /* convention: Realtor.id foreign key */
        //public Realtor Realtor { get; set; }

        [Required]
        [DisplayName("Listing Schedule Type")]
        public byte ListingScheduleTypeId { get; set; } /* convention: ListingScheduleType.id foreign key */
       // public ListingScheduleType ListingScheduleType { get; set; }

    }
}