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
    public class Initializer : DropCreateDatabaseAlways<KidStepsContext>
    {
        protected override void Seed(KidStepsContext context)
        {
            SqlServices.Install(
                "KidStepsEntity",
                SqlFeatures.Membership | SqlFeatures.RoleManager | SqlFeatures.Profile,
                WebConfigurationManager.ConnectionStrings["KidStepsContext"].ConnectionString);
            var families = new List<Family>()
            {
                new Family() { FamilyId = "Friedman", Name = "Friedman" },
                new Family() { FamilyId = "Sussman", Name = "Sussman" }
            };
            foreach (var family in families)
            {
                context.Families.Add(family);
            }
            context.SaveChanges();
        }
    }
}