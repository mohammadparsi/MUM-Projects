using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Receipt
{
    public class EditFreeCreditReceiptViewModel
    {
        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]
        [System.ComponentModel.DataAnnotations.DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]

        [System.ComponentModel.DataAnnotations.Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = Resources.Strings.MessagesKeys.RequiredFieldValidatorForDropDownList)]
        public System.DateTime? ExpirationDate { get; set; }




        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.AllocatedVolume)]

        [System.ComponentModel.DataAnnotations.Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        public double AllocatedVolume { get; set; }





        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInIrandoc)]
        public bool MustSearchInIrandoc { get; set; }



        [System.ComponentModel.DataAnnotations.Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInInternet)]
        public bool MustSearchInInternet { get; set; }


    }
}