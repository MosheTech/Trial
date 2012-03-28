using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;

namespace KidSteps.Utils
{
    public static class ActionResultExtensions
    {
        public static ActionResult WithId(this ActionResult result, int id)
        {
            return result.AddRouteValue("id", id);
        }

        public static ActionResult WithId(this ActionResult result, string id)
        {
            return result.AddRouteValue("id", id);
        }

        public static ActionResult WithId(this ActionResult result, User user)
        {
            return result.AddRouteValue("id", user.Id);
        }

        public static ActionResult WithId(this ActionResult result, Family family)
        {
            return result.AddRouteValue("id", family.Id);
        }

        public static ActionResult WithId(this ActionResult result, IDictionary<string, object> viewData)
        {
            return result.AddRouteValue("id", viewData["id"]);
        }
    }
}