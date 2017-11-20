using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcEshop.Models
{
    class ClassMetadata
    {

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "ClassId")]
        public System.Guid ClassId { get; set; }


        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "EnrollmentPassword")]
        public string EnrollmentPassword { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsExpired { get; set; }
        public System.Guid InstructorUserId { get; set; }
        public System.Guid CreatorUserId { get; set; }

        public virtual User Instructor { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<User> Students { get; set; }

    }
}
