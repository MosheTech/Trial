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

namespace KidSteps.Controllers
{
    [Authorize]
    public class FamilyController : ControllerBase
    {

        //
        // GET: /Family/

        [MyAuthorize(Role.SuperUser)]
        public ViewResult Index()
        {
            return View(db.Families.ToList());
        }

        //
        // GET: /Family/Details/5

        [Authorize]
        public ViewResult Details(long id)
        {
            FamilyDetailsViewModel model = new FamilyDetailsViewModel();

            Family family = db.Families.Find(id);
            model.Family = family;
            var allMembers = family.Members.AsQueryable().Include("User").ToList();
            model.FamilyMembers = allMembers.Where(fm => fm.Relationship != RelationshipType.Self && fm.Relationship != RelationshipType.None);
            model.Kids = allMembers.Where(fm => fm.Relationship == RelationshipType.Self);

            return View(model);
        }

        //
        // GET: /Family/Create

        public ActionResult Create()
        {
            FamilyCreateViewModel viewModel = new FamilyCreateViewModel();
            viewModel.RelationshipsToChooseFrom = new List<SelectListItem>();
            foreach (RelationshipType type in Enum.GetValues(typeof(RelationshipType)))
            {
                if (type == RelationshipType.Self)
                    continue;
                SelectListItem item = new SelectListItem() { Text = type.ToString(), Value = ((int)type).ToString() };
                viewModel.RelationshipsToChooseFrom.Add(item);
            }
            return View(viewModel);
        } 

        //
        // POST: /Family/Create

        [HttpPost]
        public ActionResult Create(FamilyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                FamilyRepository repos = new FamilyRepository();
                var family =
                    repos.Create(
                    db, 
                    model.FamilyName,
                    GetCurrentUser(),
                    model.KidName, 
                    model.RelationshipOfOwnerToKid);

                return RedirectToAction("Details", new { id = family.Id }); 
            }

            return View(model);
        }
        
        //
        // GET: /Family/Edit/5
 
        public ActionResult Edit(long id)
        {
            Family family = db.Families.Find(id);
            return View(family);
        }

        //
        // POST: /Family/Edit/5

        [HttpPost]
        public ActionResult Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(family);
        }

        //
        // GET: /Family/Delete/5
 
        public ActionResult Delete(long id)
        {
            Family family = db.Families.Find(id);
            return View(family);
        }

        //
        // POST: /Family/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            Family family = db.Families.Find(id);
            db.Families.Remove(family);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}