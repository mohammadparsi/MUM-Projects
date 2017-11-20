using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.User
{
    public class RecoverMyPasswordViewModel
    {

        [Infrastructure.CustomValidation.ExistingEmailAddress
            (ErrorMessage = null,
           ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = Resources.Strings.MessagesKeys.ExistenceValidator)]

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.Email)]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidator)]
        public string Email { get; set; }
    }
}