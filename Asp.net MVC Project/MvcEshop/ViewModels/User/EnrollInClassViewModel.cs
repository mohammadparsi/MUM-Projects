using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.User
{
    public class EnrollInClassViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.ClassId)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.Guid ClassId { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.EnrollmentPassword)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        public string EnrollmentPassword { get; set; }
    }
}