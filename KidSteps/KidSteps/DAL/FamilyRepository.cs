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
            string name, 
            User owner, 
            string kidFirstName,
            string kidLastName,
            RelationshipType
            relationshipOfOwnerToKid)
        {
            PersonName kidName = new PersonName() { First = kidFirstName, Last = kidLastName };
            return Create(context, name, owner, kidName, relationshipOfOwnerToKid);
        }

        public Family Create(
            KidStepsContext context, 
            string name,
            User owner, 
            PersonName kidName,
            RelationshipType
            relationshipOfOwnerToKid)
        {
            Family family = new Family();
            context.Families.Add(family);
            family.Name = name;
            family.Owner = owner;

            context.SaveChanges();

            // add kid
            AddUnregisteredMember(context, family, kidName, RelationshipType.Self);

            // add owner
            AddMember(
                context,
                family,
                owner,
                relationshipOfOwnerToKid,
                setAsUsersDefaultFamily: true);

            return family;
        }

        public FamilyMember AddUnregisteredMember(
            KidStepsContext context,
            Family family,
            string firstName,
            string lastName,
            RelationshipType relationshipToKid)
        {
            PersonName name = new PersonName() {First = firstName, Last = lastName};
            return AddUnregisteredMember(context, family, name, relationshipToKid);
        }

        public FamilyMember AddUnregisteredMember(
            KidStepsContext context,
            Family family,
            PersonName name,
            RelationshipType relationshipToKid)
        {
            UserRepository userRepos =
                new UserRepository();
            MembershipCreateStatus _;
            User newUser = userRepos.CreateUnregisteredUser(context, name, out _);

            return AddMember(context, family, newUser, relationshipToKid, setAsUsersDefaultFamily: true);
        }


        public FamilyMember AddMember(
            KidStepsContext context,
            Family family,
            User memberToAdd, 
            RelationshipType relationshipToKid,
            bool setAsUsersDefaultFamily)
        {
            FamilyMember membership = new FamilyMember()
            {
                Family = family,
                Role = Models.Role.UnregisteredFamilyMember,
                User = memberToAdd,
                Relationship = relationshipToKid
            };

            family.Members.Add(membership);

            if (setAsUsersDefaultFamily)
                memberToAdd.DefaultFamily = family;

            context.SaveChanges();

            return membership;
        }
    }
}