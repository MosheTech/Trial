using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using KidSteps.Utils;

namespace KidSteps.ViewModels
{
    public class UserEditRelationshipsViewModel
    {
        public User TargetUser { get; set; }

        public List<RelationshipViewModel> FamilyRelationshipsNew { get; set; }

        public List<SelectListItem> RelationshipTypesFor(RelationshipViewModel r)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (RelationshipTypeViewModel type in Enum.GetValues(typeof(RelationshipTypeViewModel)))
            {
                SelectListItem item = new SelectListItem();
                item.Text = type.GetAttributeValue<DisplayAttribute, string>(t => t.Description);
                item.Value = type.ToString();

                if (type == r.Relationship)
                    item.Selected = true;

                list.Add(item);
            }

            return list;
        }

        public enum RelationshipTypeViewModel
        {
            [Display(Description = "Parent")]
            Parent,

            [Display(Description = "Sibling")]
            Sibling,

            [Display(Description = "Child")]
            Child,

            [Display(Description = "Spouse")]
            Spouse,

            [Display(Description = "Non-immediate family member")]
            NotImmediateFamilyMember,
        }

        public class RelationshipViewModel
        {
            public int RelatedUserId { get; set; }
            public User RelatedUser { get; set; }
            public RelationshipTypeViewModel Relationship { get; set; }
        }
    }
}