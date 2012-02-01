using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public class Family
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }

        [InverseProperty("Families")]
        public virtual ICollection<User> Members { get; set; }
    }
}