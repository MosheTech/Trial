using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class HomeIndexViewModel
    {
        public bool IsLoggedOn { get; set; }

        public User CurrentUser { get; set; }

        public Family UserFamily { get; set; }
        public List<User> FamilyMembers { get; set; }

        public LogOnModel LogOnModel { get; set; }
    }
}