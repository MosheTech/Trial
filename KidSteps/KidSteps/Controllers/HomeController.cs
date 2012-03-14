﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.Web.Security;
using KidSteps.ViewModels;

namespace KidSteps.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel();
            if (User.Identity.IsAuthenticated)
            {
                viewModel.CurrentUser = GetCurrentUser();
                viewModel.IsLoggedOn = true;
                viewModel.UserDefaultFamily = viewModel.CurrentUser.DefaultFamily;
                if (viewModel.UserDefaultFamily != null)
                {
                    viewModel.FamilyMembers =
                        db.FamilyMembers.Where(fm => fm.Family.Id == viewModel.UserDefaultFamily.Id).ToList();
                }

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
        public ActionResult Index(HomeIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.LogOnModel.Email, model.LogOnModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.LogOnModel.Email, model.LogOnModel.RememberMe);
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
    }
}
