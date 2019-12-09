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
            try
            {
                traveller.ApplicationUserId = User.Identity.GetUserId();
                db.Travellers.Add(traveller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Traveller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Traveller/Edit/5
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
