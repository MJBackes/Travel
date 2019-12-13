using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{

    public class YelpBusinessResponse
    {
        public int total { get; set; }
        public Business[] businesses { get; set; }
        public Region region { get; set; }
    }

    public class Region
    {
        public Center center { get; set; }
    }

    public class Center
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Business
    {
        public double rating { get; set; }
        public string price { get; set; }
        public string phone { get; set; }
        public string id { get; set; }
        public string alias { get; set; }
        public bool is_closed { get; set; }
        public Category[] categories { get; set; }
        public int review_count { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Coordinates coordinates { get; set; }
        public string image_url { get; set; }
        public YelpLocation location { get; set; }
        public float distance { get; set; }
        public string[] transactions { get; set; }
    }

    public class Coordinates
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class YelpLocation
    {
        public string city { get; set; }
        public string country { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string state { get; set; }
        public string address1 { get; set; }
        public string zip_code { get; set; }
    }

    public class Category
    {
        public string alias { get; set; }
        public string title { get; set; }
    }

}