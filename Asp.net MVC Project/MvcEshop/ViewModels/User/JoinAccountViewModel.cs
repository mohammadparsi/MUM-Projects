using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.User
{
    public class JoinAccountViewModel
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "AccountId")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.Guid AccountId { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "JoinPassword")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        //[StringLength(maximumLength: 20, MinimumLength = 8)]
        public string JoinPassword { get; set; }
    }
}