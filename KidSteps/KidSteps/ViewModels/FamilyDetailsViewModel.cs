using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class FamilyDetailsViewModel
    {
        public Family Family { get; set; }
        public IEnumerable<FamilyMember> FamilyMembers { get; set; }
        public IEnumerable<FamilyMember> Kids { get; set; }
    }
}