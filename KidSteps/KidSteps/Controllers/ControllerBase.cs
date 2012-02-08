using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.Security.Principal;

namespace KidSteps.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected IPrincipal MembershipUser
        {
            get { return User; }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        protected KidStepsContext db = new KidStepsContext();
    }
}
