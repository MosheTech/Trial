using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Relationship
    {
        public long RelationshipId { get; set; }
        public Member Member { get; set; }
        public RelationshipType IsA { get; set; }
        public Member Of { get; set; }

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