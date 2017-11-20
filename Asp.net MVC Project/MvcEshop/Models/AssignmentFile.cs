//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcEshop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;


    public partial class AssignmentFile
    {
        public AssignmentFile()
        {
            FileId = System.Guid.NewGuid();
            UploadDate = System.DateTime.Now;
            IsDeleted = false;
            this.DocFinderServiceErrorLogs = new HashSet<DocFinderServiceErrorLog>();
            this.Receipts = new HashSet<Receipt>();
            this.Accounts = new HashSet<Account>();
            SearchInIrandoc = true;
        }

        public Nullable<bool> NotPaidFor { get; set; }
        public System.Guid FileId { get; set; }
        public string FileName { get; set; }
        public System.Guid UploaderUserId { get; set; }
        public Nullable<System.Guid> AssignmentId { get; set; }
        public string FileNameInServer { get; set; }
        public string PDFReportFileNameInServer { get; set; }
        public string SubmissionTitle { get; set; }
        public System.DateTime UploadDate { get; set; }

        [Display(ResourceType = typeof(Resources.DisplayNames), Name = "UploadDate")]
        public string DisplayUploadDate
        {
            get
            {
                return Infrastructure.Convert.ToDisplayDate
                            (Infrastructure.Convert.ToPersian(UploadDate));
            }

        }


        public virtual Assignment Assignment { get; set; }
        public virtual User UploaderUser { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string TextContent { get; set; }
        public string ReportCode { get; set; }
        public virtual ICollection<DocFinderServiceErrorLog> DocFinderServiceErrorLogs { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual Request Request { get; set; }

        public bool SearchInInternet { get; set; }
        public bool SearchInIrandoc { get; set; }
    }
}