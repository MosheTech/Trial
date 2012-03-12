﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{    
    public enum Role
    {
        None,
        UnregisteredFamilyMember,
        Read,
        Write,
        FamilyAdmin,
        SuperUser
    }
}