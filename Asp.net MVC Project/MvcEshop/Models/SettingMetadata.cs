using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MvcEshop.Models
{
    class SettingMetadata
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string Domain { get; set; }

        
        public int SettingId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SmtpServer { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public int SmtpServerPort { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SenderEmailAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SenderEmailPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string AssignmentFilesPath { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string SupportTeamEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string CustomerEmailForDFService { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = "RequiredFieldValidator")]
        public string ImageUrlForMailMessageHeader { get; set; }
    }
}
