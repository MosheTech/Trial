using System;
using System.Web;
using System.Web.Mvc;
using Elmah;
using KidSteps.ActionFilters;
using KidSteps.Models;

namespace KidSteps.Areas.Admin.Controllers 
{
    [MyAuthorize(Permission.SuperUserAccess)]
    public partial class ElmahController : Controller
    {
        public virtual ActionResult Index()
        {
            return new ElmahResult();
        }

        public virtual ActionResult Stylesheet()
        {
            return new ElmahResult("stylesheet");
        }

        public virtual ActionResult Rss()
        {
            return new ElmahResult("rss");
        }

        public virtual ActionResult DigestRss()
        {
            return new ElmahResult("digestrss");
        }

        public virtual ActionResult About()
        {
            return new ElmahResult("about");
        }

        public virtual ActionResult Detail()
        {
            return new ElmahResult("detail");
        }

        public virtual ActionResult Download()
        {
            return new ElmahResult("download");
        }

        public virtual ActionResult Json()
        {
            return new ElmahResult("json");
        }

        public virtual ActionResult Xml()
        {
            return new ElmahResult("xml");
        }
    }

    internal class ElmahResult : ActionResult
    {
        private readonly string _resouceType;

        public ElmahResult()
            : this(null)
        {
        }

        public ElmahResult(string resouceType)
        {
            _resouceType = resouceType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var factory = new ErrorLogPageFactory();

            if (!string.IsNullOrEmpty(_resouceType))
            {
                string pathInfo = "/" + _resouceType;
                context.HttpContext.RewritePath(FilePath(context), pathInfo,
                                                context.HttpContext.Request.QueryString.ToString());
            }

            var currentContext = GetCurrentContextAsHttpContext(context);

            var httpHandler = factory.GetHandler(currentContext, null, null, null);
            var httpAsyncHandler = httpHandler as IHttpAsyncHandler;

            if (httpAsyncHandler != null)
            {
                httpAsyncHandler.BeginProcessRequest(currentContext, r => { }, null);
                return;
            }

            httpHandler.ProcessRequest(currentContext);
        }

        private static HttpContext GetCurrentContextAsHttpContext(ControllerContext context)
        {
            return context.HttpContext.ApplicationInstance.Context;
        }

        private string FilePath(ControllerContext context)
        {
            return _resouceType != "stylesheet"
                       ? context.HttpContext.Request.Path.Replace(String.Format("/{0}", _resouceType), string.Empty)
                       : context.HttpContext.Request.Path;
        }
    }
}
