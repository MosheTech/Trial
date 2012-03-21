using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.ViewModels
{
    public class UserEditViewModel
    {
        [Required]
        public PersonName Name { get; set; }
        public string Bio { get; set; }
        public Image ProfilePicture { get; set; }
    }
}