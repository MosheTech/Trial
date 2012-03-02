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

        public User Create(KidStepsContext dbContext, string id)
        {
            User user = new User();
            user.Id = id;
            dbContext.Members.Add(user);
            return user;
        }
    }
}