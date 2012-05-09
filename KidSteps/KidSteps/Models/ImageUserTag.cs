using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class ImageUserTag : ImageTag
    {
        [Required]
        public virtual User User { get; set; }
    }
}