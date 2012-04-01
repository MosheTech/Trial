﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ActionFilters
{
    public static class AuthorizationExtensions
    {
        public static bool IsAllowedTo(this User user, Permission permission, User target)
        {
            bool isTargetUser = user.Id == target.Id;
            bool isInSameFamilyAsTargetUser =
                user.Family != null &&
                target.Family != null &&
                user.Family.Id == target.Family.Id;
            bool isFamilyAdminAndTargetIsUnregistered =
                target.Family != null &&
                target.Family.Admin.Id == user.Id &&
                target.IsUnregisteredFamilyMember;


            // superuser is always authorized
            if (user.IsSuperUser)
                return true;

            switch (permission)
            {
                // anyone in the family can read
                case Permission.ReadUser:
                    return (isTargetUser || isInSameFamilyAsTargetUser);
                // only target user can update
                case Permission.UpdateUser:
                    return isTargetUser || isFamilyAdminAndTargetIsUnregistered;
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool IsAllowedTo(this User user, Permission permission, Family target)
        {
            bool isFamilyAdmin = user.Id == target.Admin.Id;
            bool isInTargetFamily = user.Family.Id == target.Id;
            bool isRegisteredMemberOfTargetFamily =
                (isInTargetFamily && user.IsRegistered) || isFamilyAdmin;

            // superuser is always authorized
            if (user.IsSuperUser)
                return true;

            switch (permission)
            {
                // anyone in the family, including public viewer, can view the family
                case Models.Permission.ViewFamily:
                    return isInTargetFamily;
                // only family admin can add a family member
                case Permission.EditFamily:
                    return isFamilyAdmin;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}