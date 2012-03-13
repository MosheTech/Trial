using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using KidSteps.ViewModels;

namespace KidSteps.Controllers
{
    public class LayoutController : ControllerBase
    {
        [ChildActionOnly]
        public ActionResult Menu()
        {
            User currentUser = GetCurrentUser();
            LayoutMenuViewModel model = new LayoutMenuViewModel();
            if (currentUser != null)
                model.DefaultFamily = currentUser.DefaultFamily;

            return PartialView(model);
        }

    }
}
