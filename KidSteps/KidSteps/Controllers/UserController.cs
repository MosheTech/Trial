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

        [UserTarget(Permission.Read)]
        public ViewResult Details(int id)
        {
            return View(TargetUser);
        }
        
        //
        // GET: /User/Edit/5

        [UserTarget(Permission.Update)]
        public ActionResult Edit(int id)
        {
            UserEditViewModel model = new UserEditViewModel(TargetUser);

            return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [UserTarget(Permission.Update)]
        public ActionResult Edit(int id, UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                TargetUser.Bio = model.Bio;
                TargetUser.Name = model.Name;
                db.SaveChanges();

                if (Request.Form["changeImage"] == "yes")
                    return RedirectToAction("ProfileImageEdit");
                else
                    return RedirectToAction("Details", new { id = TargetUser.Id });
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ProfileImageEdit()
        {
            return View(db.Images.Where(image => image.CreatedBy.Id == CurrentUser.Id).ToList());
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult ProfileImageEdit(int userId, long? imageId)
        {
            var user = db.Members.Find(userId);
            Image image = null;
            if (imageId.HasValue)
                image = db.Images.Find(imageId);

            if (user != null && image != null)
            {
                user.ProfilePicture = image;
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = userId });
        }

        //
        // GET: /User/Delete/5

        [Authorize]
        public ActionResult Delete(long id)
        {
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(long id)
        {            
            User user = db.Members.Find(id);
            db.Members.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateKid()
        {
            KidCreateViewModel viewModel = new KidCreateViewModel();
            viewModel.Name.Last = CurrentUser.DefaultFamily.Name;

            if (!CurrentUser.DefaultFamily.HasKids)
                viewModel.ShouldChooseRelationship = true;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateKid(KidCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Family family = CurrentUser.DefaultFamily;

                FamilyRepository repos = new FamilyRepository();

                if (!family.HasKids)
                {
                    repos.AddRelationship(db, CurrentUser, model.RelationshipOfOwnerToKid);
                }

                repos.AddUnregisteredMember(db, family, model.Name, model.Email, RelationshipType.Self);

                return RedirectToAction("Details", "Family", new { id = family.Id });
            }

            return View(model);
        }

        public ActionResult CreateFamilyMember()
        {
            Family family = CurrentUser.DefaultFamily;

            CreateFamilyMemberViewModel viewModel = new CreateFamilyMemberViewModel();
            viewModel.Name.Last = family.Name;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateFamilyMember(CreateFamilyMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                Family family = CurrentUser.DefaultFamily;

                FamilyRepository repos = new FamilyRepository();
                repos.AddUnregisteredMember(db, family, model.Name, model.Email, model.Relationship);

                return RedirectToAction("Details", "Family", new { id = family.Id });
            }

            return View(model);
        }


    }
}