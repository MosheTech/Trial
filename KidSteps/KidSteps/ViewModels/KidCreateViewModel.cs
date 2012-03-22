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
        public long FamilyId { get; set; }
        public PersonName Name { get; set; }

        public bool ShouldChooseRelationship { get; set; }

        public RelationshipType RelationshipOfOwnerToKid { get; set; }

        public IEnumerable<SelectListItem> RelationshipsToChooseFrom { get; set; }
    }
}