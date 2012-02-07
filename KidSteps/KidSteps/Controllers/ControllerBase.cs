using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;

namespace KidSteps.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        protected KidStepsContext db = new KidStepsContext();
    }
}
