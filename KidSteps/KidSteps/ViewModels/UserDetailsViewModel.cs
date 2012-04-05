using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel()
        {
            ParentRelationships = new List<Relationship>();
            ChildrenRelationships = new List<Relationship>();
            OtherImmediateFamilyRelationships = new List<Relationship>();
        }

        public User User { get; set; }

        public bool IsAllowedToEdit { get; set; }

        public List<Relationship> ParentRelationships { get; set; }
        public List<Relationship> ChildrenRelationships { get; set; }
        public List<Relationship> OtherImmediateFamilyRelationships { get; set; }

        public string InvitationUrl { get; set; }
    }
}