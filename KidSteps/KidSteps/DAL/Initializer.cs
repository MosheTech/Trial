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
            //foreach (Models.Role role in Enum.GetValues(typeof(Models.Role)))
            //    Roles.CreateRole(role.ToString());

            // todo: unique constraint for email

            // create admin users
            UserRepository userRepos = new UserRepository();
            MembershipCreateStatus _;
            User pinchas = userRepos.Cre(context, new PersonName("Pinchas", "Friedman"), "admin", "admin", out _);//, Role.SuperUser, out _);
            pinchas.RoleFlags |= Role.SuperUser;
            User moshe = userRepos.Cre(context, new PersonName("Moshe", "Starkman"), "moshe", "moshe", out _);
            moshe.RoleFlags |= Role.SuperUser;

            // create add admin users' relationships to kids
            FamilyRepository familyRepos = new FamilyRepository();
            User shalom = familyRepos.AddFamilyMember(context, pinchas.Family, new PersonName("Shalom", "Friedman"), null, true);
            familyRepos.AddParentChildRelationship(context, pinchas, shalom);

            User yael = familyRepos.AddFamilyMember(context, pinchas.Family, new PersonName("Yael", "Friedman"), null, false);
            familyRepos.AddParentChildRelationship(context, yael, shalom);
            familyRepos.AddSpousalRelationship(context, pinchas, yael);


            //familyRepos.AddRelationship(context, pinchas, RelationshipType.Father);
            //familyRepos.AddRelationship(context, moshe, RelationshipType.Father);
            
            //// add family members
            //familyRepos.AddUnregisteredMember(context, pinchas.Family, "Shalom", "Friedman", null, RelationshipType.Self);
            //familyRepos.AddUnregisteredMember(context, pinchas.Family, "Yael", "Friedman", null, RelationshipType.Mother);

            context.SaveChanges();
        }
    }
}