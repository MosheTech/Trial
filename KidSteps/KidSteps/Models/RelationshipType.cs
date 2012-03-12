using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public enum RelationshipType
    {
        Friend,
        Parent,
        Sibling,
        Kid
    }
}