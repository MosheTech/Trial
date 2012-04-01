// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace KidSteps.Controllers {
    public partial class UserController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UserController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected UserController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UserController Actions { get { return MVC.User; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "User";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "User";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Details = "Details";
            public readonly string Edit = "Edit";
            public readonly string ProfileImageEdit = "ProfileImageEdit";
            public readonly string RelationshipsEdit = "RelationshipsEdit";
            public readonly string Delete = "Delete";
            public readonly string DeleteConfirmed = "Delete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string Details = "Details";
            public const string Edit = "Edit";
            public const string ProfileImageEdit = "ProfileImageEdit";
            public const string RelationshipsEdit = "RelationshipsEdit";
            public const string Delete = "Delete";
            public const string DeleteConfirmed = "Delete";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Delete = "~/Views/User/Delete.cshtml";
            public readonly string Details = "~/Views/User/Details.cshtml";
            public readonly string Edit = "~/Views/User/Edit.cshtml";
            public readonly string Index = "~/Views/User/Index.cshtml";
            public readonly string ProfileImageEdit = "~/Views/User/ProfileImageEdit.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_UserController: KidSteps.Controllers.UserController {
        public T4MVC_UserController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ViewResult Index() {
            var callInfo = new T4MVC_ViewResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ViewResult Details() {
            var callInfo = new T4MVC_ViewResult(Area, Name, ActionNames.Details);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(KidSteps.ViewModels.UserEditViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ProfileImageEdit() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ProfileImageEdit);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ProfileImageEdit(long? imageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ProfileImageEdit);
            callInfo.RouteValueDictionary.Add("imageId", imageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RelationshipsEdit() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RelationshipsEdit);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RelationshipsEdit(KidSteps.ViewModels.UserEditRelationshipsViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RelationshipsEdit);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Delete() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Delete);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeleteConfirmed() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.DeleteConfirmed);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
