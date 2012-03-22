using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KidSteps.ViewModels
{
    public class KidCreateViewModel
    {
        public KidCreateViewModel()
        {
            Name = new PersonName();
        }

        public PersonName Name { get; set; }

        public string Email { get; set; }

        public bool ShouldChooseRelationship { get; set; }

        [Display(Name = "My relationship to the kid")]
        public RelationshipType RelationshipOfOwnerToKid { get; set; }

        public IEnumerable<SelectListItem> RelationshipsToChooseFrom { get; set; }
    }
}