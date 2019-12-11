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
            return View();
        }
        [HttpPost]
        public ActionResult GetActivities(List<Activity> activities)
        {
            //To do:
            //Logic to store activities chosen in view to DB.
            return View("Index");
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
        public ActionResult FindHotel(Itinerary itinerary)
        {
            return View(itinerary);
        }
    }
}
