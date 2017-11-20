using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Receipt
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
           
            
        }
        //[Display(ResourceType = typeof(Resources.DisplayNames),
        //    Name = Resources.Strings.DisplayNamesKeys.Account)]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        //public System.Guid AccountId { get; set; }

        
        [Display(ResourceType = typeof(Resources.DisplayNames), 
            Name = Resources.Strings.DisplayNamesKeys.PaymentDate)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime? PaymentDate { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime? ExpirationDate { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), 
            Name = Resources.Strings.DisplayNamesKeys.AllocatedVolume)]
  
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]

        //[RegularExpression
        //    (pattern: MvcEshop.Models.RegularExpressions.DoubleNumber,
        //    ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = Resources.Strings.MessagesKeys.DataMustBeNumber)]
        //[Range(typeof(Decimal), "0", "10000000000000000", ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = Resources.Strings.MessagesKeys.VolumeTypeValidator)]
        //[DataAnnotationsExtensions.Numeric(ErrorMessage = "{0} باید از نوع عدد صحیح باشد.")]

       // [Infrastructure.CustomValidation.DataVolume
       // (ErrorMessage = null,
       //ErrorMessageResourceType = typeof(Resources.Messages),
       //ErrorMessageResourceName = Resources.Strings.MessagesKeys.VolumeTypeValidator)]
        public string AllocatedVolume { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), 
            Name = Resources.Strings.DisplayNamesKeys.PaidAmountInRial)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]

        //[RegularExpression
        //    (pattern: MvcEshop.Models.RegularExpressions.Money,
        //    ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = Resources.Strings.MessagesKeys.MoneyTypeValidator)]
        //[DataType()]
        //[Range(typeof(int), "0", "1000000000", ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = Resources.Strings.MessagesKeys.MoneyTypeValidator)]
  //      [Infrastructure.CustomValidation.Money
  // (ErrorMessage = null,
  //ErrorMessageResourceType = typeof(Resources.Messages),
  //ErrorMessageResourceName = Resources.Strings.MessagesKeys.MoneyTypeValidator)]
        public string PaidAmountInRial { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.ReceiptImageFile)]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]

        [StringLength(maximumLength: 100)]
        public string ImageFile { get; set; }



        public string GiveCreditApproach { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInIrandoc)]
        public bool MustSearchInIrandoc { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInInternet)]
        public bool MustSearchInInternet { get; set; }



    }
}