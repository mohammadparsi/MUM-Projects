using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

namespace MvcEshop.Models
{
    [DisplayName("حساب کاربری")]
    [DisplayPluralName("حساب های کاربری")]
    class AccountMetadata
    {
        
        public System.Guid AccountId { get; set; }
        public System.Guid CreatorUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.Guid ManagerUserId { get; set; }
        public System.Guid ParentAccountId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "عنوان حساب کاربری")]
        public string AccountTitle { get; set; }

         [Display(ResourceType = typeof(Resources.DisplayNames), Name = "JoinPassword")]
        public string JoinPassword { get; set; }

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "CreateDate")]
        public string DisplayCreateDate
        {
            get
            {
                return Infrastructure.Convert.GregorianToPersian(CreateDate);
            }

        }
    }
}
