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

        [Authorize]
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

        [Authorize]
        public ActionResult Edit(string id)
        {
            if (!VerifyCurrentUser(id))
                throw new Exception();
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (!VerifyCurrentUser(user.Id))
                    throw new Exception();

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Authorize]
        public ActionResult ProfileImageEdit()
        {
            var currentUser = GetCurrentUser();
            return View(db.Images.Where(image => image.CreatedBy.Id == currentUser.Id).ToList());
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult ProfileImageEdit(string userId, long? imageId)
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateKid(KidCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus _;
                UserRepository userRepos = new UserRepository();
                User kid = userRepos.CreateUnregisteredUser(db, model.Name, out _);

                FamilyRepository familyRepos = new FamilyRepository();


                RegisterModel registerModel = new RegisterModel();
                string password = Membership.GeneratePassword(30, 3);
                registerModel.Email = (new Guid()).ToString();
                registerModel.Name = model.Name;
                registerModel.Password = password;
                registerModel.ConfirmPassword = password;
                userRepos.Create(db, model.Name, registerModel.Email, registerModel.Password, Role.UnregisteredFamilyMember, out _);

                return RedirectToAction("Index");
            }

            return View();
        }


    }
}