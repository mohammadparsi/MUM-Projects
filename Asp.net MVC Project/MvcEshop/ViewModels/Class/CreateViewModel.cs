using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Class
{
    public class CreateViewModel
    {


        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Account")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidatorForDropDownList")]
        public System.Guid AccountId { get; set; }
        

        
        
        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = "ClassTitle")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        [StringLength(maximumLength: 30)]
        public string Title { get; set; }



        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "EnrollmentPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        public string EnrollmentPassword { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = "StartDate")]
        
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime StartDate { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = "EndDate")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime EndDate { get; set; }
    }
}