using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class KidCreateViewModel
    {
        public long FamilyId { get; set; }
        public PersonName Name { get; set; }
    }
}