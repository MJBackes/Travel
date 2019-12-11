using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    [NotMapped]
    public class Photos
    {
        public int Height { get; set; }
        public string[] HTMLAttributions { get; set; }
        public string PhotoReference { get; set; }
        public int Width { get; set; }
    }
}