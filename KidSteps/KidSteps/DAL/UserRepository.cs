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
            return
                Create(
                    dbContext,
                    null,
                    name,
                    email,
                    password,
                    role,
                    out status);
        }

        public MembershipCreateStatus CreateAccountForUser(
            KidStepsContext dbContext, 
            User user,
            string email, 
            string password)
        {
            MembershipCreateStatus createStatus;
            Create(
                dbContext,
                user,
                user.Name,
                email,
                password,
                Role.RegisteredMember,
                out createStatus);
            return createStatus;
        }

        public User CreateUserWithoutAccount(KidStepsContext dbContext, string firstName, string lastName)
        {
            PersonName name = new PersonName() {First = firstName, Last = lastName};
            return CreateUserWithoutAccount(dbContext, name);
        }

        public User CreateUserWithoutAccount(KidStepsContext dbContext, PersonName name)
        {
            string email = Guid.NewGuid().ToString();
            string invitationCode = Guid.NewGuid().ToString();

            User user = new User();
            dbContext.Members.Add(user);
            user.Id = email;
            user.Name = name;
            user.HasAccount = false;
            user.RoleFlags = Role.UnregisteredMember;
            user.InvitationCode = invitationCode;
            dbContext.SaveChanges();
            return user;
        }

        public User CreatePublicViewer(KidStepsContext dbContext, out MembershipCreateStatus status)
        {
            string id = Guid.NewGuid().ToString();
            string password = Membership.GeneratePassword(20, 3);

            return
                Create(dbContext, "Public", "Viewer", id, password, Role.PublicViewer, out status);
        }

        private User Create(KidStepsContext dbContext, User existingUser, PersonName name, string email, string password, Role role, out MembershipCreateStatus status)
        {
            // Attempt to register the user
            MembershipCreateStatus createStatus;
            Membership.CreateUser(email, password, email, null, null, true, null, out createStatus);

            status = createStatus;

            if (createStatus == MembershipCreateStatus.Success)
            {
                User user = existingUser;
                if (user == null)
                {
                    user = new User();
                    dbContext.Members.Add(user);
                }
                user.Id = email;
                user.Name = name;
                user.HasAccount = true;
                user.RoleFlags = role;
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
    }
}