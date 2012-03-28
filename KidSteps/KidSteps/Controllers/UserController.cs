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

namespace KidSteps.Controllers
{ 
    public class UserController : ControllerBase
    {
        //
        // GET: /User/

        [MyAuthorize(Role.SuperUser)]
        public ViewResult Index()
        {
            return View(db.Members.ToList());
        }

        //
        // GET: /User/Details/5

        [UserTarget(Permission.ReadUser)]
        public ViewResult Details(int id)
        {
            return View(TargetUser);
        }
        
        //
        // GET: /User/Edit/5

        [UserTarget(Permission.UpdateUser)]
        public ActionResult Edit(int id)
        {
            UserEditViewModel model = new UserEditViewModel(TargetUser);

            return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [UserTarget(Permission.UpdateUser)]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                TargetUser.Bio = model.Bio;
                TargetUser.Name = model.Name;
                db.SaveChanges();

                if (Request.Form["changeImage"] == "yes")
                    return RedirectToAction("ProfileImageEdit");
                else
                    return RedirectToAction("Details", IdRoute.Create(TargetUser.Id));
            }
            return View(model);
        }

        [UserTarget(Permission.UpdateUser)]
        public ActionResult ProfileImageEdit()
        {
            return View(db.Images.Where(image => image.CreatedBy.Id == TargetUser.Id).ToList());
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [UserTarget(Permission.UpdateUser)]
        public ActionResult ProfileImageEdit(long? imageId)
        {
            Image image = null;
            if (imageId.HasValue)
                image = db.Images.Find(imageId);

            if (image != null)
            {
                TargetUser.ProfilePicture = image;
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = TargetUser.Id });
        }

        //
        // GET: /User/Delete/5

        [UserTarget(Models.Permission.DeleteUser)]
        public ActionResult Delete()
        {
            return View(TargetUser);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [FamilyTarget(Models.Permission.DeleteUser)]
        public ActionResult DeleteConfirmed()
        {            
            db.Members.Remove(TargetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [UserTarget(Models.Permission.AddFamilyMember)]
        public ActionResult CreateKid()
        {
            KidCreateViewModel viewModel = new KidCreateViewModel();
            viewModel.Name.Last = TargetFamily.Name;

            if (TargetFamily.HasKids)
                viewModel.ShouldChooseRelationship = true;

            return View(viewModel);
        }

        [HttpPost]
        [FamilyTarget(Models.Permission.AddFamilyMember)]
        public ActionResult CreateKid(KidCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                FamilyRepository repos = new FamilyRepository();

                if (!TargetFamily.HasKids)
                {
                    repos.AddRelationship(db, TargetFamily.Owner, model.RelationshipOfOwnerToKid);
                }

                repos.AddUnregisteredMember(db, TargetFamily, model.Name, model.Email, RelationshipType.Self);

                return RedirectToAction("Details", "Family", IdRoute.Create(TargetFamily.Id));
            }

            return View(model);
        }

        [FamilyTarget(Models.Permission.AddFamilyMember)]
        public ActionResult CreateFamilyMember()
        {
            CreateFamilyMemberViewModel viewModel = new CreateFamilyMemberViewModel();
            viewModel.Name.Last = TargetFamily.Name;

            return View(viewModel);
        }

        [HttpPost]
        [FamilyTarget(Models.Permission.AddFamilyMember)]
        public ActionResult CreateFamilyMember(CreateFamilyMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                FamilyRepository repos = new FamilyRepository();
                repos.AddUnregisteredMember(db, TargetFamily, model.Name, model.Email, model.Relationship);

                return RedirectToAction("Details", "Family", IdRoute.Create(TargetFamily.Id));
            }

            return View(model);
        }


    }
}