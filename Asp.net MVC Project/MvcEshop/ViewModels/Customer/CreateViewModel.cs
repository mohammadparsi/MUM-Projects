using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.Customer
{
    public class CreateViewModel
    {

        public CreateViewModel()
        {

        }

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.CustomerType)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public int CustomerTypeId { get; set; }
        




         [Remote(action: "CheckNameUniqueness",
           controller: "Customer", ErrorMessage = null,
           ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "UniquenessValidator")]
        
        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "FirstName")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        
        public string Name { get; set; }




        
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Email")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Email { get; set; }





        [StringLength(maximumLength: 15)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Phone")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Phone { get; set; }




        [StringLength(maximumLength: 60)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ContactPerson")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ContactPerson { get; set; }


        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "NotificationEmail")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string NotificationEmail { get; set; }





        [Display(ResourceType = typeof(Resources.DisplayNames), 
            Name = Resources.Strings.DisplayNamesKeys.UsedToAllocatedVolumePercentageToNotify)]
        
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        
        [Range(typeof(int), "0", "100", ErrorMessageResourceType=typeof(Resources.Messages)
            , ErrorMessageResourceName=Resources.Strings.MessagesKeys.RangeValidator)]
        public int UsedToAllocatedVolumePercentageToNotify { get; set; }


        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountManagerEmail")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string AccountManagerEmail { get; set; }

    }
}