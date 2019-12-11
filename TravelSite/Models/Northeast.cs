using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    [NotMapped]
    public class Northeast
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}