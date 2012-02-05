using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.IO;

namespace KidSteps.Controllers
{ 
    public class MediaController : Controller
    {
        private KidStepsContext db = new KidStepsContext();

        //
        // GET: /Media/

        public ViewResult Index()
        {
            return View(db.Media.ToList());
        }

        //
        // GET: /Media/Details/5

        public ViewResult Details(long id)
        {
            Media media = db.Media.Find(id);
            return View(media);
        }

        //
        // GET: /Media/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Media/Create

        [HttpPost]
        public ActionResult Create(Image media)
        {
            if (ModelState.IsValid)
            {
                var rootedPath = Path.Combine(Server.MapPath("~"), media.Url);
                //VirtualPathUtility
                    
                media.File.SaveAs(rootedPath);

                db.Media.Add(media);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(media);
        }
        
        //
        // GET: /Media/Edit/5
 
        public ActionResult Edit(long id)
        {
            Media media = db.Media.Find(id);
            return View(media);
        }

        //
        // POST: /Media/Edit/5

        [HttpPost]
        public ActionResult Edit(Media media)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(media);
        }

        //
        // GET: /Media/Delete/5
 
        public ActionResult Delete(long id)
        {
            Media media = db.Media.Find(id);
            return View(media);
        }

        //
        // POST: /Media/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Media media = db.Media.Find(id);
            db.Media.Remove(media);
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