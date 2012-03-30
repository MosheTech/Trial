using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using KidSteps.ViewModels;
using KidSteps.DAL;
using System.Web.Security;
using KidSteps.ActionFilters;
using KidSteps.Utils;

namespace KidSteps.Controllers
{
    [Authorize]
    public partial class FamilyController : TargetedController<Family>
    {
        static FamilyController()
        {
            List<SelectListItem> relationshipTypes = new List<SelectListItem>();
            foreach (RelationshipType type in Enum.GetValues(typeof(RelationshipType)))
            {
                //if (type == RelationshipType.Self)
                //    continue;
                SelectListItem item = 
                    new SelectListItem() { Text = type.ToString(), Value = ((int)type).ToString() };
                relationshipTypes.Add(item);
            }
            RelationshipsTypes = relationshipTypes;
        }

        //
        // GET: /Family/

        [MyAuthorize(Role.SuperUser)]
        public virtual ViewResult Index()
        {
            return View(db.Families.ToList());
        }

        //
        // GET: /Family/Details/5

        [FamilyTarget(Permission.ViewFamily)]
        public virtual ViewResult Details()
        {
            FamilyDetailsViewModel model = new FamilyDetailsViewModel();

            //Family family = db.Families.Find(id);
            model.Family = Target;
            List<User> allMembers = Target.Members.ToList();
            model.FamilyMembers = allMembers.Where(u => !u.IsKid);//.Where(fm => fm.Relationship != RelationshipType.Self && fm.Relationship != RelationshipType.None);
            model.Kids = allMembers.Where(u => u.IsKid);

            return View(model);
        }

        //
        // GET: /Family/Create

        //public virtual ActionResult Create()
        //{
        //    FamilyCreateViewModel viewModel = new FamilyCreateViewModel();
        //    viewModel.RelationshipsToChooseFrom = new List<SelectListItem>();
        //    foreach (RelationshipType type in Enum.GetValues(typeof(RelationshipType)))
        //    {
        //        if (type == RelationshipType.Self)
        //            continue;
        //        SelectListItem item = new SelectListItem() { Text = type.ToString(), Value = ((int)type).ToString() };
        //        viewModel.RelationshipsToChooseFrom.Add(item);
        //    }
        //    return View(viewModel);
        //} 

        ////
        //// POST: /Family/Create

        //[HttpPost]
        //public virtual ActionResult Create(FamilyCreateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        FamilyRepository repos = new FamilyRepository();
        //        var family =
        //            repos.Create(
        //            db, 
        //            model.FamilyName,
        //            CurrentUser,
        //            model.KidName,
        //            model.KidEmail, 
        //            model.RelationshipOfOwnerToKid);

        //        return RedirectToAction("Details", new { id = family.Id }); 
        //    }

        //    return View(model);
        //}
        
        //
        // GET: /Family/Edit/5

        [FamilyTarget(Permission.EditFamily)]
        public virtual ActionResult Edit()
        {
            //Family family = db.Families.Find(id);
            return View(Target);
        }

        //
        // POST: /Family/Edit/5

        [HttpPost]
        [FamilyTarget(Permission.EditFamily)]
        public virtual ActionResult Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Target).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(Actions.Index());
            }
            return View(family);
        }

        ////
        //// GET: /Family/Delete/5

        //public virtual ActionResult Delete(long id)
        //{
        //    Family family = db.Families.Find(id);
        //    return View(family);
        //}

        ////
        //// POST: /Family/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public virtual ActionResult DeleteConfirmed(long id)
        //{            
        //    Family family = db.Families.Find(id);
        //    db.Families.Remove(family);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        //[FamilyTarget(Models.Permission.EditFamily)]
        //public virtual ActionResult CreateKid()
        //{
        //    KidCreateViewModel viewModel = new KidCreateViewModel();
        //    viewModel.Name.Last = Target.Name;

        //    if (!Target.HasKids)
        //        viewModel.ShouldChooseRelationship = true;

        //    return View(viewModel);
        //}

        //[HttpPost]
        //[FamilyTarget(Models.Permission.EditFamily)]
        //public virtual ActionResult CreateKid(KidCreateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        FamilyRepository repos = new FamilyRepository();

        //        if (!Target.HasKids)
        //            repos.AddRelationship(db, Target.Admin, model.RelationshipOfOwnerToKid);

        //        repos.AddUnregisteredMember(db, Target, model.Name, model.Email, RelationshipType.Self);

        //        return RedirectToAction(MVC.Family.Details().WithId(Target.Id));
        //    }

        //    return View(model);
        //}

        [FamilyTarget(Models.Permission.EditFamily)]
        public virtual ActionResult AddFamilyMember()
        {
            CreateFamilyMemberViewModel viewModel = new CreateFamilyMemberViewModel();
            viewModel.Name.Last = Target.Name;

            return View(viewModel);
        }

        [HttpPost]
        [FamilyTarget(Models.Permission.EditFamily)]
        public virtual ActionResult AddFamilyMember(CreateFamilyMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                FamilyRepository repos = new FamilyRepository();
                repos.AddFamilyMember(db, Target, model.Name, model.Email, model.IsKid);// .AddUnregisteredMember(db, Target, model.Name, model.Email, model.Relationship);

                return RedirectToAction(MVC.Family.Details().WithId(Target.Id));
            }

            return View(model);
        }

        public static IEnumerable<SelectListItem> RelationshipsTypes;
    }
}