using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidSteps.Models
{
    public class FamilyEvent
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public Conversation Conversation { get; set; }
    }
}