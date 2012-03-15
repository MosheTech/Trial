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
        UnregisteredMember = 1,
        PublicViewer = 1 << 1,
        RegisteredMember = 1 << 2,
        FamilyAdmin = 1 << 3,
        SuperUser = 1 << 4
    }
}