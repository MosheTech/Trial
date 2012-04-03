using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KidSteps.Models;

namespace KidSteps.ViewModels
{
    public class ImageSelectViewModel
    {
        public long? SelctedImageId { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}