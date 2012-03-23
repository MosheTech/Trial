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
    [Authorize]
    public class ImageController : ControllerBase
    {
        //
        // GET: /Image/

        public ViewResult Index()
        {
            var currentUser = CurrentUser;
            return View(db.Images.Where(image => image.CreatedBy.Id.Equals(currentUser.Id)).ToList());
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

        public ActionResult Create(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        } 

        //
        // POST: /Image/Create

        [HttpPost]
        public ActionResult Create(ImageViewModel imageViewModel, string returnUrl = null)
        {
            
            if (ModelState.IsValid)
            {
                Image image = new Image();
                image.AltText = string.Empty;
                image.CreatedBy = CurrentUser;
                image.Extension = Path.GetExtension(imageViewModel.File.FileName);
                db.Images.Add(image);
                db.SaveChanges();

                try
                {
                    var thumbnail =
                        System.Drawing.Image.FromStream(imageViewModel.File.InputStream).GetThumbnailImage(100, 100,
                                                                                                           null,
                                                                                                           IntPtr.Zero);

                    var rootedPath = Path.Combine(Server.MapPath("~"), image.Path);

                    //System.IO.File.WriteAllText(rootedPath, "Testing valid path & permissions.");

                    thumbnail.Save(rootedPath);
                    //imageViewModel.File.InputStream.Close();

                    if (!String.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index");
                }
                catch
                {
                    db.Images.Remove(image);
                    db.SaveChanges();
                    throw;
                }
            }

            return View(imageViewModel);
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

        //[HttpPost, ActionName("Delete")]
        //[Authorize]
        //public ActionResult DeleteConfirmed(long id)
        //{            
        //    Image image = db.Images.Find(id);
        //    db.Media.Remove(image);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}