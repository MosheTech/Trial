using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KidSteps.ViewModels
{
    public class CreateFamilyMemberViewModel
    {
        public CreateFamilyMemberViewModel()
        {
            Name = new PersonName();
        }

        [Required]
        public PersonName Name { get; set; }

        public string Email { get; set; }

        [Required]
        [Display(Name = "Relationship to kids")]
        public RelationshipType Relationship { get; set; }

        public IEnumerable<SelectListItem> RelationshipsToChooseFrom { get; set; }
    }
}