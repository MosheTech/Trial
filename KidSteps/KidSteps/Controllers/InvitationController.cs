using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.ViewModels;
using KidSteps.Models;
using KidSteps.ActionFilters;

namespace KidSteps.Controllers
{
    [Authorize]
    public partial class InvitationController : TargetedController<Family>
    {
        //
        // GET: /Invitation/

        [FamilyTarget(Permission.EditFamily)]
        public virtual ActionResult Index()
        {
            InvitationIndexViewModel model = new InvitationIndexViewModel();

            List<User> familyMembers = Target.Members.ToList();

            //IEnumerable<User> familyMembers =
            //    db.Users.Where(u => u.Family.Id == CurrentUser.Family.Id).ToList();
                //familyMembers.Where(fm => fm.Relationship != Models.RelationshipType.None && !fm.User.HasAccount).Select(fm => fm.User);
            foreach (var unregisteredUser in familyMembers.Where(u => u.IsUnregisteredFamilyMember))
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
                        Target.Admin.Name.Full,
                        url);
                invitation.User = unregisteredUser;
                invitation.EmailHRef = emailHref;
                invitation.DirectLink = url;
                model.UnregisteredUserInvitations.Add(invitation);
            }

            InvitationIndexViewModel.Invitation publicViewerInvitation = new InvitationIndexViewModel.Invitation();
            User publicViewer =
                familyMembers.FirstOrDefault(u => u.IsPublicViewer);
                //db.Users.First(u => u.Family.Id == CurrentUser.Family.Id && u.IsPublicViewer);// familyMembers.First(.Single(fm => fm.Relationship == Models.RelationshipType.None).User;
            string publicViewerUrl = Url.Action("PublicViewerLogon", "Account", new { invitationCode = publicViewer.InvitationCode }, Request.Url.Scheme);
            string publicViewerEmailHref =
                string.Format(
                    "mailto:?subject=You're invited to see my family on MyKidSteps.com!" + 
                    "&body={0} wants to show you a family on MyKidSteps.com." + 
                    "  Follow this link to see it. {1}",
                    Target.Admin.Name.Full,
                    publicViewerUrl);
            publicViewerInvitation.User = publicViewer;
            publicViewerInvitation.EmailHRef = publicViewerEmailHref;
            publicViewerInvitation.DirectLink = publicViewerUrl;
            model.PublicViewerInvitation = publicViewerInvitation;

            return View(model);
        }
    }
}
