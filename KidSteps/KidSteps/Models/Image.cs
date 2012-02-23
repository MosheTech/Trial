using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Mvc;

namespace KidSteps.Models
{
    public class Image : Media
    {
        public string AltText { get; set; }
        public string Extension { get; set; }

        public override string Url
        {
            get { return string.Format("UserContent\\Media\\{0}{1}", Id, Extension); }
        }

        [FileExtensions(Extensions = ".png,.jpg,.jpeg,.gif", ErrorMessage = "Must be an image file.")]
        public override HttpPostedFileBase File { get; set; }
    }
}