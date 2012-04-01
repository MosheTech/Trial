using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public abstract class TargetedAttribute : Mvc.ActionFilterAttribute
    {
        public TargetedAttribute(Permission permission)
            : base()
        {
            Permission = permission;
        }

        public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        {
            // set properties target user and family

            if (!filterContext.RouteData.Values.ContainsKey("id"))
                throw new ArgumentException("Missing Id");

            int id;
            bool hasId = int.TryParse(filterContext.RouteData.Values["id"].ToString(), out id);

            if (!hasId)
                throw new ArgumentException("Missing Id");

            filterContext.Controller.ViewData["id"] = id;

            Initialize(filterContext.Controller as ControllerBase);

            SetTarget(id);

            if (!IsAuthorized())
                filterContext.Result = new Mvc.HttpUnauthorizedResult();

            base.OnActionExecuting(filterContext);
        }

        protected abstract void Initialize(ControllerBase controller);

        protected abstract void SetTarget(int id);

        protected abstract bool IsAuthorized();

        public Permission Permission { get; private set; }
    }
}