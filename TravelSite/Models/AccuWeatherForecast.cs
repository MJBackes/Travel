using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{

    public class AccuWeatherForecast
    {
        public Headline Headline { get; set; }
        public Dailyforecast[] DailyForecasts { get; set; }
    }

    public class Headline
    {
        public DateTime EffectiveDate { get; set; }
        public double EffectiveEpochDate { get; set; }
        public double Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public object EndDate { get; set; }
        public object EndEpochDate { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Dailyforecast
    {
        public DateTime Date { get; set; }
        public double EpochDate { get; set; }
        public Temperature Temperature { get; set; }
        public Day Day { get; set; }
        public Night Night { get; set; }
        public string[] Sources { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Temperature
    {
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }

    public class Minimum
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public double UnitType { get; set; }
    }

    public class Maximum
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public double UnitType { get; set; }
    }

    public class Day
    {
        public double Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
    }

    public class Night
    {
        public double Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
    }

}