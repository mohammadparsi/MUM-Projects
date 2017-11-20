using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace MvcEshop.ViewModels.Account
{
    [DisplayName("حساب کاربری")]
    [DisplayPluralName("حساب های کاربری")]
    public class EditViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountTitle")]
       
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        
        [StringLength(maximumLength: 30)]
        public string AccountTitle { get; set; }

        
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "JoinPassword")]
        
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages),
        //    ErrorMessageResourceName = "RequiredFieldValidator")]
        
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string JoinPassword { get; set; }


        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]
 
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountManagerEmail")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        
        [StringLength(maximumLength: 250)]
        public string Email { get; set; }


        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Active")]
        //[Required(ErrorMessage = "لطفا گزینه ای را برای وضعیت {0} انتخاب کنید")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Deleted")]
        //[Required(ErrorMessage = "لطفا گزینه ای را برای وضعیت {0} انتخاب کنید")]
        public bool IsDeleted { get; set; }

        
        

       

        
    }
}