using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Reply
    {
        public Reply()
        {
            CreatedTime = DateTime.Now;
        }

        public int Id { get; set; }

        public virtual User Owner { get; set; }

        public string Text { get; set; }

        public DateTime CreatedTime { get; private set; }
    }
}