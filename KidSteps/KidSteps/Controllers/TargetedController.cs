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
    public class TargetedController<T> : ControllerBase
    {
        public T Target { get; set; }
    }
}
