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
        
        public User Create(KidStepsContext dbContext, string firstName, string lastName, string email, string password, Role role, out MembershipCreateStatus status)
        {
            PersonName name = new PersonName() {First = firstName, Last = lastName};
            return Create(dbContext, name, email, password, role, out status);
        }


        public User Create(KidStepsContext dbContext, PersonName name, string email, string password, Role role, out MembershipCreateStatus status)
        {
            // Attempt to register the user
            MembershipCreateStatus createStatus;
            Membership.CreateUser(email, password, email, null, null, true, null, out createStatus);

            status = createStatus;

            if (createStatus == MembershipCreateStatus.Success)
            {
                User user = new User();
                dbContext.Members.Add(user);
                user.Id = email;
                user.Name = name;
                user.HasAccount = true;
                try
                {
                    dbContext.SaveChanges();
                    Roles.AddUserToRole(user.Id, role.ToString());
                    return user;
                }
                catch
                {
                    Membership.DeleteUser(email, true);
                    throw;
                }
            }

            return null;
        }

        public User CreateUnregisteredUser(KidStepsContext dbContext, string firstName, string lastName, out MembershipCreateStatus status)
        {
            PersonName name = new PersonName() {First = firstName, Last = lastName};
            return CreateUnregisteredUser(dbContext, name, out status);
        }

        public User CreateUnregisteredUser(KidStepsContext dbContext, PersonName name, out MembershipCreateStatus status)
        {
            string password = Membership.GeneratePassword(30, 3);
            string email = Guid.NewGuid().ToString();

            // Attempt to register the user
            MembershipCreateStatus createStatus;
            Membership.CreateUser(email, password, email, null, null, true, null, out createStatus);

            status = createStatus;

            if (createStatus == MembershipCreateStatus.Success)
            {
                User user = new User();
                dbContext.Members.Add(user);
                user.Id = email;
                user.Name = name;
                user.HasAccount = false;
                try
                {
                    dbContext.SaveChanges();
                    Roles.AddUserToRole(user.Id, Role.UnregisteredFamilyMember.ToString());
                    return user;
                }
                catch
                {
                    Membership.DeleteUser(email, true);
                    throw;
                }
            }

            return null;
            
        }
    }
}