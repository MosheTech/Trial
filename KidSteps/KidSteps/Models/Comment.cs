﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class Comment : TimelineEvent
    {
        public string Text { get; set; }
    }
}