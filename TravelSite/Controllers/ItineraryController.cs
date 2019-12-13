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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Itinerary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Itinerary/Create
        [HttpPost]
        public ActionResult Create(Itinerary itinerary)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                itinerary.Travelers.Add(db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId));
                itinerary.Id = Guid.NewGuid();
                itinerary.TimeSpan = itinerary.EndDate - itinerary.StartDate;
                if (itinerary.TimeSpan.TotalDays < 0)
                    return View();
                return View("FindHotel", itinerary);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult GetActivities()
        {
            //To do:
            //Get activity data from APIs to populate view.
            var userId = User.Identity.GetUserId();
            Traveler traveler = db.Travelers.Include("Interests").Include("CurrentItinerary").FirstOrDefault(t => t.ApplicationUserId == userId);
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
            if (db.Activities.Where(a => a.Name == activity.Name && a.Address == activity.Address).Count() == 0)
            {
                db.Activities.Add(activity);
            }
            return View();
        }

        //POST: AddActivity
       [HttpPost]
        public ActionResult AddActivity(Activity activity)
        {
            var userId = User.Identity.GetUserId();
            activity.Id = Guid.NewGuid();
            Traveler traveler = db.Travelers.Include("CurrentItinerary").FirstOrDefault(t => t.ApplicationUserId == userId);
            Itinerary itinerary = traveler.CurrentItinerary;
            itinerary.Activities.Add(activity);
            db.SaveChanges();
            return View("Itinerary", "Itinerary");
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
