using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using KidSteps.ViewModels;
using KidSteps.ActionFilters;

namespace KidSteps.Controllers
{
    public partial class LayoutController : ControllerBase
    {
        [ChildActionOnly]
        public virtual ActionResult Menu()
        {
            LayoutMenuViewModel model = new LayoutMenuViewModel();
            if (CurrentUser != null)
            {
                model.Family = CurrentUser.Family;
                model.CurrentUser = CurrentUser;
            }

            return PartialView(model);
        }

        [ChildActionOnly]
        public virtual ActionResult AccountBar()
        {
            return PartialView(CurrentUser);
        }

    }
}
