using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Relationship
    {
        public long Id { get; set; }

        //[Required]
        public virtual User RelatedUser { get; set; }
        //public int RelatedUserId { get; set; }

        public int RelatedUserIsSourceUsersWrapper { get; set; }

        [Required]
        public RelationshipType RelatedUserIsSourceUsers 
        {
            get
            {
                return (RelationshipType)RelatedUserIsSourceUsersWrapper;
            }
            set
            {
                RelatedUserIsSourceUsersWrapper = (int)value;
            }
        }

        //[Required]
        public virtual User SourceUser { get; set; }
        //public int UserId { get; set; }
    }
}