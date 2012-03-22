﻿using System;
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
        public ViewResult Details(int id)
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
        public ActionResult Edit(int id)
        {
            if (!VerifyCurrentUser(id))
                throw new Exception();
            User user = db.Members.Find(id);

            UserEditViewModel model = new UserEditViewModel();
            model.Name = user.Name;
            model.Bio = user.Bio;
            model.ProfilePicture = user.ProfilePicture;

            return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User currentUser = GetCurrentUser();
                currentUser.Bio = model.Bio;
                currentUser.Name = model.Name;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = currentUser.Id });
            }
            return View(model);
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
            User user = GetCurrentUser();

            KidCreateViewModel viewModel = new KidCreateViewModel();
            viewModel.Name.Last = user.DefaultFamily.Name;

            if (!user.DefaultFamily.HasKids)
                viewModel.ShouldChooseRelationship = true;
            viewModel.RelationshipsToChooseFrom = FamilyController.FamilyRelationships;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateKid(KidCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User currentUser = GetCurrentUser();

                Family family = currentUser.DefaultFamily;

                FamilyRepository repos = new FamilyRepository();

                if (!family.HasKids)
                {
                    repos.AddRelationship(db, currentUser, model.RelationshipOfOwnerToKid);
                }

                repos.AddUnregisteredMember(db, family, model.Name, model.Email, RelationshipType.Self);



                //MembershipCreateStatus _;
                //UserRepository userRepos = new UserRepository();
                //User kid = userRepos.CreatePublicViewer(db, model.Name, Role.UnregisteredMember, out _);

                //FamilyRepository familyRepos = new FamilyRepository();
                //Family family = db.Families.Find(model.FamilyId);
                //familyRepos.AddMember(db, family, kid, RelationshipType.Self, setAsUsersDefaultFamily: true);

                return RedirectToAction("Details", "Family", new { id = family.Id });
            }

            return View();
        }

        public ActionResult CreateFamilyMember(long familyId)
        {
            Family family = GetCurrentUser().DefaultFamily;

            CreateFamilyMemberViewModel viewModel = new CreateFamilyMemberViewModel();
            viewModel.RelationshipsToChooseFrom = FamilyController.FamilyRelationships;
            viewModel.Name.Last = family.Name;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateFamilyMember(CreateFamilyMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                Family family = GetCurrentUser().DefaultFamily;

                FamilyRepository repos = new FamilyRepository();
                repos.AddUnregisteredMember(db, family, model.Name, model.Email, model.Relationship);

                //MembershipCreateStatus _;
                //UserRepository userRepos = new UserRepository();
                //User member = userRepos.CreatePublicViewer(db, model.Name, Role.UnregisteredMember, out _);

                //FamilyRepository familyRepos = new FamilyRepository();
                //Family family = db.Families.Find(model.FamilyId);
                //familyRepos.AddMember(db, family, member, model.Relationship, setAsUsersDefaultFamily: true);

                return RedirectToAction("Details", "Family", new { id = family.Id });
            }

            return View();
        }


    }
}