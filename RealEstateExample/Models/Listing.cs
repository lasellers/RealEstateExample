using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using RealEstateExample.Models;

namespace RealEstateExample.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Listing
    {
        const int NameLength = 120;
        const int AddressLength = 220;
        const int PhoneLength = 132;

        public enum States
        {
            [Description("Not For Sales")]
            NotForSale = 0,
            [Description("Closing")]
            Closing,
            [Description("For Sales")]
            ForSale
        }

        public int Id { get; set; }

        [StringLength(NameLength)]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(AddressLength)]
        public string Address { get; set; }
        [StringLength(PhoneLength)]
        public string Phone { get; set; }

        [DisplayName("Latitude")]
        public float? Lat { get; set; }
        [DisplayName("Longitude")]
        public float? Lng { get; set; }

        public int? Cost { get; set; }

        [DisplayName("Build Year")]
        public short? BuildYear { get; set; }
        public DateTime? Created { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a status")]
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