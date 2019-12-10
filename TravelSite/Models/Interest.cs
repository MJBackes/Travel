using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Interest
    {
        public Interest()
        {
            Travelers = new HashSet<Traveler>();
            Activities = new HashSet<Activity>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public bool isChecked { get; set; }
        public ICollection<Traveler> Travelers { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}