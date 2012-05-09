using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public abstract class ImageTag
    {
        public int Id { get; set; }

        [Required]
        public virtual Image Image { get; set; }
    }
}