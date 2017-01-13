using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RealEstateExample.Models
{
    public class Realtor
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public DateTime? Created { get; set; }
    }
}