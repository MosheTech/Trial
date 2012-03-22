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
    public class Initializer : DropCreateDatabaseIfModelChanges<KidStepsContext>
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

            // todo: unique constraint for email

            // create admin users
            UserRepository userRepos = new UserRepository();
            MembershipCreateStatus _;
            User pinchas = userRepos.Create(context, "Pinchas", "Friedman", "admin", "admin", Role.SuperUser, out _);
            User moshe = userRepos.Create(context, "Moshe", "Starkman", "moshe", "moshe", Role.SuperUser, out _);

            // create add admin users' relationships to kids
            FamilyRepository familyRepos = new FamilyRepository();
            familyRepos.AddRelationship(context, pinchas, RelationshipType.Father);
            familyRepos.AddRelationship(context, moshe, RelationshipType.Father);
            
            // add family members
            familyRepos.AddUnregisteredMember(context, pinchas.DefaultFamily, "Shalom", "Friedman", null, RelationshipType.Self);
            familyRepos.AddUnregisteredMember(context, pinchas.DefaultFamily, "Yael", "Friedman", null, RelationshipType.Mother);
        }
    }
}