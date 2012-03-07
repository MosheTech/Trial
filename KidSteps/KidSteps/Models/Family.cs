using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Family
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual User Owner { get; set; }

        [InverseProperty("Family")]
        public virtual ICollection<User> Members { get; set; }
    }
}