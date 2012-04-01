using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public class MyAuthorizeAttribute : Mvc.ActionFilterAttribute
    {
        public MyAuthorizeAttribute(Permission permission)
        {
            Permission = permission;
        }

        public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        {
            ControllerBase controller = filterContext.Controller as ControllerBase;
            User user = controller.CurrentUser;
            if (user == null || !user.IsAllowedTo(Permission))
                filterContext.Result = new Mvc.HttpUnauthorizedResult();

            base.OnActionExecuting(filterContext);
        }

        public Permission Permission { get; private set; }
    }
}