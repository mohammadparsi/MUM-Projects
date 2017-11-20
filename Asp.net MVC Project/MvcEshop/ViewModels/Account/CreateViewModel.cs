using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;





namespace MvcEshop.ViewModels.Account
{
    
     
    //[DisplayName("")]
    //[DisplayPluralName(  .DisplayNames.Accounts)]
    public class CreateViewModel : System.Object
    {
        public CreateViewModel()
        {
            
        }

        //[System.ComponentModel.DataAnnotations.Display(Name = "نام")]
        //public string FirstName { get; set; }

        //[System.ComponentModel.DataAnnotations.Display(Name = "نام خانوادگی")]
        //public string LastName { get; set; }
        public System.Guid AccountId { get; set; }

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountManagerEmail")]
        
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Email { get; set; }

        
        //[Display(ResourceType = typeof(Resources.DisplayNames), Name = "JoinPassword")]
        
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        
        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        //public string JoinPassword { get; set; }

        
        //[MaxLength(length:5)] works only for server side validation
        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountTitle")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string AccountTitle { get; set; }
    }
}