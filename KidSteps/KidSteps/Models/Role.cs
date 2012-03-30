using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{    
    [Flags]
    public enum Role
    {
        None = 0,
        MemberOfFamily = 1,
        PublicViewer = 1 << 1,
        Registered = 1 << 2,
        FamilyAdmin = 1 << 3,
        SuperUser = 1 << 4,
        Kid = 1 << 5
    }
}