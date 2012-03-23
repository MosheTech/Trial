using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.ViewModels;
using KidSteps.Models;

namespace KidSteps.Controllers
{
    [Authorize]
    public class InvitationController : ControllerBase
    {
        //
        // GET: /Invitation/

        public ActionResult Index()
        {
            InvitationIndexViewModel model = new InvitationIndexViewModel();

            var user = CurrentUser;
            var familyMembers = 
                user.DefaultFamily.Members.ToList();

            IEnumerable<User> unregisteredUsers =
                familyMembers.Where(fm => fm.Relationship != Models.RelationshipType.None && !fm.User.HasAccount).Select(fm => fm.User);
            foreach (var unregisteredUser in unregisteredUsers)
            {
                InvitationIndexViewModel.Invitation invitation = new InvitationIndexViewModel.Invitation();
                string url = Url.Action("Register", "Account", new { invitationCode = unregisteredUser.InvitationCode }, Request.Url.Scheme);
                string recipientAddress = 
                    unregisteredUser.HasRealEmail ? unregisteredUser.Email : string.Empty;
                string emailHref =
                    string.Format(
                        "mailto:{0}?subject={1}, come join our family on MyKidSteps.com!" +
                        "&body={2} wants you to join your family on MyKidSteps.com." +
                        "  Follow this link accept the invitation and set up your account. {3}",
                        recipientAddress,
                        unregisteredUser.Name.Full,
                        user.Name.Full,
                        url);
                invitation.User = unregisteredUser;
                invitation.EmailHRef = emailHref;
                invitation.DirectLink = url;
                model.UnregisteredUserInvitations.Add(invitation);
            }

            InvitationIndexViewModel.Invitation publicViewerInvitation = new InvitationIndexViewModel.Invitation();
            User publicViewer =
                familyMembers.Single(fm => fm.Relationship == Models.RelationshipType.None).User;
            string publicViewerUrl = Url.Action("PublicViewerLogon", "Account", new { invitationCode = publicViewer.InvitationCode }, Request.Url.Scheme);
            string publicViewerEmailHref =
                string.Format(
                    "mailto:?subject=You're invited to see my family on MyKidSteps.com!" + 
                    "&body={0} wants to show you a family on MyKidSteps.com." + 
                    "  Follow this link to see it. {1}",
                    user.Name.Full,
                    publicViewerUrl);
            publicViewerInvitation.User = publicViewer;
            publicViewerInvitation.EmailHRef = publicViewerEmailHref;
            publicViewerInvitation.DirectLink = publicViewerUrl;
            model.PublicViewerInvitation = publicViewerInvitation;

            return View(model);
        }
    }
}
