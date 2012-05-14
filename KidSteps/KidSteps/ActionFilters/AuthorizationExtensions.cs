using System;
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
            bool isFamilyAdmin =
                target.Family != null &&
                target.Family.Admin.Id == user.Id;
            bool isFamilyAdminAndTargetIsUnregistered =
                isFamilyAdmin &&
                target.IsUnregisteredFamilyMember;
            bool isFamilyMember =
                isInSameFamilyAsTargetUser &&
                !user.IsPublicViewer;


            // superuser is always authorized
            if (user.IsSuperUser)
                return true;

            switch (permission)
            {
                // anyone in the family can read
                case Permission.ReadUser:
                    return (isTargetUser || isInSameFamilyAsTargetUser);
                // only target user can read personal data and upload images
                case Permission.ReadUserPersonalData:
                    return isTargetUser;
                case Permission.UploadImage:
                    return isTargetUser;
                // only target user can update
                case Permission.UpdateUser:
                    return isTargetUser || isFamilyAdminAndTargetIsUnregistered;
                case Permission.EditFamily:
                    return isFamilyAdmin;
                case Permission.TextPost:
                    return isInSameFamilyAsTargetUser && !user.IsPublicViewer;
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

        public static bool IsAllowedTo(this User user, Permission permission, FeedItem feedItem)
        {
            if (user.IsSuperUser)
                return true;

            bool isInSameFamily =
                user.Family.Id == feedItem.SubjectUser.Family.Id;

            return isInSameFamily;
        }

        public static bool IsAllowedTo(this User user, Permission permission)
        {
            if (user.IsSuperUser)
                return true;

            return false;
        }
    }
}