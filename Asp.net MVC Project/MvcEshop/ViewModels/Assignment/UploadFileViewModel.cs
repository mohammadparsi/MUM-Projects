using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Assignment
{
    public class UploadFileViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.AccountToBeUsed)]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public System.Guid AccountId { get; set; }
        


        [Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.PaperTitle)]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]

        [StringLength(maximumLength: 250)]
        public string PaperTitle { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames),
           Name = Resources.Strings.DisplayNamesKeys.TextForDocumentFinding)]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]

        public string Text { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames),
           Name = "PaperFile")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]

        //[StringLength(maximumLength: 100)]
        public HttpPostedFileBase PaperFile { get; set; }


        public string TextSendingApproach { get; set; }

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInIrandoc)]
        public bool MustSearchInIrandoc { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = Resources.Strings.DisplayNamesKeys.SearchInInternet)]
        public bool MustSearchInInternet { get; set; }

        public string UploadingType { get; set; }
    }
}