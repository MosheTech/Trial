using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;

namespace KidSteps.Controllers
{ 
    public class UserController : ControllerBase
    {
        //
        // GET: /User/

        public ViewResult Index()
        {
            return View(db.Members.ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(string id)
        {
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // GET: /User/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        ////
        //// POST: /User/Create

        //[HttpPost]
        //public ActionResult Create(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Members.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");  
        //    }

        //    return View(user);
        //}
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(string id)
        {
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult ProfileImageEdit()
        {
            return View(db.Images);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult ProfileImageEdit(string userId, long imageId)
        {
            var user = db.Members.Find(userId);
            var image = db.Images.Find(imageId);

            if (user != null && image != null)
            {
                user.ProfilePicture = image;
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = userId });
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(long id)
        {
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            User user = db.Members.Find(id);
            db.Members.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}