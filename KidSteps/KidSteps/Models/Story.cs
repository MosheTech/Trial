using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Story : FeedItem
    {
        public string Text { get; set; }

        public virtual ICollection<User> UserTags { get; set; }
    }
}