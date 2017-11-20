using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.ViewModels.Assignment
{
    public class ShowResultViewModel
    {
        public ShowResultViewModel()
        {

        }

        public ShowResultViewModel
            (string finalReport, int totalPercentage, ICollection<MvcEshop.Models.Result> results,
            int currentPos, int totalParts, string wordFileNameInServer, string pdfReportFileNameInServer, Guid assignmentFileId)
        {
            FinalReport = finalReport;
            TotalPercentage = totalPercentage;
            Results = results;
            CurrentPos = currentPos;
            TotalParts = totalParts;
            PDFReportFileNameInServer = pdfReportFileNameInServer;
            WordFileNameInServer = wordFileNameInServer;
            AssignmentFileId = assignmentFileId;
        }
        public string FinalReport { get; set; }
        public int TotalPercentage { get; set; }
        public int CurrentPos { get; set; }
        public int TotalParts { get; set; }
        public ICollection<MvcEshop.Models.Result> Results { get; set; }
        public string WordFileNameInServer { get; set; }
        public string PDFReportFileNameInServer { get; set; }
        public Guid AssignmentFileId { get; set; }
    }
}