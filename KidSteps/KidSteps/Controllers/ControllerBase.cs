using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KidSteps.Models;
using System.Security.Principal;

namespace KidSteps.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public ControllerBase()
        {
            _currentUser = new Lazy<User>(() => GetCurrentUser());
        }

        public User CurrentUser
        {
            get { return _currentUser.Value; }
        }

        protected IPrincipal MembershipUser
        {
            get { return User; }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public KidStepsContext Context
        {
            get { return db; }
        }

        protected KidStepsContext db = new KidStepsContext();

        private User GetCurrentUser()
        {
            DAL.UserRepository repo = new DAL.UserRepository();
            return repo.FindByMembership(db, User);
        }

        private Lazy<User> _currentUser;
    }
}
