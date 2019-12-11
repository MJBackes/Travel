using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    [NotMapped]
    public class Hotel
    {
        public Geometry Geometry { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public OpeningHours OpeningHours{get;set;}
        public Photos[] Photos { get; set; }
        public string PlaceId { get; set; }
        public PlusCode PlusCode { get; set; }
        public int Rating { get; set; }
        public string Reference { get; set; }
        public string Scope { get; set; }
        public string[] Types { get; set; }
        public int TotalUserRatings { get; set; }
        public string Vicinity { get; set; }
    }
}