using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.IO;
using KidSteps.ActionFilters;
using KidSteps.Utils;

namespace KidSteps.Controllers
{ 
    [Authorize]
    public partial class ImageController : TargetedController<User>
    {
        //
        // GET: /Image/
        [UserTarget(Models.Permission.ReadUserPersonalData)]
        public virtual ViewResult Index()
        {
            return View(db.Images.Where(image => image.CreatedBy.Id.Equals(Target.Id)).ToList());
        }

        //
        // GET: /Image/Details/5

        [UserTarget(Models.Permission.ReadUserPersonalData)]
        public virtual ViewResult Details(long id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // GET: /Image/Create
        [UserTarget(Permission.UploadImage)]
        public virtual ActionResult Create(string returnUrl = null, bool shouldSetAsProfile = false)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ShouldSetAsProfile = shouldSetAsProfile;
            return View();
        } 

        //
        // POST: /Image/Create

        [HttpPost]
        [UserTarget(Permission.UploadImage)]
        public virtual ActionResult Create(ImageViewModel imageViewModel, string returnUrl = null, bool shouldSetAsProfile = false)
        {
            
            if (ModelState.IsValid)
            {
                Image image = new Image();
                image.AltText = string.Empty;
                image.CreatedBy = CurrentUser;
                image.Extension = Path.GetExtension(imageViewModel.File.FileName);
                db.Images.Add(image);
                if (shouldSetAsProfile)
                    Target.ProfilePicture = image;
                db.SaveChanges();

                try
                {
                    var thumbnail =
                        System.Drawing.Image.FromStream(imageViewModel.File.InputStream).GetThumbnailImage(200, 200,
                                                                                                           null,
                                                                                                           IntPtr.Zero);

                    var rootedPath = Path.Combine(Server.MapPath("~"), image.Path);

                    thumbnail.Save(rootedPath);

                    if (shouldSetAsProfile)
                        return RedirectToAction(MVC.User.Edit().WithId(Target));
                    else if (!String.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
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

        public virtual ActionResult Edit(long id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Image image)
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

        public virtual ActionResult Delete(long id)
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