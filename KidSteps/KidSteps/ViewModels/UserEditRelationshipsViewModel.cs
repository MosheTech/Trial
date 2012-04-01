using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.Web.Mvc;

namespace KidSteps.ViewModels
{
    public class UserEditRelationshipsViewModel
    {
        public List<Relationship> CurrentRelationships { get; set; }
        public List<SelectListItem> UnrelatedFamilyMembers { get; set; }
        public User TargetUser { get; set; }
        public IEnumerable<SelectListItem> RelationshipTypes { get; set; }
        public RelationshipType NewRelationshipType { get; set; }
        public int NewRelatedUserId { get; set; }
    }
}