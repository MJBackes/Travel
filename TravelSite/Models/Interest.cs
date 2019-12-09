using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}