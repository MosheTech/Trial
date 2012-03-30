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
    public class UserTargetAttribute : TargetedAttribute
    {
        public UserTargetAttribute(Permission permission)
            : base(permission)
        {
        }

        protected override void Initialize(ControllerBase controller)
        {
            _controller = controller as TargetedController<User>;

            if (_controller == null)
                throw new ArgumentException("Controller was not a TargetedController<User>");
        }

        protected override void SetTarget(int id)
        {
            _controller.Target = _controller.Context.Users.Find(id);

            if (_controller.Target == null)
                throw new ArgumentException("Target user not found");
        }

        protected override bool IsAuthorized()
        {
            return _controller.CurrentUser.IsAllowedTo(Permission, _controller.Target);

            //bool isTargetUser = _controller.CurrentUser.Id == _controller.Target.Id;
            //bool isInSameFamilyAsTargetUser =
            //    _controller.CurrentUser.DefaultFamily != null &&
            //    _controller.Target.DefaultFamily != null &&
            //    _controller.CurrentUser.DefaultFamily.Id == _controller.Target.DefaultFamily.Id;
            //bool isFamilyAdminAndTargetIsUnregistered =
            //    _controller.Target.DefaultFamily != null &&
            //    _controller.Target.DefaultFamily.Owner.Id == _controller.CurrentUser.Id &&
            //    _controller.Target.IsUnregisteredMember;
                

            //// superuser is always authorized
            //if (_controller.CurrentUser.IsSuperUser)
            //    return true;

            //switch (Permission)
            //{
            //    // anyone in the family can read
            //    case Permission.ReadUser:
            //        return (isTargetUser || isInSameFamilyAsTargetUser);
            //    // only target user can update
            //    case Permission.UpdateUser:
            //        return isTargetUser || isFamilyAdminAndTargetIsUnregistered;
            //    default:
            //        throw new NotImplementedException();
            //}
        }

        private TargetedController<User> _controller;
    }
}