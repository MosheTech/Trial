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
            Id = user.Id;
            Name = user.Name;
            Bio = user.Bio;
            ProfilePicture = user.ProfilePicture;
        }

        public int Id { get; set; }

        [Required]
        public PersonName Name { get; set; }
        public string Bio { get; set; }
        public Image ProfilePicture { get; set; }
    }
}