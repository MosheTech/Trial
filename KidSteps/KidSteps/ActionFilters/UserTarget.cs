using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public class UserTarget : Mvc.ActionFilterAttribute
    {
        public enum Authorization
        {
            Read,
            Edit,
            Delete
        }

        public UserTarget(Authorization authorization)
            : base()
        {
            AuthorizationType = authorization;
        }

        public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        {
            // set properties on controller

            int userId;
            bool hasUserId = int.TryParse(filterContext.ActionParameters["id"].ToString(), out userId);

            if (!hasUserId)
                throw new ArgumentException("Missing User Id");

            ControllerBase controller = filterContext.Controller as ControllerBase;

            if (controller == null)
                throw new ArgumentException("Controller did not inherit from custom ControllerBase");

            controller.SetTargetUser(userId);

            // check authorization
            bool authorized = false;

            if (controller.CurrentUser.IsSuperUser)
                authorized = true;

            switch (AuthorizationType)
            {
                case Authorization.Edit:
                    if (controller.CurrentUser.Id == controller.TargetUser.Id)
                        authorized = true;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (!authorized)
                filterContext.Result = new Mvc.HttpUnauthorizedResult();

            base.OnActionExecuting(filterContext);
        }

        public Authorization AuthorizationType { get; set; }
    }
}