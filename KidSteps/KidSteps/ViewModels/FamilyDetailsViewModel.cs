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
        public IEnumerable<User> FamilyMembers { get; set; }
        public IEnumerable<User> Kids { get; set; }
    }
}