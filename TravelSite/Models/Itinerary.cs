using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Travelers = new HashSet<Traveler>();
        }
        [Key]
        public Guid Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public TimeSpan TimeSpan { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [DisplayName("START DATE")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [DisplayName("END DATE")]
        public DateTime EndDate { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Traveler> Travelers { get; set; }
        public string HotelLocationString { get; set; }
    }
}