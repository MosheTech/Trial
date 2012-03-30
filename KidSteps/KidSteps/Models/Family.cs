using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Family
    {
        public Family()
        {
            //Members = new List<FamilyMember>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual User Admin { get; set; }

        public virtual Image Thumbnail { get; set; }

        //[InverseProperty("Family")]
        //public virtual ICollection<FamilyMember> Members { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public bool HasKids { get; set; }
    }
}