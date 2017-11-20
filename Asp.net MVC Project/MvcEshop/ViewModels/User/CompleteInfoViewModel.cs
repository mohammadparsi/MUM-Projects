using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.User
{
    public class CompleteInfoViewModel
    {
        public CompleteInfoViewModel()
        {

        }

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




        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.FatherName)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string FatherName { get; set; }


        //[Remote(action: "CheckEmailUniqueness",
        //    controller: "User", ErrorMessage=null, 
        //    ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "UniquenessValidator")]

        //[EmailAddress( ErrorMessage=null, ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RegularExpressionValidator")]

        ////[StringLength(maximumLength: 250)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "Email")]

        //[Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string Email { get; set; }


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




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SecretAnswer)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SecretAnswer { get; set; }




        [DataType(DataType.Password)]

        [System.ComponentModel.DataAnnotations.Compare(otherProperty: "Password",
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "CompareValidator")]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ConfirmPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ConfirmPassword { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.BirthDate)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime BirthDate { get; set; }

        //[StringLength(maximumLength: 300)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "SecretQuestion")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string SecretQuestion { get; set; }

        //[StringLength(maximumLength: 300)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "SecretAnswer")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string SecretAnswer { get; set; }

        [Infrastructure.CustomValidation.IranianNationalCode
            (ErrorMessage = null,
           ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        //[RegularExpression
        //    (pattern: MvcEshop.Models.RegularExpressions.NationalCode,
        //    ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.NationalCode)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string NationalCode { get; set; }




        



        //[DisplayName(Resources.DisplayNames.Gender)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Gender")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public Nullable<int> GenderId { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SecretQuestion)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public Nullable<int> SecretQuestionId { get; set; }





        [RegularExpression
            (pattern: MvcEshop.Models.RegularExpressions.CellPhoneNumber,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RegularExpressionValidator)]

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.CellPhoneNumber)]
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        public string CellPhoneNumber { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SendNotificationSmsAfterReportIsReady)]
        public bool SendSms { get; set; }





        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.AdvertisementByIrandoc)]
        public bool AdvertisementByIrandoc { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.AdvertisementByOtherOrgs)]
        public bool AdvertisementByOtherOrgs { get; set; }
    }
}