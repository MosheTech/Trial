using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KidSteps.Controllers;

namespace KidSteps.ViewModels
{
    public class CreateFamilyMemberViewModel
    {
        public CreateFamilyMemberViewModel()
        {
            Name = new PersonName();
            //RelationshipsToChooseFrom = FamilyController.RelationshipsTypes;
        }

        [Required]
        public PersonName Name { get; set; }

        public string Email { get; set; }

        //[Required]
        //[Display(Name = "Relationship to kids")]
        //public RelationshipType Relationship { get; set; }

        //public IEnumerable<SelectListItem> RelationshipsToChooseFrom { get; private set; }

        public bool IsKid { get; set; }
    }
}