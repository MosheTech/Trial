using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public abstract class TimelineEvent
    {
        public TimelineEvent()
        {
            CreatedTime = DateTime.Now;
        }

        public int Id { get; set; }
        public virtual User Owner { get; set; }
        public virtual User SubjectUser { get; set; }
        public virtual TimelineEvent IsReplyTo { get; set; }
        public DateTime CreatedTime { get; private set; }
        //public string foo { get; set; }
    }
}