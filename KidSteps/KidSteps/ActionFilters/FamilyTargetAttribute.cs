using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public class FamilyTargetAttribute : Mvc.ActionFilterAttribute
    {
        public FamilyTargetAttribute(Permission permission)
            : base()
        {
            Permission = permission;
        }

        public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        {
            // set properties target user and family

            if (!filterContext.RouteData.Values.ContainsKey("id"))
                throw new ArgumentException("Missing Family Id");

            int familyId;
            bool hasFamilyId = int.TryParse(filterContext.RouteData.Values["id"].ToString(), out familyId);

            if (!hasFamilyId)
                throw new ArgumentException("Missing Family Id");

            ControllerBase controller = filterContext.Controller as ControllerBase;

            if (controller == null)
                throw new ArgumentException("Controller did not inherit from custom ControllerBase");

            controller.SetTargetFamily(familyId);

            // check authorization
            bool authorized = false;

            bool isFamilyAdmin = controller.CurrentUser.Id == controller.TargetFamily.OwnerId;
            bool isInTargetFamily = controller.CurrentUser.DefaultFamilyId == controller.TargetFamily.Id;
            bool isRegisteredMemberOfTargetFamily =
                (isInTargetFamily && controller.CurrentUser.RoleFlags.HasFlag(Role.RegisteredMember)) || isFamilyAdmin;

            // superuser is always authorized
            if (controller.CurrentUser.IsSuperUser)
                authorized = true;

            switch (Permission)
            {
                // anyone in the family, including public viewer, can view the family
                case Models.Permission.ViewFamily:
                    if (isInTargetFamily)
                        authorized = true;
                    break;
                // only family admin can add a family member
                case Permission.AddFamilyMember:
                    if (isFamilyAdmin)
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