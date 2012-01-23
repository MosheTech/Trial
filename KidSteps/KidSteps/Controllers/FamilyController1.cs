using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidSteps.Controllers
{
    public class FamilyController1 : Controller
    {
        //
        // GET: /Family/

        public ActionResult Index()
        {
            Repository.FamilyRepository repo = 
                new Repository.FamilyRepository(new Repository.KidStepsContextFactory());
            return View(repo.All());
        }

        //
        // GET: /Family/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Create(Models.Family collection)
        {
            try
            {
                // TODO: Add insert logic here
                Repository.FamilyRepository repo =
                    new Repository.FamilyRepository(new Repository.KidStepsContextFactory());
                var family = repo.CreateInstance();
                family.Name = collection.Name;
                repo.SaveAll();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Family/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Family/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Family/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Family/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
