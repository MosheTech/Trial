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
                user = dbContext.Users.Find(id);
            return user;
        }
        
        //public User Create(KidStepsContext dbContext, string firstName, string lastName, string email, string password, Role role, out MembershipCreateStatus status)
        //{
        //    PersonName name = new PersonName() {First = firstName, Last = lastName};
        //    return Create(dbContext, name, email, password, role, out status);
        //}

        //public User Create(KidStepsContext dbContext, PersonName name, string email, string password, Role role, out MembershipCreateStatus status)
        //{
        //    return
        //        Create(
        //            dbContext,
        //            null,
        //            name,
        //            email,
        //            password,
        //            role,
        //            out status);
        //}

        //public MembershipCreateStatus CreateAccountForUser(
        //    KidStepsContext dbContext, 
        //    User user,
        //    string email, 
        //    string password)
        //{
        //    MembershipCreateStatus createStatus;
        //    Create(
        //        dbContext,
        //        user,
        //        user.Name,
        //        email,
        //        password,
        //        Role.RegisteredMember,
        //        out createStatus);
        //    return createStatus;
        //}






        public User Cre(KidStepsContext context, PersonName name, string email, string password, out MembershipCreateStatus status)
        {
            // create user
            User user = Cre(context, name, Role.MemberOfFamily | Role.Registered | Role.FamilyAdmin, email);

            // register user
            status = Register(context, user, password);

            // create family for user
            FamilyRepository familyRepos = new FamilyRepository();
            familyRepos.Create(context, user);

            return user;
        }

        public User CreFamilyMember(KidStepsContext context, PersonName name, string email)
        {
            return Cre(context, name, Role.MemberOfFamily, email);
        }

        public User CrePublicViewer(KidStepsContext context)
        {
            return Cre(context, new PersonName("Public", "Viewer"), Role.PublicViewer, null);
        }

        private User Cre(KidStepsContext context, PersonName name, Role role, string email)
        {
            User user = new User();
            context.Users.Add(user);
            user.Name = name;

            string emailToSave = email;
            if (string.IsNullOrWhiteSpace(emailToSave))
                emailToSave = Guid.NewGuid().ToString();
            else
                user.HasRealEmail = true;
            user.Email = emailToSave;

            string invitationCode = Guid.NewGuid().ToString();
            user.InvitationCode = invitationCode;

            user.RoleFlags = role;

            context.SaveChanges();
            return user;
        }

        public MembershipCreateStatus Register(KidStepsContext context, User user, string password)
        {
            user.RoleFlags |= Role.Registered;
            user.HasRealEmail = true;
            context.SaveChanges();

            // Attempt to register the user
            MembershipCreateStatus createStatus;
            Membership.CreateUser(user.Id.ToString(), password, user.Email, null, null, true, null, out createStatus);

            return createStatus;
        }






        //public User CreateUserWithoutAccount(KidStepsContext dbContext, string firstName, string lastName, string email)
        //{
        //    PersonName name = new PersonName() {First = firstName, Last = lastName};
        //    return CreateUserWithoutAccount(dbContext, name, email);
        //}

        //public User CreateUserWithoutAccount(KidStepsContext dbContext, PersonName name, string email)
        //{
        //    User user = new User();
        //    dbContext.Users.Add(user);
        //    user.Name = name;
        //    user.HasAccount = false;
        //    user.RoleFlags = Role.UnregisteredMember;

        //    string emailToSave = email;
        //    if (string.IsNullOrWhiteSpace(emailToSave))
        //        emailToSave = Guid.NewGuid().ToString();
        //    else
        //        user.HasRealEmail = true;
        //    user.Email = emailToSave;

        //    string invitationCode = Guid.NewGuid().ToString();
        //    user.InvitationCode = invitationCode;

        //    dbContext.SaveChanges();
        //    return user;
        //}

        //public User CreatePublicViewer(KidStepsContext dbContext, out MembershipCreateStatus status)
        //{
        //    string email = Guid.NewGuid().ToString();
        //    string password = Membership.GeneratePassword(20, 3);

        //    return
        //        Create(dbContext, "Public", "Viewer", email, password, Role.PublicViewer, out status);
        //}

        //private User Create(KidStepsContext dbContext, User existingUser, PersonName name, string email, string password, Role role, out MembershipCreateStatus status)
        //{
        //    User user = existingUser;
        //    if (user == null)
        //    {
        //        user = new User();
        //        dbContext.Users.Add(user);
        //    }
        //    user.Email = email;
        //    user.Name = name;
        //    user.InvitationCode = Guid.NewGuid().ToString();
        //    if (role.HasFlag(Role.PublicViewer))
        //        user.HasAccount = false;
        //    else
        //    {
        //        user.HasAccount = true;
        //        user.HasRealEmail = true;
        //    }
        //    user.RoleFlags = role;
        //    dbContext.SaveChanges();

        //    // Attempt to register the user
        //    MembershipCreateStatus createStatus;
        //    Membership.CreateUser(user.Id.ToString(), password, email, null, null, true, null, out createStatus);

        //    status = createStatus;

        //    // if this was a brand new user, create a family for that user
        //    if (existingUser == null && !user.IsPublicViewer)
        //    {
        //        FamilyRepository familyRepos = new FamilyRepository();
        //        familyRepos.Create(dbContext, user);
        //    }

        //    return user;
        //}
    }
}