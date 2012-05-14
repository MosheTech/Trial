using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual FamilyEvent FamilyEvent { get; set; }
        public virtual ICollection<ImagePost> ImagePosts { get; set; }
        public virtual ICollection<TextPost> TextPosts { get; set; }
        public virtual ICollection<Story> Stories { get; set; }

        public virtual ICollection<User> UserTags { get; set; }
    }
}