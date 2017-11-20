using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.User
{
    public class LoginViewModel
    {

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        //[StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.Email )]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidator)]
        public string Email { get; set; }


        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        //[RegularExpression
        //    (pattern: MvcEshop.Models.RegularExpressions.Password,
        //    ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "PasswordPolicy_aA1__8_20")]

        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.Password)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidator)]

        [DataType(DataType.Password)]
        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.RememberMe)]
        public bool RememberMe { get; set; }

    }
}