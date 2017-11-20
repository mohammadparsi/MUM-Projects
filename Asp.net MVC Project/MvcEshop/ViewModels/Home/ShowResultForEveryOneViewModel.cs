using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Home
{
    public class ShowResultForEveryOneViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.ReportCode)]

        [System.ComponentModel.DataAnnotations.Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ReportCode { get; set; }
    }
}