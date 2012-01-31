using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Relationship
    {
        public long RelationshipId { get; set; }
        public User Member { get; set; }
        public RelationshipType IsA { get; set; }
        public User Of { get; set; }

        //public RelationshipType IsA 
        //{ 
        //    get { return (Models.RelationshipType)RelationshipType; } 
        //}
        //public Member Of 
        //{ 
        //    get { return Member1; } 
        //}
    }
}