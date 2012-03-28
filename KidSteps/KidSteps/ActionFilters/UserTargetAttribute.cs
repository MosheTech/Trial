using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.DAL;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public class UserTargetAttribute : TargetedAttribute //: Mvc.ActionFilterAttribute
    {
        public UserTargetAttribute(Permission permission)
            : base(permission)
        {
        }

        //public override void OnActionExecuting(Mvc.ActionExecutingContext filterContext)
        //{
        //    // set properties target user and family

        //    if (!filterContext.RouteData.Values.ContainsKey("id"))
        //        throw new ArgumentException("Missing User Id");

        //    int userId;
        //    bool hasUserId = int.TryParse(filterContext.RouteData.Values["id"].ToString(), out userId);

        //    if (!hasUserId)
        //        throw new ArgumentException("Missing User Id");

        //    ControllerBase controller = filterContext.Controller as ControllerBase;

        //    if (controller == null)
        //        throw new ArgumentException("Controller did not inherit from custom ControllerBase");

        //    controller.SetTargetUser(userId);

        //    if (controller.CurrentUser.DefaultFamilyId.HasValue)
        //        controller.SetTargetFamily(controller.CurrentUser.DefaultFamilyId.Value);

        //    // check authorization
        //    bool authorized = false;

        //    bool isTargetUser = controller.CurrentUser.Id == controller.TargetUser.Id;
        //    bool isInSameFamilyAsTargetUser =
        //        controller.CurrentUser.DefaultFamily != null &&
        //        controller.TargetUser.DefaultFamily != null &&
        //        controller.CurrentUser.DefaultFamily.Id == controller.TargetUser.DefaultFamily.Id;

        //    // superuser is always authorized
        //    if (controller.CurrentUser.IsSuperUser)
        //        authorized = true;

        //    switch (Permission)
        //    {
        //            // anyone in the family can read
        //        case Permission.ReadUser:
        //            if (isTargetUser || isInSameFamilyAsTargetUser)
        //                authorized = true;
        //            break;
        //            // only target user can update
        //        case Permission.UpdateUser:
        //            if (isTargetUser)
        //                authorized = true;
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }

        //    if (!authorized)
        //        filterContext.Result = new Mvc.HttpUnauthorizedResult();

        //    base.OnActionExecuting(filterContext);
        //}

        //public Permission Permission { get; set; }

        protected override void Initialize(ControllerBase controller)
        {
            _controller = controller as TargetedController<User>;

            if (_controller == null)
                throw new ArgumentException("Controller was not a TargetedController<User>");
        }

        protected override void SetTarget(int id)
        {
            _controller.Target = _controller.Context.Members.Find(id);

            if (_controller.Target == null)
                throw new ArgumentException("Target user not found");
        }

        protected override bool IsAuthorized()
        {
            bool isTargetUser = _controller.CurrentUser.Id == _controller.Target.Id;
            bool isInSameFamilyAsTargetUser =
                _controller.CurrentUser.DefaultFamily != null &&
                _controller.Target.DefaultFamily != null &&
                _controller.CurrentUser.DefaultFamily.Id == _controller.Target.DefaultFamily.Id;

            // superuser is always authorized
            if (_controller.CurrentUser.IsSuperUser)
                return true;

            switch (Permission)
            {
                // anyone in the family can read
                case Permission.ReadUser:
                    return (isTargetUser || isInSameFamilyAsTargetUser);
                // only target user can update
                case Permission.UpdateUser:
                    return isTargetUser;
                default:
                    throw new NotImplementedException();
            }
        }

        private TargetedController<User> _controller;
    }
}