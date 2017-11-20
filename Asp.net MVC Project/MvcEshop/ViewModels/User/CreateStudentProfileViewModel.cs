using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.User
{
    public class CreateStudentProfileViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ClassId")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.Guid ClassId { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "EnrollmentPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        public string EnrollmentPassword { get; set; }



        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "FirstName")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string FirstName { get; set; }


        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "LastName")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string LastName { get; set; }



        [Infrastructure.CustomValidation.UniqueEmailAddress
            (ErrorMessage = null,
           ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "UniquenessValidator")]

        [Remote(action: "CheckEmailUniqueness",
            controller: "User", ErrorMessage = null,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "UniquenessValidator")]

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]

        //[StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Email")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Email { get; set; }


        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        [DataType(DataType.Password)]

        [RegularExpression
            (pattern: MvcEshop.Models.RegularExpressions.Password,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "PasswordPolicy_aA1__8_20")]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Password")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Password { get; set; }


        [DataType(DataType.Password)]

        [System.ComponentModel.DataAnnotations.Compare(otherProperty: "Password",
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "CompareValidator")]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ConfirmPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ConfirmPassword { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }

        [StringLength(maximumLength: 300)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "SecretQuestion")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SecretQuestion { get; set; }

        [StringLength(maximumLength: 300)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "SecretAnswer")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SecretAnswer { get; set; }


        //[DisplayName(Resources.DisplayNames.Gender)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Gender")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public Nullable<int> GenderId { get; set; }

    }
}