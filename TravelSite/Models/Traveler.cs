using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Traveler
    {
        public Traveler()
        {
            Interests = new HashSet<Interest>();
            Itineraries = new HashSet<Itinerary>();
        }
        [Key]
        public Guid Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser {get;set;}
        [DisplayName("Interests")]
        public ICollection<Interest> Interests { get; set; }
        [DisplayName("Itineraries")]
        public ICollection<Itinerary> Itineraries { get; set; }

    }
}