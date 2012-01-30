using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Family
    {
        public string FamilyId { get; set; }
        public string Name { get; set; }
        public Member Owner { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}