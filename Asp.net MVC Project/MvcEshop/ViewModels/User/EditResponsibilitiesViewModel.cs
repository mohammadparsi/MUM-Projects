using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MvcEshop.ViewModels.User
{
    public class EditResponsibilitiesViewModel
    {





        [Display(Name = "مدیریت گروه‌ها")]
        public bool CustomersManagement { get; set; }



        [Display(Name = "مدیریت حساب‌های اعتباری")]
        public bool AccountsManagement { get; set; }


        [Display(Name = "مدیریت تعرفه‌ها")]
        public bool TariffManagement { get; set; }


        [Display(Name = "مدیریت محتوای صفحات")]
        public bool PageContentManagement { get; set; }
    }
}