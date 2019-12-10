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
            Interests = new HashSet<Interest>();
        }
        [Key]
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public ICollection<Interest> Interests { get; set; }

    }
}