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
    public class AddFamilyMemberViewModel
    {
        public AddFamilyMemberViewModel()
        {
            Name = new PersonName();
            //RelationshipsToChooseFrom = FamilyController.RelationshipsTypes;
        }

        public string PageHeader
        {
            get { return IsKid ? "Add your child" : "Add a family member"; }
        }

        [Required]
        public PersonName Name { get; set; }

        [RegularExpression(@"\b[A-Z0-9a-z._%+-]+@[A-Z0-9a-z.-]+\.[A-Za-z]{2,4}\b", ErrorMessage = "Incorrect email address format.")]
        public string Email { get; set; }

        //[Required]
        //[Display(Name = "Relationship to kids")]
        //public RelationshipType Relationship { get; set; }

        //public IEnumerable<SelectListItem> RelationshipsToChooseFrom { get; private set; }

        public bool IsKid { get; set; }
    }
}