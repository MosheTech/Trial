using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.DAL
{
    public class FamilyRepository
    {
        public Family Create(KidStepsContext context, string name, User owner)
        {
            Family family = new Family();
            context.Families.Add(family);
            family.Name = name;
            family.Owner = owner;

            context.SaveChanges();

            AddMember(context, family, owner, RelationshipType.Friend);

            return family;
        }

        public FamilyMember AddMember(
            KidStepsContext context,
            Family family,
            User memberToAdd, 
            RelationshipType relationshipToKid)
        {
            FamilyMember membership = new FamilyMember()
            {
                Family = family,
                Role = Models.Role.UnregisteredFamilyMember,
                User = memberToAdd,
                Relationship = relationshipToKid
            };

            family.Members.Add(membership);

            context.SaveChanges();

            return membership;
        }
    }
}