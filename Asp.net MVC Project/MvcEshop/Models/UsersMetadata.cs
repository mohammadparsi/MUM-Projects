using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;
using MvcEshop.Classes;

namespace MvcEshop.Models
{
    [DisplayName("کاربر")]
    [DisplayPluralName("کاربران")]
    class UsersMetadata
    {

        [Key]
        public int UserID { get; set; }

        [Display(Name = "گروه کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RoleID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [CheckUniqUser(ErrorMessage = "ایمیل تکراری است")]
        public string Email { get; set; }

        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreateDate { get; set; }
    }
}
