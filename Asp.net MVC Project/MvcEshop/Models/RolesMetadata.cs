using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.Models
{
    public partial class RolesMetadata
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleTitle { get; set; }

       [Display(Name = "عنوان سیستمی گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleNameInSystem { get; set; }

         [Display(Name = "تاریخ ایجاد")]
         [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreateDate { get; set; }
    }
}