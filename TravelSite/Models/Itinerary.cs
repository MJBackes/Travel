using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Itinerary
    {
        public Itinerary()
        {
            Activities = new HashSet<Activity>();
            Travellers = new HashSet<Traveller>();
        }
        [Key]
        public Guid Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Traveller> Travellers { get; set; }
    }
}