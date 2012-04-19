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
            //throw new Exception();

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
            User pinchas = userRepos.CreateFamilyMember(context, new PersonName("Pinchas", "Friedman"), "admin@gmail.com", "admin", out _);
            pinchas.RoleFlags |= Role.SuperUser;
            User moshe = userRepos.CreateFamilyMember(context, new PersonName("Moshe", "Starkman"), "moshe@gmail.com", "moshe", out _);
            moshe.RoleFlags |= Role.SuperUser;

            // create add admin users' relationships to kids
            FamilyRepository familyRepos = new FamilyRepository();
            User shalom = familyRepos.AddFamilyMember(context, pinchas.Family, new PersonName("Shalom", "Friedman"), null, true);
            familyRepos.UpdateRelationship(context, new Relationship() { SourceUser = pinchas, RelatedUser = shalom, RelatedUserIsSourceUsers = RelationshipType.Child });

            User yael = familyRepos.AddFamilyMember(context, pinchas.Family, new PersonName("Yael", "Friedman"), null, false);
            familyRepos.UpdateRelationship(context, new Relationship() { SourceUser = yael, RelatedUser = shalom, RelatedUserIsSourceUsers = RelationshipType.Child });
            familyRepos.UpdateRelationship(context, new Relationship() { SourceUser = pinchas, RelatedUser = yael, RelatedUserIsSourceUsers = RelationshipType.Spouse });

            // add some comments
            TimelineEvent t1 = new Comment()
            {
                Owner = pinchas,
                SubjectUser = pinchas,
                Text = "Foo"
            };
            TimelineEvent t2 = new Comment()
            {
                Owner = pinchas,
                SubjectUser = pinchas,
                Text = "Foo2",
                IsReplyTo = t1
            };
            TimelineEvent t3 = new Comment()
            {
                Owner = pinchas,
                SubjectUser = pinchas,
                Text = " New Foo"
            };
            context.TimelineEvents.Add(t1);
            context.TimelineEvents.Add(t2);
            context.TimelineEvents.Add(t3);

            //context.Entry(pinchas).Entity.TimelineEvents.Add(t);
            context.SaveChanges();
        }
    }
}