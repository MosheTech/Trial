using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Conversation : UserContent
    {
        public string Title { get; set; }

        public virtual ICollection<ImagePost> ImagePosts { get; set; }
        public virtual ICollection<TextPost> TextPosts { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
    }
}