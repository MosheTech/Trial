using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class UserDetailsViewModel
    {
        public User User { get; set; }

        public bool IsAllowedToEdit { get; set; }
    }
}