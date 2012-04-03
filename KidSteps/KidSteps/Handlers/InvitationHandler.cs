using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;
using System.Web.Mvc;

namespace KidSteps.Handlers
{
    public class InvitationHandler
    {
        public static string CreateUrl(User user, UrlHelper urlHelper, HttpRequestBase request)
        {
            if (!user.IsPublicViewer)
                return urlHelper.Action(MVC.Account.Register(user.InvitationCode), request.Url.Scheme);
            else
                return urlHelper.Action(MVC.Account.PublicViewerLogOn(user.InvitationCode), request.Url.Scheme);
        }
    }
}