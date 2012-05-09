using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Image
    {
        public long Id { get; set; }
        [Required]
        public virtual User CreatedBy { get; set; }
        public string AltText { get; set; }
        public string Extension { get; set; }

        public virtual ICollection<ImageTag> Tags { get; set; }

        public string Url
        {
            get { return string.Format("/UserContent/Media/{0}{1}", Id, Extension); }
        }

        public string Path
        {
            get
            {
                return string.Format("UserContent\\Media\\{0}{1}", Id, Extension);
            }
        }
    }
}