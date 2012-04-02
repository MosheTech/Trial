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
using KidSteps.Utils;

namespace KidSteps.Controllers
{
    public partial class UserController : TargetedController<User>
    {
        //
        // GET: /User/

        [MyAuthorize(Permission.SuperUserAccess)]
        public virtual ViewResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /User/Details/5

        [UserTarget(Permission.ReadUser)]
        public virtual ViewResult Details()
        {
            UserDetailsViewModel model = new UserDetailsViewModel();
            model.User = Target;
            model.IsAllowedToEdit = CurrentUser.IsAllowedTo(Permission.UpdateUser, Target);

            model.Children.AddRange(
                Target.Relationships.
                    Where(rel => rel.RelatedUserIsSourceUsers == RelationshipType.Child).
                    Select(rel => rel.RelatedUser));

            model.Parents.AddRange(
                Target.Relationships.
                    Where(rel => rel.RelatedUserIsSourceUsers == RelationshipType.Parent).
                    Select(rel => rel.RelatedUser));

            model.OtherImmediateFamily.AddRange(
                Target.Relationships.Select(rel => rel.RelatedUser)
                .Except(model.Parents)
                .Except(model.Children));

            return View(model);
        }
        
        //
        // GET: /User/Edit/5

        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult Edit()
        {
            UserEditViewModel model = new UserEditViewModel(Target);

            model.CanEditRelationships =
                CurrentUser.IsAllowedTo(Permission.EditFamily, Target.Family);            

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
                    return RedirectToAction(Actions.ProfileImageEdit().WithId(Target));
                else
                    return RedirectToAction(Actions.Details().WithId(Target));
            }
            return View(model);
        }

        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult ProfileImageEdit()
        {
            return View(db.Images.Where(image => image.CreatedBy.Id == CurrentUser.Id).ToList());
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

            return RedirectToAction(Actions.Edit().WithId(Target));
        }

        [UserTarget(Models.Permission.EditFamily)]
        public virtual ActionResult RelationshipsEdit()
        {
            UserEditRelationshipsViewModel model = new UserEditRelationshipsViewModel();
            model.RelationshipTypes = FamilyController.RelationshipsTypes;
            model.CurrentRelationships = Target.Relationships.ToList();
            model.UnrelatedFamilyMembers = new List<SelectListItem>();
            var unrelatedUsers =
                Target.Family.Members.Except(model.CurrentRelationships.Select(r => r.RelatedUser)).ToList();
            foreach (User unrelatedUser in unrelatedUsers)
            {
                if (unrelatedUser.IsPublicViewer || unrelatedUser.Id == Target.Id)
                    continue;
                model.UnrelatedFamilyMembers.Add(
                    new SelectListItem()
                    {
                        Text = unrelatedUser.Name.Full,
                        Value = unrelatedUser.Id.ToString()
                    });
            }

            model.TargetUser = Target;

            return View(model);
        }

        [HttpPost]
        [UserTarget(Models.Permission.EditFamily)]
        public virtual ActionResult RelationshipsEdit(UserEditRelationshipsViewModel model)
        {
            if (ModelState.IsValid)
            {
                FamilyRepository repos = new FamilyRepository();
                // save the new relationship
                Relationship relationship = new Relationship();
                relationship.SourceUser = Target;
                relationship.RelatedUser = db.Users.Find(model.NewRelatedUserId);
                relationship.RelatedUserIsSourceUsers = model.NewRelationshipType;
                repos.AddRelationship(db, relationship);

                return RedirectToAction(MVC.User.RelationshipsEdit().WithId(Target));
            }

            return View(model);
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
            db.Users.Remove(Target);
            db.SaveChanges();
            return RedirectToAction(Actions.Index());
        }


    }
}