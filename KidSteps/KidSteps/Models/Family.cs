﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Family
    {
        public Family()
        {
            Members = new List<User>();
            Kids = new List<User>();
        }

        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public virtual User Owner { get; set; }

        public virtual Image Thumbnail { get; set; }

        [InverseProperty("Families")]
        public virtual ICollection<User> Members { get; set; }

        public virtual ICollection<User> Kids { get; set; }
    }
}