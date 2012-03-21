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
            int id;
            bool hasId = int.TryParse(membershipUser.Identity.Name, out id);
            User user = null;
            if (hasId)
                user = dbContext.Members.Find(id);
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
            user.Email = email;
            user.Name = name;
            user.HasAccount = false;
            user.RoleFlags = Role.UnregisteredMember;
            user.InvitationCode = invitationCode;
            dbContext.SaveChanges();
            return user;
        }

        public User CreatePublicViewer(KidStepsContext dbContext, out MembershipCreateStatus status)
        {
            string email = Guid.NewGuid().ToString();
            string password = Membership.GeneratePassword(20, 3);

            return
                Create(dbContext, "Public", "Viewer", email, password, Role.PublicViewer, out status);
        }

        private User Create(KidStepsContext dbContext, User existingUser, PersonName name, string email, string password, Role role, out MembershipCreateStatus status)
        {
            User user = existingUser;
            if (user == null)
            {
                user = new User();
                dbContext.Members.Add(user);
            }
            user.Email = email;
            user.Name = name;
            user.InvitationCode = Guid.NewGuid().ToString();
            if (role == Role.PublicViewer)
                user.HasAccount = false;
            else
                user.HasAccount = true;
            user.RoleFlags = role;
            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
            }




            // Attempt to register the user
            MembershipCreateStatus createStatus;
            Membership.CreateUser(user.Id.ToString(), password, email, null, null, true, null, out createStatus);
            Roles.AddUserToRole(user.Id.ToString(), role.ToString());

            status = createStatus;

            // if this was a brand new user, create a family for that user
            if (existingUser == null && !user.IsPublicViewer)
            {
                FamilyRepository familyRepos = new FamilyRepository();
                familyRepos.CreateForUser(dbContext, user);
            }

            return user;

            //if (createStatus == MembershipCreateStatus.Success)
            //{
            //    User user = existingUser;
            //    if (user == null)
            //    {
            //        user = new User();
            //        dbContext.Members.Add(user);
            //    }
            //    user.Id = email;
            //    user.Name = name;
            //    user.HasAccount = true;
            //    user.RoleFlags = role;
            //    try
            //    {                    
            //        dbContext.SaveChanges();
            //        Roles.AddUserToRole(user.Id, role.ToString());
            //        return user;
            //    }
            //    catch
            //    {
            //        Membership.DeleteUser(email, true);
            //        throw;
            //    }
            //}

            //return null;
        }
    }
}