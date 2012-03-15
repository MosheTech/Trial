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
            model.UnregisteredUsers =
                familyMembers.Where(fm => fm.Relationship != Models.RelationshipType.None && !fm.User.HasAccount).Select(fm => fm.User).ToList();
            //model.PublicViewer =
            User publicViewer = 
                familyMembers.Single(fm => fm.Relationship == Models.RelationshipType.None).User;
            string url = @"http://mykidsteps.com/account/logon/" + publicViewer.Id;
            model.PublicViewerUrl = url;
            

            return View(model);
        }

        public ActionResult Generate(string id)
        {
            return View();
        }
    }
}
