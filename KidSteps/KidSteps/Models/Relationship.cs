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
        public User RelatedUser { get; set; }
        //public int RelatedUserId { get; set; }

        [Required]
        public RelationshipType RelatedUserIsUsers { get; set; }

        //[Required]
        public User Thingy { get; set; }
        //public int UserId { get; set; }
    }
}