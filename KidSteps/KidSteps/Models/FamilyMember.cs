//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;

//namespace KidSteps.Models
//{
//    public class FamilyMember
//    {
//        public long Id { get; set; }
//        [InverseProperty("Members")]
//        public virtual Family Family { get; set; }
//        [InverseProperty("FamilyMemberships")]
//        public virtual User User { get; set; }

//        public int RelationshipTypeWrapper { get; set; }
//        public RelationshipType Relationship
//        {
//            get { return (RelationshipType)RelationshipTypeWrapper; }
//            set { RelationshipTypeWrapper = (int)value; }
//        }
//        public Role Role { get; set; }
//    }
//}