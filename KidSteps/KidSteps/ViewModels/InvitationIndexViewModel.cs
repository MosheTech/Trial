using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class InvitationIndexViewModel
    {
        public InvitationIndexViewModel()
        {
            UnregisteredUserInvitations = new List<Invitation>();
        }

        public List<Invitation> UnregisteredUserInvitations { get; set; }
        public Invitation PublicViewerInvitation { get; set; }

        public class Invitation
        {
            public User User { get; set; }
            public string EmailHRef { get; set; }
            public string DirectLink { get; set; }
        }
    }
}