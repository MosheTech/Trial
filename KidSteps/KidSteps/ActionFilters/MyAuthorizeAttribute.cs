using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.Models;
using System.Text;

namespace KidSteps.ActionFilters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        MyAuthorizeAttribute(params Role[] roles)
            : base()
        {
            Roles = string.Concat(
        }
    }
}