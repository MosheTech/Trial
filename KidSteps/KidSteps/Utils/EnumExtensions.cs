using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Utils
{
    public static class EnumExtensions
    {
        public static TExpected GetAttributeValue<TAttribute, TExpected>(this Enum enumeration, Func<TAttribute, TExpected> expression)
            where TAttribute : Attribute
        {
            TAttribute attribute =
                enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().SingleOrDefault();

            if (attribute == null)
                return default(TExpected);

            return expression(attribute);
        } 
    }
}