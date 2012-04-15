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
using KidSteps.Handlers;

namespace KidSteps.Controllers
{
    public partial class UserController : TargetedController<User>
    {
        //
        // GET: /User/

        [MyAuthorize(Permission.SuperUserAccess)]
        public virtual ViewResult Index()
        {
            return View(db.Users.OrderBy(u => u.Name.Last).ToList());
        }

        //
        // GET: /User/Details/5

        [UserTarget(Permission.ReadUser)]
        public virtual ViewResult Details()
        {
            UserDetailsViewModel model = new UserDetailsViewModel();
            model.User = Target;
            model.IsAllowedToEdit = CurrentUser.IsAllowedTo(Permission.UpdateUser, Target);

            if (model.IsAllowedToEdit && Target.IsUnregisteredFamilyMember)
                model.InvitationUrl = InvitationHandler.CreateUrl(Target, Url, Request);

            model.ChildrenRelationships.AddRange(
                Target.Relationships.
                    Where(rel => rel.RelatedUserIsSourceUsers == RelationshipType.Child));

            model.ParentRelationships.AddRange(
                Target.Relationships.
                    Where(rel => rel.RelatedUserIsSourceUsers == RelationshipType.Parent));

            model.OtherImmediateFamilyRelationships.AddRange(
                Target.Relationships
                .Except(model.ParentRelationships)
                .Except(model.ChildrenRelationships));

            return View(model);
        }
        
        //
        // GET: /User/Edit/5

        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult Edit()
        {
            UserEditViewModel model = new UserEditViewModel(Target);

            model.CanEditRelationships = CurrentUser.IsAllowedTo(Permission.EditFamily, Target.Family);

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
                if (!string.IsNullOrEmpty(model.Email))
                {
                    Target.Email = model.Email;
                    Target.HasRealEmail = true;
                }
                else
                {
                    Target.HasRealEmail = false;
                }
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
            ImageSelectViewModel model = new ImageSelectViewModel();
            model.Images = db.Images.Where(image => image.CreatedBy.Id == CurrentUser.Id).ToList();

            return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [UserTarget(Permission.UpdateUser)]
        public virtual ActionResult ProfileImageEdit(ImageSelectViewModel model)
        {
            Image image = db.Images.Find(model.SelectedImageId);

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

            model.TargetUser = Target;

            model.FamilyRelationshipsNew = new List<UserEditRelationshipsViewModel.RelationshipViewModel>();
            // existing relationships
            foreach (var relationship in Target.Relationships)
            {
                ViewModels.UserEditRelationshipsViewModel.RelationshipViewModel r =
                    new UserEditRelationshipsViewModel.RelationshipViewModel();
                r.RelatedUserId = relationship.RelatedUser.Id;
                r.RelatedUser = relationship.RelatedUser;
                r.Relationship = (UserEditRelationshipsViewModel.RelationshipTypeViewModel)relationship.RelatedUserIsSourceUsers.Reciprocal();
                model.FamilyRelationshipsNew.Add(r);
            }
            // non-immediate family
            foreach (var familyMember in Target.Family.Members)
            {
                if (model.FamilyRelationshipsNew.Any(vm => vm.RelatedUserId == familyMember.Id))
                    continue;
                if (familyMember.Id == Target.Id)
                    continue;
                if (familyMember.IsPublicViewer)
                    continue;

                ViewModels.UserEditRelationshipsViewModel.RelationshipViewModel r =
                    new UserEditRelationshipsViewModel.RelationshipViewModel();
                r.RelatedUserId = familyMember.Id;
                r.RelatedUser = familyMember;
                r.Relationship = UserEditRelationshipsViewModel.RelationshipTypeViewModel.NotImmediateFamilyMember;
                model.FamilyRelationshipsNew.Add(r);
            }

            model.FamilyRelationshipsNew.Sort((r1, r2) => r1.RelatedUser.Name.First.CompareTo(r2.RelatedUser.Name.First));



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
                //Relationship relationship = new Relationship();
                //relationship.SourceUser = Target;
                //relationship.RelatedUser = db.Users.Find(model.NewRelatedUserId);
                //relationship.RelatedUserIsSourceUsers = model.NewRelationshipType;
                //repos.UpdateRelationship(db, relationship);

                foreach (var item in model.FamilyRelationshipsNew)
                {
                    Relationship relationship = new Relationship();
                    relationship.SourceUser = Target;
                    relationship.RelatedUser = db.Users.Find(item.RelatedUserId);
                    if (item.Relationship == UserEditRelationshipsViewModel.RelationshipTypeViewModel.NotImmediateFamilyMember)
                    {
                        repos.RemoveRelationship(db, relationship);
                    }
                    else
                    {
                        relationship.RelatedUserIsSourceUsers = ((RelationshipType)item.Relationship).Reciprocal();
                        repos.UpdateRelationship(db, relationship);
                    }
                }

                return RedirectToAction(MVC.User.Details().WithId(Target));
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