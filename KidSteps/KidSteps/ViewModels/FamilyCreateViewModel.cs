using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class FamilyCreateViewModel
    {
        [Required]
        public string FamilyName { get; set; }
    }
}