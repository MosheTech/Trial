using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using KidSteps.ActionFilters;
using KidSteps.ViewModels;
using KidSteps.DAL;
using System.Web.Security;
using KidSteps.Utils;

namespace KidSteps.Controllers
{
    public partial class UserController : TargetedController<User>
    {
        //
        // GET: /User/

        [MyAuthorize(Role.SuperUser)]
        public virtual ViewResult Index()
        {
            return View(db.Members.ToList());
        }

        //
        // GET: /User/Details/5

        [UserTarget(Permission.ReadUser)]
        public virtual ViewResult Details()
        {
            return View(Target);
        }
        
        //
        // GET: /User/Edit/5

        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult Edit()
        {
            UserEditViewModel model = new UserEditViewModel(Target);

            return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Target.Bio = model.Bio;
                Target.Name = model.Name;
                db.SaveChanges();

                if (Request.Form["changeImage"] == "yes")
                    return RedirectToAction(Actions.ProfileImageEdit());// "ProfileImageEdit");
                else
                    return RedirectToAction(Actions.Details().WithId(Target));// "Details", IdRoute.Create(TargetUser.Id));
            }
            return View(model);
        }

        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult ProfileImageEdit()
        {
            return View(db.Images.Where(image => image.CreatedBy.Id == Target.Id).ToList());
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult ProfileImageEdit(long? imageId)
        {
            Image image = null;
            if (imageId.HasValue)
                image = db.Images.Find(imageId);

            if (image != null)
            {
                Target.ProfilePicture = image;
                db.SaveChanges();
            }

            return RedirectToAction(Actions.Edit().WithId(Target));// "Edit", new { id = TargetUser.Id });
        }

        //
        // GET: /User/Delete/5

        [UserTarget(Models.Permission.DeleteUser)]
        public virtual ActionResult Delete()
        {
            return View(Target);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [UserTarget(Models.Permission.DeleteUser)]
        public virtual ActionResult DeleteConfirmed()
        {            
            db.Members.Remove(Target);
            db.SaveChanges();
            return RedirectToAction(Actions.Index());// "Index");
        }


    }
}