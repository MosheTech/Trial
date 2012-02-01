using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Member
    {
        public long MemberId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Family> Families { get; set; }
        public ICollection<Relationship> Relationships { get; set; }
        public string PublicBio { get; set; }
        public string PrivateBio { get; set; }
    }
}