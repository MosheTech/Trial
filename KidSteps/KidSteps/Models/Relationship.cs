using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public partial class Relationship
    {
        public RelationshipType IsA 
        { 
            get { return (Models.RelationshipType)RelationshipType; } 
        }
        public Member Of 
        { 
            get { return Member1; } 
        }
    }
}