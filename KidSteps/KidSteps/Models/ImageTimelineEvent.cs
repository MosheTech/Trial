using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class ImageTimelineEvent : TimelineEvent
    {
        public virtual Image Image { get; set; }
    }
}