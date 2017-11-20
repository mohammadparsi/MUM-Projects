using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Shared
{
    public class SlideShowItemViewModel
    {
        public SlideShowItemViewModel(string fileName, int order)
        {
            FileName = fileName;
            Order = order;
        }

        public string FileName { get; set; }
        public int Order { get; set; }
    }
}