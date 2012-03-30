using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc = System.Web.Mvc;
using KidSteps.Controllers;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public class FamilyTargetAttribute : TargetedAttribute
    {
        public FamilyTargetAttribute(Permission permission)
            : base(permission)
        {
        }

        protected override void Initialize(ControllerBase controller)
        {
            _controller = controller as TargetedController<Family>;

            if (_controller == null)
                throw new ArgumentException("Controller was not a TargetedController<Family>");
        }

        protected override void SetTarget(int id)
        {
            _controller.Target = _controller.Context.Families.Find(id);

            if (_controller.Target == null)
                throw new ArgumentException("Target not found");
        }

        protected override bool IsAuthorized()
        {
            return _controller.CurrentUser.IsAllowedTo(Permission, _controller.Target);

            //bool isFamilyAdmin = _controller.CurrentUser.Id == _controller.Target.OwnerId;
            //bool isInTargetFamily = _controller.CurrentUser.DefaultFamilyId == _controller.Target.Id;
            //bool isRegisteredMemberOfTargetFamily =
            //    (isInTargetFamily && _controller.CurrentUser.RoleFlags.HasFlag(Role.RegisteredMember)) || isFamilyAdmin;

            //// superuser is always authorized
            //if (_controller.CurrentUser.IsSuperUser)
            //    return true;

            //switch (Permission)
            //{
            //    // anyone in the family, including public viewer, can view the family
            //    case Models.Permission.ViewFamily:
            //        return isInTargetFamily;
            //    // only family admin can add a family member
            //    case Permission.EditFamily:
            //        return isFamilyAdmin;
            //    default:
            //        throw new NotImplementedException();
            //}
        }

        private TargetedController<Family> _controller;
    }
}