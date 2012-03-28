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
    public class TargetedController<T> : ControllerBase//, ITargetedController
    {
        public T Target { get; set; }
        //public Type Type
        //{
        //    get { return typeof (T); }
        //}
        //public object TargetUntyped
        //{
        //    get
        //    {
        //        return Target;
        //    }
        //    set
        //    {
        //        Target = (T)value;
        //    }
        //}

        //public Type TargetType
        //{
        //    get { return typeof(T); }
        //}
    }

    //public interface ITargetedController : IController
    //{
    //    object TargetUntyped { get; set; }
    //    Type TargetType { get; }
    //}
}
