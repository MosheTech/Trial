using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.Security.Principal;

namespace KidSteps.Models
{
    public class User
    {
        public User()
        {
            Name = new PersonName();
            Bio = string.Empty;
            Relationships = new List<Relationship>();
        }

        public virtual int Id { get; set; }

        public bool HasRealEmail { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(25)]
        public string Phone { get; set; }

        public int RoleWrapper { get; set; }

        public Role RoleFlags
        {
            get { return (Role) RoleWrapper; }
            set { RoleWrapper = (int) value; }
        }

        public bool IsPublicViewer
        {
            get { return RoleFlags.HasFlag(Role.PublicViewer); }
        }

        public bool IsMemberOfFamily
        {
            get { return RoleFlags.HasFlag(Role.MemberOfFamily); }
        }

        public bool IsRegistered
        {
            get { return RoleFlags.HasFlag(Role.Registered); }
        }

        public bool IsUnregisteredFamilyMember
        {
            get { return IsMemberOfFamily && !IsRegistered && !IsPublicViewer; }
        }

        public bool IsSuperUser
        {
            get { return RoleFlags.HasFlag(Role.SuperUser); }
        }

        public bool IsKid
        {
            get { return RoleFlags.HasFlag(Role.Kid); }
        }

        [Required]
        public virtual PersonName Name { get; set; }        
        public virtual Image ProfilePicture { get; set; }
        public virtual string Bio { get; set; }
        public virtual string InvitationCode { get; set; }

        //[ForeignKey("Family")]
        //public int? FamilyId { get; set; }
        public virtual Family Family { get; set; }

        //[InverseProperty("User")]
        //public virtual ICollection<FamilyMember> FamilyMemberships { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; }
        public virtual ICollection<TimelineEvent> TimelineEvents { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}