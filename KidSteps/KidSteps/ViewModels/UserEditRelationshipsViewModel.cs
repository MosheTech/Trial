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
        //public UserEditRelationshipsViewModel()
        //{
        //    RelationshipTypesNew = new List<SelectListItem>();
        //    foreach (var item in Enum.GetValues(typeof(RelationshipTypeViewModel)))
        //    {
        //        var s = new SelectListItem();
        //        s.Text = item.ToString();
        //        s.Value = item.ToString();

        //        if (s.Text.ToString() == RelationshipTypeViewModel.Sibling.ToString())
        //            s.Selected = true;

        //        RelationshipTypesNew.Add(s);
        //    }
        //}

        public List<Relationship> CurrentRelationships { get; set; }
        public List<SelectListItem> UnrelatedFamilyMembers { get; set; }
        public User TargetUser { get; set; }
        public IEnumerable<SelectListItem> RelationshipTypes { get; set; }
        public RelationshipType NewRelationshipType { get; set; }
        public int NewRelatedUserId { get; set; }

        public List<Tuple<User, Relationship>> FamilyRelationships { get; set; }

        public List<RelationshipViewModel> FamilyRelationshipsNew { get; set; }

        public List<SelectListItem> RelationshipTypesFor(RelationshipViewModel r)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (RelationshipTypeViewModel type in Enum.GetValues(typeof(RelationshipTypeViewModel)))
            {
                SelectListItem item = new SelectListItem();
                item.Text = type.ToString();
                item.Value = type.ToString();

                if (type == r.Relationship)
                    item.Selected = true;

                list.Add(item);
            }

            return list;
        }

        public enum RelationshipTypeViewModel
        {
            Parent,
            Sibling,
            Child,
            Spouse,
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