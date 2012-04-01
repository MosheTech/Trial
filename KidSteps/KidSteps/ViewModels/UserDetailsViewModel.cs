using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel()
        {
            Parents = new List<User>();
            Children = new List<User>();
            OtherImmediateFamily = new List<User>();
        }

        public User User { get; set; }

        public bool IsAllowedToEdit { get; set; }

        public List<User> Parents { get; set; }
        public List<User> Children { get; set; }
        public List<User> OtherImmediateFamily { get; set; }
    }
}