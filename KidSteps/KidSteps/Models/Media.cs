using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public abstract class Media
    {
        public long Id { get; set; }
        public abstract string Url { get; }
    }
}