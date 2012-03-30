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
            User publicViewer = userRepos.CrePublicViewer(context);
            publicViewer.Family = family;

            context.SaveChanges();

            return family;
        }

        public User AddFamilyMember(KidStepsContext context, Family family, PersonName name, string email, bool isKid)
        {
            UserRepository userRepos =
                new UserRepository();
            User newUser = userRepos.CreFamilyMember(context, name, email);
            newUser.Family = family;

            if (isKid)
                newUser.RoleFlags |= Role.Kid;

            context.SaveChanges();

            return newUser;
        }

        //public Family CreateForUser(
        //    KidStepsContext context,
        //    User owner)
        //{
        //    return Create(context, owner.Name.Last, owner, null, null);
        //}

        //public Family Create(
        //    KidStepsContext context, 
        //    string name, 
        //    User owner, 
        //    string kidFirstName,
        //    string kidLastName,
        //    string kidEmail,
        //    RelationshipType relationshipOfOwnerToKid)
        //{
        //    PersonName kidName = new PersonName() { First = kidFirstName, Last = kidLastName };
        //    return Create(context, name, owner, kidName, kidEmail, relationshipOfOwnerToKid);
        //}

        //public Family Create(
        //    KidStepsContext context, 
        //    string name,
        //    User owner, 
        //    PersonName kidName,
        //    string kidEmail,
        //    RelationshipType relationshipOfOwnerToKid = RelationshipType.None)
        //{
        //    Family family = new Family();
        //    context.Families.Add(family);
        //    family.Name = name;
        //    family.Admin = owner;

        //    owner.Family = family;

        //    context.SaveChanges();

        //    // add kid
        //    if (kidName != null)
        //    {
        //        AddUnregisteredMember(context, family, kidName, kidEmail, true, RelationshipType.Self);

        //        // add owner
        //        AddMember(
        //            context,
        //            family,
        //            owner,
        //            relationshipOfOwnerToKid,
        //            setAsUsersDefaultFamily: true);
        //    }

        //    // add public viewer
        //    UserRepository userRepos = new UserRepository();
        //    MembershipCreateStatus _;
        //    User publicViewer = userRepos.CreatePublicViewer(context, out _);
        //    AddMember(context, family, publicViewer, RelationshipType.None, setAsUsersDefaultFamily: true);

        //    return family;
        //}

        //public FamilyMember AddUnregisteredMember(
        //    KidStepsContext context,
        //    Family family,
        //    string firstName,
        //    string lastName,
        //    string email,
        //    RelationshipType relationshipToKid)
        //{
        //    PersonName name = new PersonName() {First = firstName, Last = lastName};
        //    return AddUnregisteredMember(context, family, name, email, true, relationshipToKid);
        //}

        //public FamilyMember AddUnregisteredMember(
        //    KidStepsContext context,
        //    Family family,
        //    PersonName name,
        //    string email,
        //    RelationshipType relationshipToKid)
        //{
        //    UserRepository userRepos =
        //        new UserRepository();
        //    User newUser = userRepos.CreateUserWithoutAccount(context, name, email);

        //    return AddMember(context, family, newUser, relationshipToKid, setAsUsersDefaultFamily: true);
        //}


        //public FamilyMember AddMember(
        //    KidStepsContext context,
        //    Family family,
        //    User memberToAdd, 
        //    RelationshipType relationshipToKid,
        //    bool setAsUsersDefaultFamily)
        //{
        //    if (setAsUsersDefaultFamily)
        //        memberToAdd.Family = family;

        //    FamilyMember membership =
        //        AddRelationship(context, memberToAdd, relationshipToKid);

        //    //FamilyMember membership = new FamilyMember()
        //    //{
        //    //    Family = family,
        //    //    Role = Models.Role.UnregisteredMember,
        //    //    User = memberToAdd,
        //    //    Relationship = relationshipToKid
        //    //};

        //    //family.Members.Add(membership);

        //    //if (relationshipToKid == RelationshipType.Self)
        //    //    family.HasKids = true;

        //    return membership;
        //}

        //public FamilyMember AddRelationship(
        //    KidStepsContext context,
        //    User user,
        //    RelationshipType relationship)
        //{
        //    FamilyMember membership = new FamilyMember()
        //    {
        //        Family = user.Family,
        //        Role = Models.Role.UnregisteredMember,
        //        User = user,
        //        Relationship = relationship
        //    };

        //    user.Family.Members.Add(membership);

        //    if (relationship == RelationshipType.Self)
        //        user.Family.HasKids = true;

        //    context.SaveChanges();

        //    return membership;
        //}

        public void AddParentChildRelationship(KidStepsContext context, User parent, User child)
        {
            Relationship parentsRelationshipToChild =
                new Relationship() {RelatedUserId = parent.Id, RelatedUserIsUsers = RelationshipType.Parent, User = child};
            Relationship childsRelationshipToParent =
                new Relationship() {RelatedUserId = child.Id, RelatedUserIsUsers = RelationshipType.Child, User = parent};

            parent.Relationships.Add(childsRelationshipToParent);
            child.Relationships.Add(parentsRelationshipToChild);

            context.SaveChanges();
        }

        public void AddSiblingRelationship(KidStepsContext context, User sibling1, User sibling2)
        {
            Relationship relationship1 =
                new Relationship() { RelatedUserId = sibling2.Id, RelatedUserIsUsers = RelationshipType.Sibling, User = sibling1 };
            Relationship relationship2 =
                new Relationship() { RelatedUserId = sibling1.Id, RelatedUserIsUsers = RelationshipType.Sibling, User = sibling2 };

            sibling1.Relationships.Add(relationship1);
            sibling2.Relationships.Add(relationship2);

            context.SaveChanges();
        }

        public void AddSpousalRelationship(KidStepsContext context, User spouse1, User spouse2)
        {
            Relationship relationship1 =
                new Relationship() { RelatedUserId = spouse2.Id, RelatedUserIsUsers = RelationshipType.Spouse, User = spouse1 };
            Relationship relationship2 =
                new Relationship() { RelatedUserId = spouse1.Id, RelatedUserIsUsers = RelationshipType.Spouse, User = spouse2 };

            spouse1.Relationships.Add(relationship1);
            spouse2.Relationships.Add(relationship2);

            context.SaveChanges();
            
        }

        //private FamilyMember AddUnregisteredMember(
        //    KidStepsContext context,
        //    Family family,
        //    PersonName name,
        //    string email,
        //    bool isMemberOfFamily,
        //    RelationshipType relationshipToKid)
        //{
        //    Role role = isMemberOfFamily ? Role.UnregisteredMember : Role.PublicViewer;

        //    UserRepository userRepos =
        //        new UserRepository();
        //    User newUser = userRepos.CreateUserWithoutAccount(context, name, email);

        //    return AddMember(context, family, newUser, relationshipToKid, setAsUsersDefaultFamily: true);
        //}


    }
}