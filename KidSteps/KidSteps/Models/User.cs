using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.Security.Principal;

namespace KidSteps.Models
{
    public class User
    {
        public User()
        {
            Name = new PersonName();
            Bio = string.Empty;
        }

        public virtual string Id { get; set; }

        [Required]
        public virtual PersonName Name { get; set; }        
        public virtual Image ProfilePicture { get; set; }
        public virtual string Bio { get; set; }

        [InverseProperty("Members")]
        public virtual ICollection<Family> Families { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; }
        public virtual ICollection<TimelineEvent> TimelineEvents { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}