using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateExample.Models
{
    public class Realtor
    {
        const int NameLength = 120;
        const int AddressLength = 220;
        const int PhoneLength = 132;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(NameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(AddressLength)]
        public string Phone { get; set; }
        [StringLength(PhoneLength)]
        public string Address { get; set; }

        public DateTime? Created { get; set; }
    }
}