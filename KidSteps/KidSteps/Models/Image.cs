using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Image : Media
    {
        public string AltText { get; set; }
        public string Extension { get; set; }

        public override string Url
        {
            get { return string.Format("UserContent\\Media\\{0}.{1}", Id, Extension); }
        }
    }
}