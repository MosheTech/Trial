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
        [Required]
        public long FamilyId { get; set; }
        [Required]
        public PersonName Name { get; set; }
        [Required]
        public RelationshipType Relationship { get; set; }

        public IEnumerable<SelectListItem> RelationshipsToChooseFrom { get; set; }
    }
}