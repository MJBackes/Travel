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
            ViewBag.Activities = GetMatchingActivities(traveler);
            ViewBag.Popular = GetTopFive(ViewBag.Activities).ToArray();
            ViewBag.Activities = ViewBag.Activities.ToArray();
            ViewBag.HotelLat = decimal.Parse(traveler.CurrentItinerary.HotelLocationString.Split(',')[0]);
            ViewBag.HotelLng = decimal.Parse(traveler.CurrentItinerary.HotelLocationString.Split(',')[1]);
            return View();
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
        private List<Business> GetTopFive(List<Business> businesses)
        {
            if (businesses.Count <= 5)
                return businesses;
            List<Business> output = new List<Business>();
            businesses = businesses.OrderByDescending(b => b.review_count).ToList();
            for (int i = 0; i < 5; i++) {
                output.Add(businesses[i]);
                }
            return output;
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
                db.ItineraryActivities.Add(new ItineraryActivity { Itinerary = itinerary, Activity = ActivityFromDB});
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
                traveler.Itineraries.Add(itinerary);
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
