using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.Web.Security;
using KidSteps.ViewModels;
using KidSteps.ActionFilters;

namespace KidSteps.Controllers
{
    public partial class HomeController : ControllerBase
    {
        public virtual ActionResult Index()
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel();
            if (CurrentUser != null)
            {
                viewModel.CurrentUser = CurrentUser;
                if (viewModel.CurrentUser == null)
                {
                    viewModel.IsLoggedOn = false;
                    return View(viewModel);
                }
                viewModel.IsLoggedOn = true;

                if (viewModel.CurrentUser.IsPublicViewer)
                    ViewBag.Message = "Welcome!";
                else
                    ViewBag.Message = string.Format("Welcome {0}!", viewModel.CurrentUser.Name.First);
            }
            else
            {
                viewModel.IsLoggedOn = false;
                ViewBag.Message = "Welcome to Kid Steps!";
            }

            var xyz = db.Images.ToList();

            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Index(HomeIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.FirstOrDefault(m => m.Email == model.LogOnModel.Email);

                if (u != null && Membership.ValidateUser(u.Id.ToString(), model.LogOnModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), model.LogOnModel.RememberMe);
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

        public virtual ActionResult About()
        {
            return View();
        }
    }
}
