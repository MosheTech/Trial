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
        public Family CreateForUser(
            KidStepsContext context,
            User owner)
        {
            return Create(context, owner.Name.Last, owner, null, null);
        }

        public Family Create(
            KidStepsContext context, 
            string name, 
            User owner, 
            string kidFirstName,
            string kidLastName,
            string kidEmail,
            RelationshipType relationshipOfOwnerToKid)
        {
            PersonName kidName = new PersonName() { First = kidFirstName, Last = kidLastName };
            return Create(context, name, owner, kidName, kidEmail, relationshipOfOwnerToKid);
        }

        public Family Create(
            KidStepsContext context, 
            string name,
            User owner, 
            PersonName kidName,
            string kidEmail,
            RelationshipType relationshipOfOwnerToKid = RelationshipType.None)
        {
            Family family = new Family();
            context.Families.Add(family);
            family.Name = name;
            family.Owner = owner;

            owner.DefaultFamily = family;

            context.SaveChanges();

            // add kid
            if (kidName != null)
            {
                AddUnregisteredMember(context, family, kidName, kidEmail, true, RelationshipType.Self);

                // add owner
                AddMember(
                    context,
                    family,
                    owner,
                    relationshipOfOwnerToKid,
                    setAsUsersDefaultFamily: true);
            }

            // add public viewer
            UserRepository userRepos = new UserRepository();
            MembershipCreateStatus _;
            User publicViewer = userRepos.CreatePublicViewer(context, out _);
            AddMember(context, family, publicViewer, RelationshipType.None, setAsUsersDefaultFamily: true);

            return family;
        }

        public FamilyMember AddUnregisteredMember(
            KidStepsContext context,
            Family family,
            string firstName,
            string lastName,
            string email,
            RelationshipType relationshipToKid)
        {
            PersonName name = new PersonName() {First = firstName, Last = lastName};
            return AddUnregisteredMember(context, family, name, email, true, relationshipToKid);
        }

        public FamilyMember AddUnregisteredMember(
            KidStepsContext context,
            Family family,
            PersonName name,
            string email,
            RelationshipType relationshipToKid)
        {
            UserRepository userRepos =
                new UserRepository();
            User newUser = userRepos.CreateUserWithoutAccount(context, name, email);

            return AddMember(context, family, newUser, relationshipToKid, setAsUsersDefaultFamily: true);
        }


        public FamilyMember AddMember(
            KidStepsContext context,
            Family family,
            User memberToAdd, 
            RelationshipType relationshipToKid,
            bool setAsUsersDefaultFamily)
        {
            if (setAsUsersDefaultFamily)
                memberToAdd.DefaultFamily = family;

            FamilyMember membership =
                AddRelationship(context, memberToAdd, relationshipToKid);

            //FamilyMember membership = new FamilyMember()
            //{
            //    Family = family,
            //    Role = Models.Role.UnregisteredMember,
            //    User = memberToAdd,
            //    Relationship = relationshipToKid
            //};

            //family.Members.Add(membership);

            //if (relationshipToKid == RelationshipType.Self)
            //    family.HasKids = true;

            return membership;
        }

        public FamilyMember AddRelationship(
            KidStepsContext context,
            User user,
            RelationshipType relationship)
        {
            FamilyMember membership = new FamilyMember()
            {
                Family = user.DefaultFamily,
                Role = Models.Role.UnregisteredMember,
                User = user,
                Relationship = relationship
            };

            user.DefaultFamily.Members.Add(membership);

            if (relationship == RelationshipType.Self)
                user.DefaultFamily.HasKids = true;

            context.SaveChanges();

            return membership;
        }

        private FamilyMember AddUnregisteredMember(
            KidStepsContext context,
            Family family,
            PersonName name,
            string email,
            bool isMemberOfFamily,
            RelationshipType relationshipToKid)
        {
            Role role = isMemberOfFamily ? Role.UnregisteredMember : Role.PublicViewer;

            UserRepository userRepos =
                new UserRepository();
            User newUser = userRepos.CreateUserWithoutAccount(context, name, email);

            return AddMember(context, family, newUser, relationshipToKid, setAsUsersDefaultFamily: true);
        }
    }
}