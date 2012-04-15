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
        public UserEditViewModel()
        {
        }

        public UserEditViewModel(User user)
        {
            Name = user.Name;
            Bio = user.Bio;
            ProfilePicture = user.ProfilePicture;
            if (user.HasRealEmail)
                Email = user.Email;
        }

        [Required]
        public PersonName Name { get; set; }

        [RegularExpression(@"\b[A-Z0-9a-z._%+-]+@[A-Z0-9a-z.-]+\.[A-Za-z]{2,4}\b", ErrorMessage = "Incorrect email address format.")]
        public string Email { get; set; }
        public string Bio { get; set; }

        public bool CanEditProfilePicture { get; set; }
        public Image ProfilePicture { get; set; }

        public bool CanEditRelationships { get; set; }
    }
}