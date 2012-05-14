using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidSteps.Controllers
{
    public class FamilyFeedController : Controller
    {
        //
        // GET: /FamilyFeed/

        public ActionResult Timeline()
        {
            return View();
        }

        public ActionResult Activity()
        {
            return View();
        }
    }
}
