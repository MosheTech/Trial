using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using KidSteps.Models;

namespace KidSteps.DAL
{
    public class FamilyRepository
    {
        public Family Create(
            KidStepsContext context,
            User admin)
        {
            Family family = new Family();
            context.Families.Add(family);
            family.Name = admin.Name.Last;
            family.Admin = admin;
            admin.Family = family;

            // add public viewer
            UserRepository userRepos = new UserRepository();
            User publicViewer = userRepos.CreatePublicViewer(context);
            publicViewer.Family = family;

            context.SaveChanges();

            return family;
        }

        public User AddFamilyMember(KidStepsContext context, Family family, PersonName name, string email, bool isKid)
        {
            UserRepository userRepos =
                new UserRepository();
            User newUser = userRepos.CreateFamilyMember(context, name, email);
            newUser.Family = family;

            if (isKid)
            {
                newUser.RoleFlags |= Role.Kid;
                family.HasKids = true;
            }

            context.SaveChanges();

            return newUser;
        }

        public void UpdateRelationship(KidStepsContext context, Relationship relationship)
        {
            var existingRelationship =
                relationship.SourceUser.Relationships.FirstOrDefault(rel => rel.RelatedUser.Id == relationship.RelatedUser.Id);

            if (existingRelationship == null)
                context.Relationships.Add(relationship);
            else
                existingRelationship.RelatedUserIsSourceUsers = relationship.RelatedUserIsSourceUsers;

            Relationship reciprocalRelationship = new Relationship();
            reciprocalRelationship.RelatedUser = relationship.SourceUser;
            reciprocalRelationship.SourceUser = relationship.RelatedUser;
            reciprocalRelationship.RelatedUserIsSourceUsers = relationship.RelatedUserIsSourceUsers.Reciprocal();

            var existingReciprocalRelationship =
                relationship.RelatedUser.Relationships.FirstOrDefault(rel => rel.RelatedUser.Id == relationship.SourceUser.Id);

            if (existingReciprocalRelationship == null)
                context.Relationships.Add(reciprocalRelationship);
            else
                existingReciprocalRelationship.RelatedUserIsSourceUsers = reciprocalRelationship.RelatedUserIsSourceUsers;

            context.SaveChanges();
        }

        public void RemoveRelationship(KidStepsContext context, Relationship relationship)
        {

            var existingRelationship =
                relationship.SourceUser.Relationships.FirstOrDefault(rel => rel.RelatedUser.Id == relationship.RelatedUser.Id);

            if (existingRelationship != null)
                context.Relationships.Remove(existingRelationship);

            var existingReciprocalRelationship =
                relationship.RelatedUser.Relationships.FirstOrDefault(rel => rel.RelatedUser.Id == relationship.SourceUser.Id);

            if (existingReciprocalRelationship != null)
                context.Relationships.Remove(existingReciprocalRelationship);

            context.SaveChanges();
        }


    }
}