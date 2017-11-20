using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Contract
{
    public class CreateViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Customer")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public System.Guid CustomerId { get; set; }
        
        //[DataType(DataType.Date)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ContractIssueDate")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/mm/yy")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime IssueDate { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ContractStartDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime StartDate { get; set; }

        //[DataType(DataType.Date)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ContractExpirationDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime ExpirationDate { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "VolumeInKBytes")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public int VolumeInKBytes { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "PriceInRials")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public int PriceInRials { get; set; }




        //[StringLength(maximumLength: 200)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "FullName")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string Name { get; set; }
        
        
        
        //[EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RegularExpressionValidator")]

        //[StringLength(maximumLength: 250)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "NotificationEmail")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string NotificationEmail { get; set; }



        //[EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RegularExpressionValidator")]

        //[StringLength(maximumLength: 250)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountManagerEmail")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string AccountManagerEmail { get; set; }



        



        //[StringLength(maximumLength: 20)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "Phone")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string Phone { get; set; }


        //[StringLength(maximumLength: 20)]
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "ContactPersonForInstitutes")]

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        //public string ContactPerson { get; set; }
    
    }
}