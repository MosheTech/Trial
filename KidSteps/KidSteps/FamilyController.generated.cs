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
    public partial class FamilyController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FamilyController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FamilyController(Dummy d) { }

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

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ViewResult Details() {
            return new T4MVC_ViewResult(Area, Name, ActionNames.Details);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Edit() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FamilyController Actions { get { return MVC.Family; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Family";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Family";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Details = "Details";
            public readonly string Edit = "Edit";
            public readonly string CreateKid = "CreateKid";
            public readonly string CreateFamilyMember = "CreateFamilyMember";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Index = "Index";
            public const string Details = "Details";
            public const string Edit = "Edit";
            public const string CreateKid = "CreateKid";
            public const string CreateFamilyMember = "CreateFamilyMember";
        }


        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit {
            public readonly string id = "id";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Create = "~/Views/Family/Create.cshtml";
            public readonly string Delete = "~/Views/Family/Delete.cshtml";
            public readonly string Details = "~/Views/Family/Details.cshtml";
            public readonly string Edit = "~/Views/Family/Edit.cshtml";
            public readonly string Index = "~/Views/Family/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_FamilyController: KidSteps.Controllers.FamilyController {
        public T4MVC_FamilyController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ViewResult Index() {
            var callInfo = new T4MVC_ViewResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.ViewResult Details(int id) {
            var callInfo = new T4MVC_ViewResult(Area, Name, ActionNames.Details);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(int id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(KidSteps.Models.Family family) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("family", family);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateKid() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreateKid);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateKid(KidSteps.ViewModels.KidCreateViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreateKid);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateFamilyMember() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreateFamilyMember);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateFamilyMember(KidSteps.ViewModels.CreateFamilyMemberViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreateFamilyMember);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
