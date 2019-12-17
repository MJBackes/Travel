using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class ItineraryController : Controller
    {
        ApplicationDbContext db;
        private const int MAXFORECAST = 5;
        public ItineraryController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Itinerary
        public ActionResult Index()
        {
            return View();
        }

        // GET: Itinerary/Details/5
        public ActionResult Itinerary(Guid? id)
        {
            ViewBag.Activities = db.ItineraryActivities.Include("Activity").Where(i => i.ItineraryId == id).ToList();
            return View(db.Itineraries.FirstOrDefault(i => i.Id == id));
        }

        [HttpGet]
        public ActionResult GetActivities(Guid? id)
        {
            var userId = User.Identity.GetUserId();
            Traveler traveler = db.Travelers.Include("Interests").Include("CurrentItinerary").FirstOrDefault(t => t.ApplicationUserId == userId);
            if (id != null && id != traveler.CurrentItineraryID)
            {
                traveler.CurrentItineraryID = id;
                traveler.CurrentItinerary = db.Itineraries.Find(id);
            }
            if((traveler.CurrentItinerary.StartDate - DateTime.Today).TotalDays < MAXFORECAST)
            {
                string state = traveler.CurrentItinerary.City.Split(',')[1].Trim().Split(',')[0];
                string city = traveler.CurrentItinerary.City.Split(',')[0];
                List<AccuWeatherLocationResponse> responses = AccuWeatherAPIHandler.GetLocation(state, city).Result;
                if (responses.Count > 0)
                {
                    string key = responses[0].Key;
                    AccuWeatherForecast forecast = AccuWeatherAPIHandler.GetForecast(key).Result;
                    if (forecast.Headline.Text.ToLower().Contains("rain") || forecast.Headline.Text.ToLower().Contains("snow") || GetAverageTemp(forecast) < 40)
                    {
                        traveler.Interests.Remove(db.Interests.FirstOrDefault(i => i.Name == "Park"));
                    }
                    ViewBag.AverageTemp = GetAverageTemp(forecast);
                }
            }
            ViewBag.Activities = GetMatchingActivities(traveler);
            ViewBag.Popular = GetTopN(ViewBag.Activities, 5).ToArray();
            ViewBag.Activities = ViewBag.Activities.ToArray();
            ViewBag.HotelLat = decimal.Parse(traveler.CurrentItinerary.HotelLocationString.Split(',')[0]);
            ViewBag.HotelLng = decimal.Parse(traveler.CurrentItinerary.HotelLocationString.Split(',')[1]);
            return View();
        }
        private double GetAverageTemp(AccuWeatherForecast forecast)
        {
            double output = 0;
            foreach(Dailyforecast daily in forecast.DailyForecasts)
            {
                output += GetDailyAverage(daily);
            }
            return output / forecast.DailyForecasts.Length;
        }
        private double GetDailyAverage(Dailyforecast dailyforecast)
        {
            return (dailyforecast.Temperature.Minimum.Value + dailyforecast.Temperature.Maximum.Value) / 2;
        }
        private List<Business> GetMatchingActivities(Traveler traveler)
        {
            List<Business> output = new List<Business>();
            for (int i = 0; i < traveler.Interests.Count; i++)
            {
                List<Business> businesses = YelpAPIHandler.GetActivities(traveler.CurrentItinerary.HotelLocationString, 36000, traveler.Interests.ToList()[i].Value, "").Result.businesses.ToList();
                foreach (Business b in businesses)
                    output.Add(b);
            }
            return output;
        }
        private List<Business> GetTopN(List<Business> businesses, int N)
        {
            if (businesses.Count <= N)
                return businesses;
            List<Business> output = new List<Business>();
            //businesses = businesses.OrderByDescending(b => b.review_count).ToList();
            //for (int i = 0; i < 5; i++) {
            //    output.Add(businesses[i]);
            //    }
            return businesses.OrderByDescending(b => b.review_count).Take(N).ToList();
        }
        [HttpPost]
        public ActionResult GetActivities(Activity activity)
        {
            Activity DBActivity = db.Activities.FirstOrDefault(a => a.Name == activity.Name && ((a.Address == activity.Address) || (a.Lat == activity.Lat && a.Lng == activity.Lng)));
            if (DBActivity == null)
            {
                activity.Id = Guid.NewGuid();
                db.Activities.Add(activity);
                db.SaveChanges();
                DBActivity = db.Activities.Find(activity.Id);
                DBActivity = activity;
            }
            TempData["Activity"] = DBActivity;
            TempData["Reviews"] = db.Reviews.Include("Traveler").Where(r => r.ActivityId == DBActivity.Id).ToList();
            return RedirectToAction("AddActivity");
        }
        //POST: AddActivity
        [HttpGet]
        public ActionResult AddActivity()
        {
            ViewBag.Activity = TempData["Activity"];
            ViewBag.Reviews = TempData["Reviews"];
            TempData.Keep();
            return View();
        }
       [HttpPost]
        public ActionResult AddActivity(Activity activity)
        {
            Activity ActivityFromDB = db.Activities.Find(activity.Id);
            var userId = User.Identity.GetUserId();
            Traveler traveler = db.Travelers.Include("CurrentItinerary").FirstOrDefault(t => t.ApplicationUserId == userId);
            Itinerary itinerary = traveler.CurrentItinerary;
            if (db.ItineraryActivities.Where(a => a.Id == activity.Id).Count() == 0)
            {
                db.ItineraryActivities.Add(new ItineraryActivity {Id= Guid.NewGuid(), Itinerary = itinerary, Activity = ActivityFromDB});
                db.SaveChanges();
            }
            return RedirectToAction("GetActivities");
        }


        // GET: Itinerary/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Itinerary/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Itinerary/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Itinerary/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult FindHotel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindHotel(Itinerary itinerary)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Traveler traveler = db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId);
                itinerary.Id = Guid.NewGuid();
                db.TravelerItineraries.Add(new TravelerItinerary() { Id = Guid.NewGuid(), Itinerary = itinerary, Traveler = traveler });
                traveler.CurrentItineraryID = itinerary.Id;
                db.SaveChanges();
                if (itinerary.StartDate.Ticks - itinerary.EndDate.Ticks >= 0)
                    return View();
                return RedirectToAction("GetActivities");
            }
            catch
            {
                return View();
            }
        }
    }
}
