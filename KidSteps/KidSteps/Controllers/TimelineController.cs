using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidSteps.ActionFilters;
using KidSteps.Models;

namespace KidSteps.Controllers
{
    public partial class TimelineController : TargetedController<User>
    {
        [UserTarget(Permission.ReadUser)]
        public virtual PartialViewResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.Items = Target.TimelineEvents.OrderByDescending(e => e.CreatedTime).ToList();
            return PartialView(model);
        }

        #region ViewModels

        public class IndexViewModel
        {
            public List<TimelineEvent> Items { get; set; }
        }

        #endregion
    }
}
