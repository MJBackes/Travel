using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class TravelerController : Controller
    {
        public ApplicationDbContext db;
        public TravelerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Traveler
        public ActionResult Index()
        {
            return View();
        }

        // GET: Traveler/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Traveler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Traveler/Create
        [HttpPost]
        public ActionResult Create(Traveler Traveler)
        {
            Traveler.Id = Guid.NewGuid();
                Traveler.ApplicationUserId = User.Identity.GetUserId();
                db.Travelers.Add(Traveler);
                db.SaveChanges();
                return RedirectToAction("GetInterests");

        }
        [HttpGet]
        public ActionResult GetInterests()
        {
            return View(db.Interests.ToList());
        }
        [HttpPost]
        public ActionResult GetInterests(List<Interest> interests)
        {
            var userId = User.Identity.GetUserId();
            Traveler Traveler = db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId);
            foreach(Interest interest in interests)
            {
                if(interest.isChecked)
                Traveler.Interests.Add(db.Interests.FirstOrDefault(i => i.Id == interest.Id));
            }
            db.SaveChanges();
            return View("Index");
        }

        [HttpGet]
        public ActionResult EditInterests()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Travelers.First(t => t.ApplicationUserId == userId).Interests.ToList());
        }
        [HttpPost]
        public ActionResult EditInteresets(List<Interest> interests)
        {
            var userId = User.Identity.GetUserId();
            List<Interest> origianlInterests = db.Travelers.First(t => t.ApplicationUserId == userId).Interests.ToList();
            foreach(Interest i in origianlInterests)
            {
                if (!interests.Contains(i))
                {
                    origianlInterests.Remove(i);
                }
            }
            foreach (Interest i in interests)
            {
                if (!origianlInterests.Contains(i))
                {
                    origianlInterests.Add(i);
                }
            }
            return View("Index");
        }
        // GET: Traveler/Edit/5
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId));
        }

        // POST: Traveler/Edit/5
        [HttpPost]
        public ActionResult Edit(Traveler Traveler)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Traveler TravelerFromDb = db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId);
                TravelerFromDb.FirstName = Traveler.FirstName;
                TravelerFromDb.LastName = Traveler.LastName;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Traveler/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Traveler/Delete/5
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
    }
}
