using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class ImagePost : FeedItem
    {
        public virtual Image Image { get; set; }
    }
}