using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class TravellerController : Controller
    {
        public ApplicationDbContext db;
        public TravellerController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Traveller
        public ActionResult Index()
        {
            return View();
        }

        // GET: Traveller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Traveller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Traveller/Create
        [HttpPost]
        public ActionResult Create(Traveller traveller)
        {
            traveller.Id = Guid.NewGuid();
                traveller.ApplicationUserId = User.Identity.GetUserId();
                db.Travellers.Add(traveller);
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
            Traveller traveller = db.Travellers.FirstOrDefault(t => t.ApplicationUserId == userId);
            foreach(Interest interest in interests)
            {
                if(interest.isChecked)
                traveller.Interests.Add(db.Interests.FirstOrDefault(i => i.Id == interest.Id));
            }
            db.SaveChanges();
            return View("Index");
        }

        [HttpGet]
        public ActionResult EditInterests()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Travellers.First(t => t.ApplicationUserId == userId).Interests.ToList());
        }
        [HttpPost]
        public ActionResult EditInteresets(List<Interest> interests)
        {
            var userId = User.Identity.GetUserId();
            List<Interest> origianlInterests = db.Travellers.First(t => t.ApplicationUserId == userId).Interests.ToList();
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
        // GET: Traveller/Edit/5
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Travellers.FirstOrDefault(t => t.ApplicationUserId == userId));
        }

        // POST: Traveller/Edit/5
        [HttpPost]
        public ActionResult Edit(Traveller traveller)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Traveller travellerFromDb = db.Travellers.FirstOrDefault(t => t.ApplicationUserId == userId);
                travellerFromDb.FirstName = traveller.FirstName;
                travellerFromDb.LastName = traveller.LastName;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Traveller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Traveller/Delete/5
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
