using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
    }
}