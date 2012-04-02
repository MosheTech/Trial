using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class ManageFamilyViewModel
    {
        public Family UserFamily { get; set; }
        public List<User> FamilyMembers { get; set; }
    }
}