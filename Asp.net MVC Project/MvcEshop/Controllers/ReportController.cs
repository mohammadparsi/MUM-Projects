using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.Controllers
{
    public partial class ReportController : Infrastructure.BaseControllerWithDatabaseContext
    {

        //string _reportsFolderPath;

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);

        //    // now Server has been initialized
        //    _reportsFolderPath = Server.MapPath("~/Infrastructure/Reports");

        //}



        [HttpGet]
        public virtual ActionResult InitializationBeforeSend(Guid? id)
        {
            if (id.Value == null)
            {
                return HttpNotFound();
            }

            MvcEshop.Models.Request oRequest =
                MVC.Assignment.GetAssignmentFileById(id.Value).Request;

            if (oRequest == null || string.IsNullOrEmpty(oRequest.FinalReport))
            {

                //send the client to the assignment inbox and show the appropriate message to them.
                TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

                return RedirectToAction
                    (MVC.Assignment.PreviousRequests(MVC.User.GetUserIdByEmail(User.Identity.Name)));

            }

            System.Web.HttpContext.Current.Session["AssignmentFileIdForReportSending"] = id.Value;

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] =
                MVC.Assignment.GetAssignmentFileById(id.Value).SubmissionTitle+ " " + ">";

            return RedirectToAction(MVC.Report.SendReport());
        }

        [HttpGet]
        public virtual ActionResult SendReport()
        {

            return View();
        }



        //[HttpPost]
        //public virtual PartialViewResult SendReportToEmail(string email)
        //{
        //    

        //    List<MvcEshop.Models.Customer> lstCustomers = MVC.Customer.GetExistingCustomers()
        //        .Where(current => current.Name.Contains(customerName))
        //        .ToList();
        //    //ViewBag.ListOfCustomers = lstCustomers;
        //    return PartialView("_Partial_CustomersSearchResults", lstCustomers);

        //}


        [HttpPost]
        public virtual PartialViewResult GetCustomersSearchResults(string customerName)
        {
            

            List<MvcEshop.Models.Customer> lstCustomers = MVC.Customer.GetExistingCustomers()
                .Where(current => current.Name.Contains(customerName) && current.IsConfirmed == true)
                .ToList();
            //ViewBag.ListOfCustomers = lstCustomers;
            return PartialView("_Partial_CustomersSearchResults", lstCustomers);

        }




        [HttpPost]
        public virtual JsonResult AddReceiver(string customerId)
        {
            System.Guid guidCustomerId = System.Guid.Parse(customerId);

            System.Guid guidFileId = System.Guid.Parse
                (System.Web.HttpContext.Current.Session["AssignmentFileIdForReportSending"].ToString());

            System.Web.HttpContext.Current.Session["AssignmentFileIdForReportSending"] = guidFileId;

            MvcEshop.Models.AssignmentFile oFile =
                MVC.Assignment.GetAssignmentFileById(guidFileId);

            MvcEshop.Models.Customer oCustomer = MVC.Assignment.DFEntities.Customers
                .Where(current => current.IsDeleted == false && current.CustomerId == guidCustomerId)
                .ToList()
                .FirstOrDefault();

            oCustomer.Accounts.FirstOrDefault().AssignmentFiles.Add(oFile);
            //MVC.Customer.DFEntities.Entry(oCustomer).State = EntityState.Detached;

            MVC.Assignment.DFEntities.Entry(oCustomer).State = System.Data.Entity.EntityState.Modified;
            MVC.Assignment.DFEntities.SaveChanges();

            string strMessage =
                string.Format(Resources.Messages.DocFindReportSentSuccessfullyToThePanel,
                Resources.DisplayNames.DocFindingReport, Resources.DisplayNames.Panel,
                oCustomer.Accounts.FirstOrDefault().AccountTitle);

            var varJsonResult =
                Json(new { MyMessage = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet);

            return (varJsonResult);
        }



        [HttpPost]
        public virtual PartialViewResult GetSendingConfirmationPartialView(string email)
        {
            

            MvcEshop.ViewModels.User.MessageContentViewModel oMessageContentViewModel =
                new ViewModels.User.MessageContentViewModel();


            if (new Infrastructure.RegexUtilities().IsValidEmail(email) == false)
            {
                oMessageContentViewModel.Message = string.Format(Resources.Messages.RegularExpressionValidator, email);
                oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.failure;

                goto end;
            }

            System.Guid guidFileId = System.Guid.Parse
                (System.Web.HttpContext.Current.Session["AssignmentFileIdForReportSending"].ToString());

            System.Web.HttpContext.Current.Session["AssignmentFileIdForReportSending"] = guidFileId;

            string strPDFReportFilePath;

            CreatePDFReportFileByAssignmentFileIdAndSaveInServer(guidFileId, out strPDFReportFilePath);

            //create appropriate email message
            string strMainMessage =
                            string.Format(Resources.Messages.PDFReportIsAttachedToTheMessage,
                            Resources.DisplayNames.DocFindingReport,
                            MVC.Assignment.GetAssignmentFileById(guidFileId).UploaderUser.FullName);

            string strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strMainMessage);

            //send pdf report to the email address.
            Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    email, Resources.DisplayNames.DocFindingReport, strEmailBody, strPDFReportFilePath);

            //List<MvcEshop.Models.Result> ResultsToShowInPDFReport =
            //    oAssignmentFile.Request.Results.ToList();

            oMessageContentViewModel.Message =
                string.Format(Resources.Messages.ReportSentSuccessfullyToEmail,
                Resources.DisplayNames.DocFindingReport, email);

            oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.success;


        end:

            return PartialView("_Partial_MessageBox", oMessageContentViewModel);

        }

        //[NonAction]
        //public virtual ActionResult CreatePDFReportBasedOnInput(string userFullName, Guid docCode, List<MvcEshop.Models.Result>)
        //{
        //    Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[1];
        //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("DocFindingReportCode", "Test", false);
        //    //params[1] = new ReportParameter("Quantity ", p.Quantity , false);

        //    string path = System.IO.Path.Combine(Server.MapPath("~/Infrastructure/Reports"), "DocFindingReport.rdlc");
        //    Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
        //    lr.ReportPath = path;
        //    lr.SetParameters(parameters);

        //    string reportType = "PDF";
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;

        //    Microsoft.Reporting.WebForms.Warning[] warnings;
        //    string[] streams;
        //    byte[] renderedBytes;

        //    renderedBytes = lr.Render(
        //        reportType,
        //        null,
        //        out mimeType,
        //        out encoding,
        //        out fileNameExtension,
        //        out streams,
        //        out warnings);

        //    return File(renderedBytes, mimeType);
        //}

        /// <summary>
        /// creates appropriate doc-finding pdf report for a specific assignment file and save the pdf file in server. finally 
        /// returns the pdf file path to be used for the future.
        /// </summary>
        /// <param name="assignmentFileId"></param>
        /// <param name="PDFFilePath"></param>
        [NonAction]
        public void CreatePDFReportFileByAssignmentFileIdAndSaveInServer(Guid? assignmentFileId, out string PDFFilePath)
        {
            MvcEshop.Models.AssignmentFile oAssignmentFile =
                MVC.Assignment.GetAssignmentFileById(assignmentFileId.Value);

            if (string.IsNullOrEmpty(oAssignmentFile.PDFReportFileNameInServer))
            {
                List<MvcEshop.Models.Result> ResultsToShowInPDFReport = new List<Models.Result>();

                if (oAssignmentFile.Request.Results.Count!=0)
                {
                    ResultsToShowInPDFReport =
                                oAssignmentFile.Request.Results
                                .OrderByDescending(current => current.SimilarityPercentage)
                                .ToList(); 
                }

                else
                {
                    MvcEshop.Models.Result oResult = new Models.Result();

                    oResult.Address = Resources.Messages.NoCopyFound;
                    oResult.SimilarityPercentage = 0;

                    ResultsToShowInPDFReport.Add(oResult);
                }


                Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[7];

                //Generate a ReportCode for the assignmentFile and save in db.
                MvcEshop.Models.AssignmentFile oFile = 
                    MVC.Assignment.GetAssignmentFileById(assignmentFileId.Value);

                oFile.ReportCode =
                    System.Guid.NewGuid().ToString().Replace("-", "").Substring(1, 10);

                MVC.Assignment.DFEntities.Entry(oFile).State = EntityState.Modified;
                MVC.Assignment.DFEntities.SaveChanges();


                parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("DocFindingReportCode", oFile.ReportCode, false);
                parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("UserFullName", oAssignmentFile.UploaderUser.FullName, false);
                parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Title", oAssignmentFile.SubmissionTitle, false);
                parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("SenderEmailAddress", Setting.SenderEmailAddress, false);

                parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter
                    ("TodayDate", Infrastructure.Convert.ToDisplayDate(Infrastructure.Convert.ToPersian(System.DateTime.Now)), false);

                parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter
                    ("ShowResultActionAddress", Setting.Domain + Setting.ShowResultActionAddress, false);
                //params[1] = new ReportParameter("Quantity ", p.Quantity , false);
                string strResourcesToCompare = string.Empty;

                if (oAssignmentFile.SearchInIrandoc)
                {
                    strResourcesToCompare = "پایگاه‌های ایرانداک";
                }

                if (oAssignmentFile.SearchInInternet)
                {
                    if (string.IsNullOrEmpty(strResourcesToCompare) == false)
                    {
                        strResourcesToCompare += " و ";
                    }

                    strResourcesToCompare += "اینترنت";
                }

                parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("ResourcesToCompare", strResourcesToCompare, false);


                string path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Reports"), "DocFindingReport.rdlc");
                Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                Microsoft.Reporting.WebForms.ReportDataSource oReportDataSource =
                    new Microsoft.Reporting.WebForms.ReportDataSource("ResultsDataSet", ResultsToShowInPDFReport);

                lr.DataSources.Add(oReportDataSource);

                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                Microsoft.Reporting.WebForms.Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    null,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                //save a pdf file
                string strFileName =
                    System.Guid.NewGuid().ToString() + ".pdf";

                string strFilePath =
                    Infrastructure.File.GetFilePath(Setting.AssignmentFilesPath, strFileName);

                using (System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Create))
                {
                    fs.Write(renderedBytes, 0, renderedBytes.Length);
                }

                PDFFilePath = strFilePath;

                //save fileNameInServer in db to be used in the future for download.
                oAssignmentFile.PDFReportFileNameInServer = strFileName;

                MVC.Assignment.DFEntities.Entry(oAssignmentFile).State = EntityState.Modified;
                MVC.Assignment.DFEntities.SaveChanges();

            }

            else
            {

                PDFFilePath = Infrastructure.File.GetFilePath
                    (Setting.AssignmentFilesPath, oAssignmentFile.PDFReportFileNameInServer);
            }
        }

        [Authorize(Roles = "SystemAdministrator, AccountManager")]
        public virtual ActionResult CreateExcelReportForCurrentCreditOfAccounts()
        {
            
          
            List<MvcEshop.Models.Account> listOfAccounts = new List<Models.Account>();

            //if the current role of the user is not "SystemAdministrator"
            if (System.Convert.ToInt32
                   (System.Web.HttpContext.Current.Session["RoleId"].ToString()) != 2)
            {
                //the report will show only the accounts that their manager is the current user.
                var varAccounts =
                DFEntities.Accounts
                .Where(current => current.IsDeleted == false && current.Customer.IsConfirmed == true)
                .OrderByDescending(current => current.CreateDate)
                .ToList();

                //get the userId of current User.
                Guid guidCurrentUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);

                listOfAccounts=
                     varAccounts = varAccounts
                    .Where(current => current.ManagerUserId == guidCurrentUserId && current.Customer.IsConfirmed == true)
                    .ToList();

            }
                //if the current role of the current user is "SystemAdministrator"
            else
            {
                //the report will show all the accounts.
                listOfAccounts=
                DFEntities.Accounts
                    .Where(current => current.IsDeleted == false && current.Customer.IsConfirmed == true)
                    .OrderByDescending(current => current.CreateDate)
                    .ToList();
                
            }


            List<MvcEshop.Models.AccountCredit> AccountCredits = new List<Models.AccountCredit>();

            foreach (MvcEshop.Models.Account account in listOfAccounts)
            {
                if (account.CurrentContractReceipt != null)
                {

                    AccountCredits.Add(new MvcEshop.Models.AccountCredit
                        (account.CurrentContractReceipt.UsedVolume.Value,
                        account.CurrentContractReceipt.RemainingVolume,
                        account.CurrentContractReceipt.ExpirationDateForUse.Value,
                        account.AccountTitle));
                }


            }

            string path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Reports"), "CreditOfEachAccountReport.rdlc");
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = path;
            //lr.SetParameters(parameters);

            Microsoft.Reporting.WebForms.ReportDataSource oReportDataSource =
                new Microsoft.Reporting.WebForms.ReportDataSource("AccountCredits", AccountCredits);

            lr.DataSources.Add(oReportDataSource);

            string reportType = "Excel";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                null,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        [HttpGet]
        public virtual ActionResult CreateInvoiceReportByAccountIdInPDF(Guid? id)
        {
            MvcEshop.Models.Account oAccount =
               MVC.Account.GetExistingAccounts()
               .Where(current => current.AccountId == id.Value)
               .ToList()
               .FirstOrDefault();

            MvcEshop.Models.Receipt oCurrentContractReceipt = oAccount.CurrentContractReceipt;


            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[4];

            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("AccountTitle", oAccount.AccountTitle, false);

            int intMoneyForUsedVolume =
                       Infrastructure.BusinessLogic.Float.CalculateMoney
                       (oCurrentContractReceipt.UsedVolume.Value, oCurrentContractReceipt.MustSearchInIrandoc.Value,
                       oCurrentContractReceipt.MustSearchInInternet.Value, oCurrentContractReceipt.CurrentIrandocTariff,
                       oCurrentContractReceipt.CurrentInternetTariff);

            int intCreditorAmount = oCurrentContractReceipt.PaidAmountInRial - intMoneyForUsedVolume;

            parameters[1] =
                new Microsoft.Reporting.WebForms.ReportParameter("CreditorAmount", intCreditorAmount.ToString(), false);

            parameters[2] =
                    new Microsoft.Reporting.WebForms.ReportParameter
                        ("DisplayExpirationDate", oCurrentContractReceipt.DisplayExpirationDate, false);

            parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter
                ("DisplayPaymentDate", oCurrentContractReceipt.DisplayPaymentDate, false);

            string path =
                System.IO.Path.Combine
                (System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Reports"), "InvoiceReportBasedOnReceipts.rdlc");

            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = path;
            lr.SetParameters(parameters);

            List<MvcEshop.Models.Receipt> lstReceipts = new List<Models.Receipt>();
            lstReceipts.Add(oCurrentContractReceipt);

            Microsoft.Reporting.WebForms.ReportDataSource oReportDataSource =
                new Microsoft.Reporting.WebForms.ReportDataSource("CurrentContractReceiptForSpecificAccount", lstReceipts);

            lr.DataSources.Add(oReportDataSource);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                null,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DFEntities.Dispose();
            }
            base.Dispose(disposing);
        }

        //public virtual ActionResult ShowPDFReport(Guid? id)
        //{
        //    MvcEshop.Models.AssignmentFile oAssignmentFile =
        //        MVC.Assignment.GetAssignmentFileById(id.Value);

        //    List<MvcEshop.Models.Result> ResultsToShowInPDFReport = new List<Models.Result>();

        //    ResultsToShowInPDFReport =
        //        oAssignmentFile.Request.Results
        //        .OrderByDescending(current => current.SimilarityPercentage)
        //        .ToList();



        //    Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[6];


        //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("DocFindingReportCode", id.Value.ToString(), false);
        //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("UserFullName", oAssignmentFile.UploaderUser.FullName, false);
        //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Title", oAssignmentFile.SubmissionTitle, false);
        //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("SenderEmailAddress", Setting.SenderEmailAddress, false);

        //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter
        //        ("TodayDate", Infrastructure.Convert.ToDisplayDate(Infrastructure.Convert.ToPersian(System.DateTime.Now)), false);

        //    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("ShowResultActionAddress", Setting.ShowResultActionAddress, false);
        //    //params[1] = new ReportParameter("Quantity ", p.Quantity , false);

        //    string path = System.IO.Path.Combine(Server.MapPath("~/Infrastructure/Reports"), "DocFindingReport.rdlc");
        //    Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
        //    lr.ReportPath = path;
        //    lr.SetParameters(parameters);

        //    Microsoft.Reporting.WebForms.ReportDataSource oReportDataSource =
        //        new Microsoft.Reporting.WebForms.ReportDataSource("ResultsDataSet", ResultsToShowInPDFReport);

        //    lr.DataSources.Add(oReportDataSource);

        //    string reportType = "PDF";
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;

        //    Microsoft.Reporting.WebForms.Warning[] warnings;
        //    string[] streams;
        //    byte[] renderedBytes;

        //    renderedBytes = lr.Render(
        //        reportType,
        //        null,
        //        out mimeType,
        //        out encoding,
        //        out fileNameExtension,
        //        out streams,
        //        out warnings);

        //    //save a pdf file
        //    string strFileName =
        //        System.Guid.NewGuid().ToString()+".pdf";

        //    string strFilePath =
        //        Infrastructure.File.GetFilePath(Setting.AssignmentFilesPath, strFileName);

        //    using (System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Create))
        //    {
        //        fs.Write(renderedBytes, 0, renderedBytes.Length);
        //    }

        //    Infrastructure.Communication.SendEmail
        //            (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
        //            Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
        //            "mohammadparsi@rocketmail.com", "گزارش همانندجویی", "This is test", strFilePath);

        //    return File(renderedBytes, mimeType);
        //}

    }
}