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
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public Request()
        {
            this.Results = new HashSet<Result>();
        }
    
        public System.DateTime CreateDate { get; set; }
        public System.Guid AssignmentFileId { get; set; }
        public int RequestId { get; set; }
        public string FinalReport { get; set; }
        public Nullable<int> SimilarityPercentage { get; set; }
    
        public virtual AssignmentFile AssignmentFile { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}