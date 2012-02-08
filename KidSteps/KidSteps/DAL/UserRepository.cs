using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.Security.Principal;

namespace KidSteps.DAL
{
    public class UserRepository
    {


        public User FindByMembership(KidStepsContext dbContext, IPrincipal membershipUser)
        {
            User user = dbContext.Members.SingleOrDefault(
                u => u.Id == membershipUser.Identity.Name);
            return user;
        }

        public User Create(KidStepsContext dbContext, IPrincipal membershipUser)
        {
            User user = new User();
            user.Id = membershipUser.Identity.Name;
            return user;
        }
    }
}