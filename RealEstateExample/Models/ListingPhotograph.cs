using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateExample.Models
{
    public class ListingPhotograph
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime? Created { get; set; }

        [DisplayName("Listing #")]
        public int? ListingId { get; set; }
    }
}