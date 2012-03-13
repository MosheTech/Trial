using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class FamilyCreateViewModel
    {
        [Required]
        public string FamilyName { get; set; }

        public RelationshipType RelationshipOfOwnerToKid { get; set; }

        public List<SelectListItem> RelationshipsToChooseFrom { get; set; }

        [Required]
        public PersonName KidName { get; set; }
    }
}