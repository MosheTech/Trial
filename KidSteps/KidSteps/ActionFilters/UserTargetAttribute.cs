using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public class UserTargetAttribute : Mvc.ActionFilterAttribute
    {
        public UserTargetAttribute(Permission permission)
            : base()
        {
            Permission = permission;
        }

        public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        {
            // set properties target user and family

            if (!filterContext.ActionParameters.ContainsKey("id"))
                throw new ArgumentException("Missing User Id");

            int userId;
            bool hasUserId = int.TryParse(filterContext.ActionParameters["id"].ToString(), out userId);

            if (!hasUserId)
                throw new ArgumentException("Missing User Id");

            ControllerBase controller = filterContext.Controller as ControllerBase;

            if (controller == null)
                throw new ArgumentException("Controller did not inherit from custom ControllerBase");

            controller.SetTargetUser(userId);

            if (controller.CurrentUser.DefaultFamilyId.HasValue)
                controller.SetTargetFamily(controller.CurrentUser.DefaultFamilyId.Value);

            // check authorization
            bool authorized = false;

            if (controller.CurrentUser.IsSuperUser)
                authorized = true;
            bool isTargetUser = controller.CurrentUser.Id == controller.TargetUser.Id;
            bool isInSameFamilyAsTargetUser =
                controller.CurrentUser.DefaultFamily != null &&
                controller.TargetUser.DefaultFamily != null &&
                controller.CurrentUser.DefaultFamily.Id == controller.TargetUser.DefaultFamily.Id;

            switch (Permission)
            {
                case Permission.Read:
                    if (isTargetUser || isInSameFamilyAsTargetUser)
                        authorized = true;
                    break;
                case Permission.Update:
                    if (isTargetUser)
                        authorized = true;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (!authorized)
                filterContext.Result = new Mvc.HttpUnauthorizedResult();

            base.OnActionExecuting(filterContext);
        }

        public Permission Permission { get; set; }
    }
}