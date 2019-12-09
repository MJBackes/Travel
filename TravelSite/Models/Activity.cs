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
        [Key]
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        [ForeignKey("Interests")]
        public int? InterestId { get; set; }
        public List<Interest> Interests { get; set; }
    }
}