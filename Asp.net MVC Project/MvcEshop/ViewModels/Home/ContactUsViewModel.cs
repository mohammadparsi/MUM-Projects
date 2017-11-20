using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.Home
{
    public class ContactUsViewModel
    {

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        //[StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.Email)]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidator)]
        public string Email { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.QuestionSuggestionOrCritic)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidator)]

        //[StringLength(maximumLength: 100)]
        public string Message { get; set; }

    }
}