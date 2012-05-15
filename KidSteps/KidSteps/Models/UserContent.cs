using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public abstract class UserContent
    {
        public UserContent()
        {
            CreatedTime = DateTime.Now;
        }
        public int Id { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<User> UserTags { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }

        public DateTime CreatedTime { get; private set; }
    }
}