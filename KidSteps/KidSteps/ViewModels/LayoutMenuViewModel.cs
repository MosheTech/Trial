using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class LayoutMenuViewModel
    {
        public Family DefaultFamily { get; set; }
        public User CurrentUser { get; set; }
    }
}