using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    [ComplexType]
    public class PersonName
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }

        public string Full
        {
            get { return string.Format("{0} {1} {2}", First, Middle, Last); }
        }
    }
}