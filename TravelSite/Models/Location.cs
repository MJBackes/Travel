using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    [NotMapped]
    public class Location
    {
        public double? Lat { get; set; }
        public double? Long { get; set; }
    }
}