using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        public ChangePasswordViewModel()
        {

        }


        [Remote(action: "CheckPasswordCorrectness",
            controller: "User", ErrorMessage = null,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "FieldDataIsNotCorrect")]

        [DataType(DataType.Password)]

        //[RegularExpression
        //    (pattern: MvcEshop.Models.RegularExpressions.Password,
        //    ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "PasswordPolicy_aA1__8_20")]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "PreviousPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string PreviousPassword { get; set; }


        [DataType(DataType.Password)]

        [RegularExpression
            (pattern: MvcEshop.Models.RegularExpressions.Password,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "PasswordPolicy_aA1__8_20")]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "NewPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]

        [System.ComponentModel.DataAnnotations.Compare(otherProperty: "NewPassword",
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "CompareValidator")]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ConfirmNewPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ConfirmNewPassword { get; set; }

    }
}