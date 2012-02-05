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
        public abstract string Url { get; }
        [NotMapped]
        //[Required, Microsoft.Web.Mvc.FileExtensions(Extensions = "csv", ErrorMessage = "Specify a CSV file. (Comma-separated values)")]
        public HttpPostedFileBase File { get; set; }
    }
}