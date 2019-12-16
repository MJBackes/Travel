using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class ItineraryActivity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Itinerary")]
        public Guid ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }
        [ForeignKey("Activity")]
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}