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

            // create admin and example users
            UserRepository userRepos = new UserRepository();
            MembershipCreateStatus _;
            User pinchas = userRepos.Create(context, "Pinchas", "Friedman", "admin", "admin", Role.SuperUser, out _);
            User moshe = userRepos.Create(context, "Moshe", "Starkman", "moshe", "moshe", Role.SuperUser, out _);
            User testUser = userRepos.Create(context, "Test", "User", "fam", "fam", Role.FamilyAdmin, out _);

            // create example families with intial kid
            FamilyRepository familyRepos = new FamilyRepository();
            Family starkman = familyRepos.Create(context, "Starkman", testUser, "Shlomo", "Starkman", RelationshipType.Uncle);
            Family friedman = familyRepos.Create(context, "Friedman", pinchas, "Chaim", "Friedman", RelationshipType.Father);
            // add family members
            familyRepos.AddMember(context, starkman, moshe, RelationshipType.Father, true);
            familyRepos.AddUnregisteredMember(context, friedman, "Shalom", "Friedman", RelationshipType.Self);
            familyRepos.AddUnregisteredMember(context, friedman, "Yael", "Friedman", RelationshipType.Mother);
        }
    }
}