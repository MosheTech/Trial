using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public enum RelationshipType
    {
        Mother,
        Father,
        Grandmother,
        Grandfather,
        Aunt,
        Uncle,
        Cousin,
        Self,
        None
    }
}