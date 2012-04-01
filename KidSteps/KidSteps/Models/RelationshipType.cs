using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KidSteps.Models
{
    public enum RelationshipType
    {
        Parent,
        Sibling,
        Child,
        Spouse
    }

    public static class RelationshipTypeExtensions
    {
        public static RelationshipType Reciprocal(this RelationshipType relationship)
        {
            switch (relationship)
            {
                case RelationshipType.Child:
                    return RelationshipType.Parent;
                case RelationshipType.Parent:
                    return RelationshipType.Child;
                case RelationshipType.Sibling:
                    return RelationshipType.Sibling;
                case RelationshipType.Spouse:
                    return RelationshipType.Spouse;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}