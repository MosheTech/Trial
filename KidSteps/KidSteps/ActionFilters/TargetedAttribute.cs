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

            Initialize(filterContext.Controller as ControllerBase);

            SetTarget(id);

            if (!IsAuthorized())
                filterContext.Result = new Mvc.HttpUnauthorizedResult();

            //ITargetedController controller = filterContext.Controller as ITargetedController;

            //if (controller == null)
            //    throw new ArgumentException("Controller did not implement ITargetedController");

            //string targetType = controller.TargetType.Name;

            //if (targetType == typeof(User).Name)
            //{
                
            //}

            //switch(controller.TargetType.Name)
            //{
            //    case typeof(User).Name:
            //        break;
            //    default:
            //        throw new NotImplementedException();
            //}

            //controller.SetTargetUser(userId);

            //if (controller.CurrentUser.DefaultFamilyId.HasValue)
            //    controller.SetTargetFamily(controller.CurrentUser.DefaultFamilyId.Value);

            //// check authorization
            //bool authorized = false;

            //bool isTargetUser = controller.CurrentUser.Id == controller.TargetUser.Id;
            //bool isInSameFamilyAsTargetUser =
            //    controller.CurrentUser.DefaultFamily != null &&
            //    controller.TargetUser.DefaultFamily != null &&
            //    controller.CurrentUser.DefaultFamily.Id == controller.TargetUser.DefaultFamily.Id;

            //// superuser is always authorized
            //if (controller.CurrentUser.IsSuperUser)
            //    authorized = true;

            //switch (Permission)
            //{
            //    // anyone in the family can read
            //    case Permission.ReadUser:
            //        if (isTargetUser || isInSameFamilyAsTargetUser)
            //            authorized = true;
            //        break;
            //    // only target user can update
            //    case Permission.UpdateUser:
            //        if (isTargetUser)
            //            authorized = true;
            //        break;
            //    default:
            //        throw new NotImplementedException();
            //}

            //if (!authorized)
            //    filterContext.Result = new Mvc.HttpUnauthorizedResult();

            base.OnActionExecuting(filterContext);
        }

        protected abstract void Initialize(ControllerBase controller);

        protected abstract void SetTarget(int id);

        protected abstract bool IsAuthorized();

        //private void SetUserTarget()
        //{
            
        //}

        //private bool IsUserAuthorized()
        //{
        //    throw new NotImplementedException();
        //}

        public Permission Permission { get; private set; }
    }
}