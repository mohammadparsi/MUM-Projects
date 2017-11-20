using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.User
{
    public class AddSystemExpertViewModel
    {
       

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Email")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Email { get; set; }



        //[Display(Name = "مدیریت گروه‌ها")]
        //public bool CustomersManagement { get; set; }



        //[Display( Name = "مدیریت حساب‌های اعتباری")]
        //public bool AccountsManagement { get; set; }


        //[Display( Name = "مدیریت تعرفه‌ها")]
        //public bool TariffManagement { get; set; }


        //[Display( Name = "مدیریت محتوای صفحات")]
        //public bool PageContentManagement { get; set; }
    }
}