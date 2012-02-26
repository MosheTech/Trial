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
    public class ImageController : ControllerBase
    {
        //
        // GET: /Image/

        public ViewResult Index()
        {
            return View(db.Images.ToList());
        }

        //
        // GET: /Image/Details/5

        public ViewResult Details(long id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // GET: /Image/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Image/Create

        [HttpPost]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Media.Add(image);
                image.Extension = Path.GetExtension(image.File.FileName);
                db.SaveChanges();

                var thumbnail = System.Drawing.Image.FromStream(image.File.InputStream).GetThumbnailImage(100, 100, null, IntPtr.Zero);

                var rootedPath = Path.Combine(Server.MapPath("~"), image.Path);

                thumbnail.Save(rootedPath);

                return RedirectToAction("Index");
            }

            return View(image);
        }
        
        //
        // GET: /Image/Edit/5
 
        public ActionResult Edit(long id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        //
        // GET: /Image/Delete/5
 
        public ActionResult Delete(long id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Image image = db.Images.Find(id);
            db.Media.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}