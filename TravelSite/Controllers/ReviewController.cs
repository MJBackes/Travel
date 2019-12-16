using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSite.Models;

namespace TravelSite.Controllers
{
    public class ReviewController : Controller
    {
        ApplicationDbContext db;
        public ReviewController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Review
        public ActionResult Index()
        {
            var listOfReviews = db.Reviews.ToList();
            return View(listOfReviews);
        }

        //// GET: Review/Details/5
        //public ActionResult Details(Guid id)
        //{
           
        //}

        // GET: Review/Create
        public ActionResult Create(Guid? id)
        {
            var userId = User.Identity.GetUserId();
            Traveler traveler = db.Travelers.FirstOrDefault(t => t.ApplicationUserId == userId);
            return View(new Review { ActivityId = (Guid)id, TravelerId = traveler.Id });
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(Review review)
        {
            try
            {
                //logged in user
                var userId = User.Identity.GetUserId();
                review.Id = Guid.NewGuid();
                //add view to specific activity\
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("AddActivity", "Itinerary");
            }
            catch
            {
                return RedirectToAction("AddActivity", "Itinerary");
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(Guid? id)
        {
            Review review = db.Reviews.Find(id);
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(Review review)
        {
            //incomnp. backup option below
            //review = db.Review.Find()
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Review/Delete/5
        public ActionResult Delete(Guid id)
        {
            Review review = db.Reviews.Find(id);
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(Review review)
        {
            try
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
