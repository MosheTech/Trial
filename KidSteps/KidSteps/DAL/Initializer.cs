using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KidSteps.Models;
using System.Web.Management;
using System.Web.Configuration;

namespace KidSteps.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<KidStepsContext>
    {
        protected override void Seed(KidStepsContext context)
        {
            SqlServices.Install(
                "KidSteps",
                SqlFeatures.Membership | SqlFeatures.RoleManager | SqlFeatures.Profile,
                WebConfigurationManager.ConnectionStrings["KidStepsContext"].ConnectionString);

            var families = new List<Family>()
            {
                new Family() { Id = "Example1", Name = "Example1" },
                new Family() { Id = "Example2", Name = "Example2" }
            };
            foreach (var family in families)
            {
                context.Families.Add(family);
            }
            context.SaveChanges();
        }
    }
}