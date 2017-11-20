using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

namespace MvcEshop.Models
{
    [DisplayName("کاربر")]
    [DisplayPluralName("کاربران")]
    class UserMetadata
    {
        public System.Guid UserId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name="نام")]
        public string FirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "ایمیل")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "رمز عبور")]
        public string Password { get; set; }

        //[Display(Name = "جنسیت")]
        //[Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int GenderId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "سوال امنیتی")]
        public string SecretQuestion { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "جواب سوال امنیتی")]
        public string SecretAnswer { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public System.Guid ActiveCode { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
