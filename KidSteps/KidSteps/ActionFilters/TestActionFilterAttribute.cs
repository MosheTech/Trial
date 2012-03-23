using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;

namespace KidSteps.ActionFilters
{
    public class TestActionFilterAttribute : Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult();

            ControllerBase controller = filterContext.Controller as ControllerBase;

            if (controller == null)
                throw new ArgumentException("Controller did not inherit from custom ControllerBase");

            base.OnActionExecuting(filterContext);
        }
    }
}