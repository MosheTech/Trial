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

        public ViewResult Details(string id)
        {
            Family family = db.Families.Find(id);
            return View(family);
        }

        //
        // GET: /Family/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Family/Create

        [HttpPost]
        public ActionResult Create(FamilyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Family family = new Family();
                db.Families.Add(family);
                family.Name = model.FamilyName;
                var currentUser = GetCurrentUser();
                family.Owner = currentUser;
                family.Members.Add(family.Owner);

                db.SaveChanges();

                return RedirectToAction("Edit", new { id = family.Id }); 
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
 
        public ActionResult Delete(string id)
        {
            Family family = db.Families.Find(id);
            return View(family);
        }

        //
        // POST: /Family/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
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