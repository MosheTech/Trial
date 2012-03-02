using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public abstract class Media
    {
        public long Id { get; set; }
        [Required]
        public virtual User CreatedBy { get; set; }
        public abstract string Url { get; }
        public virtual string Path 
        {
            get { return string.Empty; }
        }
        public bool HasPhysicalFile { get; set; }
    }
}