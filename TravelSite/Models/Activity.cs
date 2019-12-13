using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Activity
    {
        public Activity()
        {
            Itineraries = new HashSet<Itinerary>();
        }
        [Key]
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        ICollection<Itinerary> Itineraries { get; set; }
    }
}