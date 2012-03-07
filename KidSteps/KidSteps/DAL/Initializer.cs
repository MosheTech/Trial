using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KidSteps.Models;
using System.Web.Management;
using System.Web.Configuration;
using System.Web.Security;

namespace KidSteps.DAL
{
    public class Initializer : DropCreateDatabaseAlways<KidStepsContext>
    {
        protected override void Seed(KidStepsContext context)
        {
            SqlServices.Install(
                "KidSteps",
                SqlFeatures.Membership | SqlFeatures.RoleManager | SqlFeatures.Profile,
                WebConfigurationManager.ConnectionStrings["KidStepsContext"].ConnectionString);

            MembershipCreateStatus createStatus;
            Membership.CreateUser("admin", "admin", "admin", null, null, true, null, out createStatus);
            User admin = new User();
            context.Members.Add(admin);
            admin.Id = "admin";
            admin.Name = new PersonName() { First = "Pinchas", Last = "Friedman" };
            context.SaveChanges();

            Roles.CreateRole("Admin");
            Roles.CreateRole("User");
            Roles.CreateRole("UserWithFamily");

            Roles.AddUserToRole("admin", "admin");

            var families = new List<Family>()
            {
                new Family() { Name = "Example1", Owner = admin },
                new Family() { Name = "Example2", Owner = admin }
            };
            foreach (var family in families)
            {
                context.Families.Add(family);
            }
            context.SaveChanges();
        }
    }
}