using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using KidSteps.ViewModels;

namespace KidSteps.Controllers
{
    public partial class LayoutController : ControllerBase
    {
        [ChildActionOnly]
        public virtual ActionResult Menu()
        {
            LayoutMenuViewModel model = new LayoutMenuViewModel();
            if (CurrentUser != null)
                model.DefaultFamily = CurrentUser.DefaultFamily;

            return PartialView(model);
        }

    }
}
