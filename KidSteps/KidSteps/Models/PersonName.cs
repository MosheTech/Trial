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
        [StringLength(32)]
        [MinLength(1)]
        [Required]
        [Display(Name = "First name")]
        public string First { get; set; }

        [StringLength(32)]
        [MinLength(1)]
        [Required]
        [Display(Name = "Last name")]
        public string Last { get; set; }

        public string Full
        {
            get { return string.Format("{0} {1}", First, Last); }
        }

        public override string ToString()
        {
            return Full;
        }
    }
}