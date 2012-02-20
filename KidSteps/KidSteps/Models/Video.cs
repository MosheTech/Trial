using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Video : Media
    {
        public override string Url
        {
            get { throw new NotImplementedException(); }
        }

        [Required, Microsoft.Web.Mvc.FileExtensions(Extensions = ".png,.jpg,.jpeg,.gif", ErrorMessage = "Specify a CSV file. (Comma-separated values)")]
        public override HttpPostedFileBase File { get; set; }
    }
}