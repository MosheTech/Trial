using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.Security.Principal;
using System.Web.Security;

namespace KidSteps.DAL
{
    public class UserRepository
    {


        public User FindByMembership(KidStepsContext dbContext, IPrincipal membershipUser)
        {
            var user = dbContext.Members.Find(membershipUser.Identity.Name);
            return user;
        }

        public User Create(KidStepsContext dbContext, Models.RegisterModel model, Role role, out MembershipCreateStatus status)
        {
            // Attempt to register the user
            MembershipCreateStatus createStatus;
            Membership.CreateUser(model.Email, model.Password, model.Email, null, null, true, null, out createStatus);

            status = createStatus;

            if (createStatus == MembershipCreateStatus.Success)
            {
                User user = new User();
                dbContext.Members.Add(user);
                user.Id = model.Email;
                user.Name = model.Name;
                user.HasAccount = true;
                try
                {
                    dbContext.SaveChanges();
                    Roles.AddUserToRole(user.Id, role.ToString());
                    return user;
                }
                catch
                {
                    Membership.DeleteUser(model.Email, true);
                    throw;
                }
            }

            return null;
        }
    }
}