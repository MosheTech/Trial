using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public enum Permission
    {
        ReadUser,
        ReadUserPersonalData,
        UpdateUser,
        DeleteUser,
        EditFamily,
        ViewFamily,
        SuperUserAccess,
        UploadImage,
        TextPost,
        Reply
    }
}