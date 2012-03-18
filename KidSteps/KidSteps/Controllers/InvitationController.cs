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

            var user = GetCurrentUser();
            var familyMembers = 
                user.DefaultFamily.Members.ToList();

            model.UnregisteredUsers = new List<Tuple<Models.User, string>>();
            IEnumerable<User> unregisteredUsers =
                familyMembers.Where(fm => fm.Relationship != Models.RelationshipType.None && !fm.User.HasAccount).Select(fm => fm.User);
            foreach (var unregisteredUser in unregisteredUsers)
            {
                model.UnregisteredUsers.Add(
                    Tuple.Create(
                        unregisteredUser, 
                        Url.Action("Register", "Account", new { invitationCode = unregisteredUser.Id }, Request.Url.Scheme)));
            }

            model.Inviter = user;

            User publicViewer = 
                familyMembers.Single(fm => fm.Relationship == Models.RelationshipType.None).User;
            string url = Url.Action("PublicViewerLogon", "Account", new {id = publicViewer.Id}, Request.Url.Scheme);
            model.PublicViewerUrl = url;

            return View(model);
        }
    }
}
