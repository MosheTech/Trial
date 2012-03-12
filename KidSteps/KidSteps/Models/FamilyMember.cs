using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class FamilyMember
    {
        public long Id { get; set; }
        public virtual Family Family { get; set; }
        public virtual User User { get; set; }
        public RelationshipType Relationship { get; set; }
        public Role Role { get; set; }
    }
}