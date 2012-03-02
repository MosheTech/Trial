﻿using System;
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

        public override string Url
        {
            get { return string.Format("/UserContent/Media/{0}{1}", Id, Extension); }
        }

        public override string Path
        {
            get
            {
                return string.Format("UserContent\\Media\\{0}{1}", Id, Extension);
            }
        }
    }

    public class ImageViewModel
    {

        [FileExtensions(Extensions = ".png,.jpg,.jpeg,.gif", ErrorMessage = "Must be an image file.")]
        public override HttpPostedFileBase File { get; set; }

    }
}