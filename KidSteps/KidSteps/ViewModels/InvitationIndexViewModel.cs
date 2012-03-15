using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class InvitationIndexViewModel
    {
        public List<User> UnregisteredUsers { get; set; }
        public string PublicViewerUrl { get; set; }
        public User Inviter { get; set; }
    }
}