using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Assignment
{
    public class FolderUploadViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.AccountToBeUsed)]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public System.Guid AccountId { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInIrandoc)]
        public bool MustSearchInIrandoc { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInInternet)]
        public bool MustSearchInInternet { get; set; }


    }
}