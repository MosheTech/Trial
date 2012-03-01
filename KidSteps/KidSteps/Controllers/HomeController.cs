using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.Web.Security;

namespace KidSteps.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRepo = new DAL.UserRepository();
                var user = userRepo.FindByMembership(db, User);
                ViewBag.Message = string.Format("Welcome {0}!", user.Name.First);
            }
            else
            {
                ViewBag.Message = "Welcome to Kid Steps!";
            }

            var xyz = db.Images.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
