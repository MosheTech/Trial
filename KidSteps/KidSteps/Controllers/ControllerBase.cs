using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KidSteps.Models;
using System.Security.Principal;

namespace KidSteps.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected bool VerifyCurrentUser(string id)
        {
            return User.Identity.Name == id;
        }

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
