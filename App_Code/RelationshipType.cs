using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

/// <summary>
/// Summary description for RelationshipType
/// </summary>
public enum RelationshipType : ushort
{
    [Display(Name = "Friend")]
    None = 0,

    Son = 1,
    Daughter = 2,
    Father = 3,
    Mother = 4,
    Brother = 5,
    Sister = 6,
    [Display(Name = "Maternal Grandmother")]
    GrandmotherMaternal = 7,

    [Display(Name = "Paternal Grandmother")]
    GrandmotherPaternal = 8,

    [Display(Name = "Maternal Grandfather")]
    GrandfatherMaternal = 9,

    [Display(Name = "Paternal Grandfather")]
    GrandfatherPaternal = 10,

    Aunt = 11,
    Uncle = 12
}

public class RelationshipTypeExtensions
{
    public static IEnumerable<RelationshipTypeItem> GetAllRelationshipTypes()
    {
        foreach (RelationshipType type in Enum.GetValues(typeof(RelationshipType)))
        {
            string text = type.ToString();

            Type enumType = typeof(RelationshipType);
            string enumValue = Enum.GetName(enumType, type);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            object[] attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs.Length > 0)
            {
                string outString = ((DisplayAttribute)attrs[0]).Name;

                if (((DisplayAttribute)attrs[0]).ResourceType != null)
                {
                    outString = ((DisplayAttribute)attrs[0]).GetName();
                }

                text = outString;
            }

            RelationshipTypeItem relationshipItem = 
                new RelationshipTypeItem(
                    text,
                    (ushort)type);
            yield return relationshipItem;
        }
    }

}

public class RelationshipTypeItem
{
    public RelationshipTypeItem(string text, ushort value)
    {
        Text = text;
        Value = value;
    }
    public string Text { get; set; }
    public ushort Value { get; set; }
}