using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.ViewModels;

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
            model.UnregisteredUsers =
                familyMembers.Where(fm => fm.Relationship != Models.RelationshipType.None && !fm.User.HasAccount).Select(fm => fm.User).ToList();
            model.PublicViewer =
                familyMembers.Single(fm => fm.Relationship == Models.RelationshipType.None).User;

            return View(model);
        }

        public ActionResult Generate(string id)
        {
            return View();
        }
    }
}
