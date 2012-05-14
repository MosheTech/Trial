using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidSteps.Controllers
{
    public partial class FamilyFeedController : Controller
    {
        //
        // GET: /FamilyFeed/

        public virtual ActionResult Timeline()
        {
            return View();
        }

        public virtual ActionResult Activity()
        {
            return View();
        }
    }
}
