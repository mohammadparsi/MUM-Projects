using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.User
{
    public class EditCreditViewModel
    {
        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = Resources.Strings.DisplayNamesKeys.VolumeInKBytes)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public double AllocatedVolume { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.ExpirationDateForVolumeUse)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public  System.DateTime ExpirationDate { get; set; }


    }
}