using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    [ComplexType]
    public enum RelationshipType
    {
        Friend,
        Parent,
        Sibling,
        Kid
    }
}