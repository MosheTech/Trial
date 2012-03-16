using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using KidSteps.ActionFilters;
using KidSteps.ViewModels;
using KidSteps.DAL;
using System.Web.Security;

namespace KidSteps.Controllers
{ 
    public class UserController : ControllerBase
    {
        //
        // GET: /User/

        [MyAuthorize(Role.SuperUser)]
        public ViewResult Index()
        {
            return View(db.Members.ToList());
        }

        //
        // GET: /User/Details/5

        [Authorize]
        public ViewResult Details(string id)
        {
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // GET: /User/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        ////
        //// POST: /User/Create

        //[HttpPost]
        //public ActionResult Create(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Members.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");  
        //    }

        //    return View(user);
        //}
        
        //
        // GET: /User/Edit/5

        [Authorize]
        public ActionResult Edit(string id)
        {
            if (!VerifyCurrentUser(id))
                throw new Exception();
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (!VerifyCurrentUser(user.Id))
                    throw new Exception();

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = user.Id });
            }
            return View(user);
        }

        [Authorize]
        public ActionResult ProfileImageEdit()
        {
            var currentUser = GetCurrentUser();
            return View(db.Images.Where(image => image.CreatedBy.Id == currentUser.Id).ToList());
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult ProfileImageEdit(string userId, long? imageId)
        {
            var user = db.Members.Find(userId);
            Image image = null;
            if (imageId.HasValue)
                image = db.Images.Find(imageId);

            if (user != null && image != null)
            {
                user.ProfilePicture = image;
                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = userId });
        }

        //
        // GET: /User/Delete/5

        [Authorize]
        public ActionResult Delete(long id)
        {
            User user = db.Members.Find(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(long id)
        {            
            User user = db.Members.Find(id);
            db.Members.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateKid(long familyId)
        {
            KidCreateViewModel viewModel = new KidCreateViewModel();
            viewModel.FamilyId = familyId;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateKid(KidCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Family family = GetCurrentUser().DefaultFamily;

                FamilyRepository repos = new FamilyRepository();
                repos.AddUnregisteredMember(db, family, model.Name, RelationshipType.Self);

                //MembershipCreateStatus _;
                //UserRepository userRepos = new UserRepository();
                //User kid = userRepos.CreatePublicViewer(db, model.Name, Role.UnregisteredMember, out _);

                //FamilyRepository familyRepos = new FamilyRepository();
                //Family family = db.Families.Find(model.FamilyId);
                //familyRepos.AddMember(db, family, kid, RelationshipType.Self, setAsUsersDefaultFamily: true);

                return RedirectToAction("Details", "Family", new { id = model.FamilyId });
            }

            return View();
        }

        public ActionResult CreateFamilyMember(long familyId)
        {
            CreateFamilyMemberViewModel viewModel = new CreateFamilyMemberViewModel();
            viewModel.FamilyId = familyId;
            viewModel.RelationshipsToChooseFrom = new List<SelectListItem>();
            foreach (RelationshipType type in Enum.GetValues(typeof(RelationshipType)))
            {
                if (type == RelationshipType.Self)
                    continue;
                SelectListItem item = new SelectListItem() { Text = type.ToString(), Value = ((int)type).ToString() };
                viewModel.RelationshipsToChooseFrom.Add(item);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateFamilyMember(CreateFamilyMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                Family family = GetCurrentUser().DefaultFamily;

                FamilyRepository repos = new FamilyRepository();
                repos.AddUnregisteredMember(db, family, model.Name, model.Relationship);

                //MembershipCreateStatus _;
                //UserRepository userRepos = new UserRepository();
                //User member = userRepos.CreatePublicViewer(db, model.Name, Role.UnregisteredMember, out _);

                //FamilyRepository familyRepos = new FamilyRepository();
                //Family family = db.Families.Find(model.FamilyId);
                //familyRepos.AddMember(db, family, member, model.Relationship, setAsUsersDefaultFamily: true);

                return RedirectToAction("Details", "Family", new { id = model.FamilyId });
            }

            return View();
        }


    }
}