using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.ViewModels
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {
            Relationships = new List<Relationship>();
        }

        public UserEditViewModel(User user)
        {
            Name = user.Name;
            Bio = user.Bio;
            ProfilePicture = user.ProfilePicture;
        }

        [Required]
        public PersonName Name { get; set; }
        public string Bio { get; set; }
        public Image ProfilePicture { get; set; }

        public bool CanEditRelationships { get; set; }
        public List<Relationship> Relationships { get; set; }
        public List<User> UnrelatedFamilyMembers { get; set; }
        public List<RelationshipType> RelationshipTypes { get; set; }
        public RelationshipType NewRelationshipType { get; set; }
        public int NewRelatedUserId { get; set; }
    }
}