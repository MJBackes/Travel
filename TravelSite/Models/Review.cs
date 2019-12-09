using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        public double? Rating { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Traveller")]
        public Guid TravellerId { get; set;}
        public Traveller Traveller { get; set; }
        [ForeignKey("Activity")]
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}