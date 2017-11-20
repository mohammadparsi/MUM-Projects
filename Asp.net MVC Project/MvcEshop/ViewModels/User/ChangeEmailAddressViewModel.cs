using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcEshop.ViewModels.User
{
    public class ChangeEmailAddressViewModel
    {
        //server-side validation for iranian national code.
        [Infrastructure.CustomValidation.IranianNationalCode
            (ErrorMessage = null,
           ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        //client-side validation for email uniqueness
        [Remote(action: "CheckEmailUniqueness",
           controller: "User", ErrorMessage = null,
           ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "UniquenessValidator")]

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]

        //[StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "NewEmail")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string NewEmail { get; set; }
    }
}