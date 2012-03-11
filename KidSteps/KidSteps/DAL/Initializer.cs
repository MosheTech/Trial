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
            // install/initialize asp.net membership services
            SqlServices.Install(
                "KidSteps",
                SqlFeatures.Membership | SqlFeatures.RoleManager | SqlFeatures.Profile,
                WebConfigurationManager.ConnectionStrings["KidStepsContext"].ConnectionString);
            foreach (Models.Role role in Enum.GetValues(typeof(Models.Role)))
                Roles.CreateRole(role.ToString());

            // create admin and example users
            UserRepository userRepos = new UserRepository();
            MembershipCreateStatus _;

            Models.RegisterModel registerModel = 
                new RegisterModel()
                {
                    Name = new PersonName() { First = "Pinchas", Last = "Friedman" },
                    Email = "admin",
                    Password = "admin",
                    ConfirmPassword = "admin",
                    RememberMe = false
                };
            User admin = userRepos.Create(context, registerModel, Role.SuperUser, out _);
            registerModel =
                new RegisterModel()
                {
                    Name = new PersonName() { First = "Moshe", Last = "Starkman" },
                    Email = "moshe",
                    Password = "moshe",
                    ConfirmPassword = "moshe",
                    RememberMe = false
                };
            userRepos.Create(context, registerModel, Role.SuperUser, out _);
            registerModel =
                new RegisterModel()
                {
                    Name = new PersonName() { First = "Test", Last = "User" },
                    Email = "test@user.com",
                    Password = "test",
                    ConfirmPassword = "test",
                    RememberMe = false
                };
            userRepos.Create(context, registerModel, Role.FamilyAdmin, out _);

            var families = new List<Family>()
            {
                new Family() { Name = "Example1", Owner = admin }
            };
            foreach (var family in families)
            {
                context.Families.Add(family);
            }
            context.SaveChanges();
        }
    }
}