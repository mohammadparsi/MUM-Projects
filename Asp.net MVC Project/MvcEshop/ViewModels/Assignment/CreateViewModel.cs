using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Assignment
{
    public class CreateViewModel
    {
        [Display(ResourceType = typeof(Resources.DisplayNames),
           Name = "AssignmentTitle")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]

        [StringLength(maximumLength: 60)]
        public string Title { get; set; }

      
        
        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "PointValue")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public int PointValue { get; set; }


        
        
        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = "StartDate")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime StartDate { get; set; }



        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = "DueDate")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime DueDate { get; set; }




        [Display(ResourceType = typeof(Resources.DisplayNames),
            Name = "PostDate")]

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public System.DateTime PostDate { get; set; }
    }
}