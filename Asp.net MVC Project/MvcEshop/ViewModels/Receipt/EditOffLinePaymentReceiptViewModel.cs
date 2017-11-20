using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Receipt
{
    public class EditOffLinePaymentReceiptViewModel
    {
        public EditOffLinePaymentReceiptViewModel()
        {


        }
       

        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.PaymentDate)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime? PaymentDate { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime? ExpirationDate { get; set; }





        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.PaidAmountInRial)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public int PaidAmountInRial { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInIrandoc)]
        public bool MustSearchInIrandoc { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInInternet)]
        public bool MustSearchInInternet { get; set; }



    }
}