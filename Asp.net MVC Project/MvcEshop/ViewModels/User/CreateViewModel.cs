using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.User
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
            //AllocatedVolume = 0;
            //ExpirationDate = null;
        }

        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "FirstName")]
        
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        public string FirstName { get; set; }


        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "LastName")]
        
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        public string LastName { get; set; }


        //[Remote(action: "CheckEmailUniqueness",
        //   controller: "User", ErrorMessage = null,
        //   ErrorMessageResourceType = typeof(Resources.Messages),
        //   ErrorMessageResourceName = "UniquenessValidator")]

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Email")]
        
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Email { get; set; }



        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
          Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]

        [System.ComponentModel.DataAnnotations.Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidatorForDropDownList)]
        public System.DateTime ExpirationDate { get; set; }



        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.AllocatedVolume)]

        [System.ComponentModel.DataAnnotations.Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidator)]
        public double AllocatedVolume { get; set; }
    }
}