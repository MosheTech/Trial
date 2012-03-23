using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KidSteps.Models;
using KidSteps.DAL;
using KidSteps.ViewModels;

namespace KidSteps.Controllers
{
    public class AccountController : ControllerBase
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        public ActionResult PublicViewerLogOn(string invitationCode)
        {
            // TODO: sanitize string

            var user = db.Members.FirstOrDefault(m => m.InvitationCode == invitationCode);
            if (user != null && user.IsPublicViewer)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), createPersistentCookie: false); // remember me?
            }

            return RedirectToAction("Index", "Home");

            //var familyMembership = user.FamilyMemberships.First(fm => fm.Family.Id == user.DefaultFamily.Id);

            //if (familyMembership.Relationship == RelationshipType.None)
            //else
            //    return RedirectToAction("ConnectToAccount");
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register(string invitationCode)
        {
            RegisterModel model = new RegisterModel();

            if (!string.IsNullOrEmpty(invitationCode))
            {
                UserRepository repos = new UserRepository();
                User user = db.Members.FirstOrDefault(m => m.InvitationCode == invitationCode);
                if (user != null && user.IsUnregisteredMember)
                {
                    model.Name = user.Name;
                    model.InvitationCode = invitationCode;
                    if (user.HasRealEmail)
                        model.Email = user.Email;
                }
            }           

            return View(model);
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                UserRepository repos = new UserRepository();
                MembershipCreateStatus createStatus;

                if (string.IsNullOrEmpty(model.InvitationCode))
                {
                    // register new user

                    // Attempt to register the user
                    User user = repos.Create(db, model.Name, model.Email, model.Password, Role.FamilyAdmin,
                                             out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), model.RememberMe);

                        return RedirectToAction("CreateKid", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    }
                }
                else
                {
                    // register an unregistered family member
                    User user = db.Members.FirstOrDefault(u => u.InvitationCode == model.InvitationCode);

                    if (user != null && user.IsUnregisteredMember)
                    {
                        createStatus = repos.CreateAccountForUser(db, user, model.Email, model.Password);

                        if (createStatus == MembershipCreateStatus.Success)
                        {
                            FormsAuthentication.SetAuthCookie(user.Id.ToString(), model.RememberMe);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", ErrorCodeToString(createStatus));
                        }
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
