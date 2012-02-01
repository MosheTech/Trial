using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class User
    {
        public long Id { get; set; }

        public PersonName Name { get; set; }
        public Image ProfilePicture { get; set; }
        public string Bio { get; set; }

        [InverseProperty("Members")]
        public virtual ICollection<Family> Families { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; }
        public virtual ICollection<TimelineEvent> TimelineEvents { get; set; }
    }
}