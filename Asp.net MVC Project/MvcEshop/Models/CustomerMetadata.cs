using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcEshop.Models
{
    class CustomerMetadata
    {
        public System.Guid CustomerId { get; set; }


        [StringLength(maximumLength: 30)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "FirstName")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Name { get; set; }


        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
           ErrorMessageResourceName = "RegularExpressionValidator")]

        [StringLength(maximumLength: 250)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Email")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Email { get; set; }


        [StringLength(maximumLength: 15)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Phone")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Phone { get; set; }


        [StringLength(maximumLength: 60)]
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ContactPerson")]

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ContactPerson { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
