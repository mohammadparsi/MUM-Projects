using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Kendo.Mvc.Examples.Models;

namespace MvcEshop.Controllers
{
    //[Authorize]
    public partial class TestRolesController : Infrastructure.BaseControllerWithDatabaseContext
    {

        [HttpGet]
        public virtual ActionResult TestErrorHandling()
        {
            
            return View();
        }

        public virtual ActionResult TestView(int pageId)
        {
            string str = string.Empty;

            if (pageId==1)
            {
                str = "1";    
            }

            return Content(str);
        }
        //public virtual ActionResult GroupEnrollment(System.Guid? id)
        //{
        //    //id is considered as current account's accountId
        //    if (id != null)
        //    {
        //        TempData["AccountOrClassId"] = id.Value;

        //        //the following session variable could be used in other controllers like assignmentController.
        //        System.Web.HttpContext.Current.Session["AccountOrClassId"] = id.Value;
        //    }

        //    return View();
        //}


        //[HttpPost]
        //public virtual ActionResult GroupEnrollment(HttpPostedFileBase excelFile)
        //{
        //    string strFileNameWithoutExtension = System.Guid.NewGuid().ToString();

        //    string strFilePath = string.Format
        //                    (@"{0}", DFEntities.Settings.FirstOrDefault().EmailListExcelFilesPath)
        //                    + strFileNameWithoutExtension + System.IO.Path.GetExtension(excelFile.FileName);

        //    if (excelFile != null)
        //    {
        //        excelFile.SaveAs(strFilePath);
        //    }

        //    //strFilePath.Replace("\", "\\");
        //    //strFilePath.Replace('\','x');
        //    Infrastructure.ExcellUtility.ImportFromExcelToSqlServer
        //        (strFilePath.Replace(@"\", "\\"), strFileNameWithoutExtension, "EmailList", "EmailList");

        //    System.Web.HttpContext.Current.Session["ErrorLogForBeingUsedAsNonActionMethod"] = new List<string>();

        //    foreach (MvcEshop.Models.EmailList emailRecord in DFEntities.EmailLists)
        //    {

        //        System.Web.HttpContext.Current.Session["IsThisActionUsedAsNonActionMethodForUserCreation"] = true;

        //        MvcEshop.ViewModels.User.CreateViewModel oCreateUserViewModel =
        //            new MvcEshop.ViewModels.User.CreateViewModel();

        //        oCreateUserViewModel.Email = emailRecord.Email;

        //        MVC.User.Create(oCreateUserViewModel);

        //    }

        //    //get the list of errors filled by action "Create" in "UserController".
        //    List<string> lstErrors =
        //        System.Web.HttpContext.Current.Session["ErrorLogForBeingUsedAsNonActionMethod"] as List<string>;

        //    MvcEshop.ViewModels.User.MessageContentViewModel oMessageContentViewModel =
        //        new ViewModels.User.MessageContentViewModel();

        //    //if all users were created successfully.
        //    if (lstErrors.Count == 0)
        //    {
        //        oMessageContentViewModel.Message =
        //            string.Format(Resources.Messages.SthDoneSuccessfully, Resources.DisplayNames.GroupEnrollment);

        //        oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.success;

        //        goto end;
        //    }

        //    //if some users could not be created successfully.
        //    if (lstErrors.Count != 0)
        //    {

        //        oMessageContentViewModel.Message =
        //            string.Format("<div>{0}</div>", Resources.Messages.ErrorInCreationOfSomeUsers);

        //        //creates error-list in html.
        //        foreach (string error in lstErrors)
        //        {
        //            oMessageContentViewModel.Message =
        //                string.Concat(oMessageContentViewModel.Message, string.Format("<div>{0}</div>"), error);
        //        }

        //        oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.failure;

        //    }


        //end:

        //    return (PartialView("_Partial_MessageBox", oMessageContentViewModel));
        //    //return View();
        //}

        // GET: TestRoles
        //[Authorize]
        public virtual ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Save(string editor)
        {
            string value = HttpUtility.HtmlDecode(editor);

            return View();
        }

        [Authorize(Roles = "Programmer")]
        //[Authorize(Users = "Programmer")]
        //[AllowAnonymous]
        public virtual ActionResult Index1()
        {
            return View();
        }

        public void index2()
        {
            Infrastructure.MyRoleProvider oMyRoleProvider =
                new Infrastructure.MyRoleProvider();

            oMyRoleProvider.AddUserToRole("mohammadparsi@rocketmail.com", "NotCompletedProfile");
            oMyRoleProvider.GetRolesForUser("mohammadparsi@rocketmail.com");
        }

        [HttpPost]
        public virtual ActionResult UploadFile()
        {
            string path = @"C:\Assignments\";

            HttpPostedFileBase photo = Request.Files["photo"];

            if (photo != null)
                photo.SaveAs(path + photo.FileName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Upload()
        {
            return View();
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(MvcEshop.ViewModels.Assignment.UploadFileViewModel uploadFileViewModel,
            HttpPostedFileBase PaperFile)
        {
            if (ModelState.IsValid)
            {
                string FileName = "no-photo.jpg";
                if (PaperFile != null)
                {
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(PaperFile.FileName);
                    PaperFile.SaveAs(@"C:\Assignments\" + FileName);
                }
                //products.ProductImage = ImageName;
                //db.Products.Add(products);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(uploadFileViewModel);
        }

        public virtual ActionResult TestOpms()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Payment()
        {
            MvcEshop.ViewModels.Assignment.PaymentConfirmationViewModel oPaymentConfirmationViewModel =
                new MvcEshop.ViewModels.Assignment.PaymentConfirmationViewModel();


            oPaymentConfirmationViewModel.Email = User.Identity.Name;

            oPaymentConfirmationViewModel.Amount = "1300";


            oPaymentConfirmationViewModel.SysId = MVC.Assignment.Setting.SysId;

            oPaymentConfirmationViewModel.Username = "123456";


            oPaymentConfirmationViewModel.FactorNum =
                System.Guid.NewGuid().ToString().Replace("-", "");
            //
            string timestamp = string.Empty;

            oPaymentConfirmationViewModel.Fp =
                MVC.Assignment.CreateFp(oPaymentConfirmationViewModel.Email, oPaymentConfirmationViewModel.Amount,
                oPaymentConfirmationViewModel.SysId, oPaymentConfirmationViewModel.Username,
                oPaymentConfirmationViewModel.FactorNum, out timestamp);

            oPaymentConfirmationViewModel.TimeStamp = timestamp;

            return View(oPaymentConfirmationViewModel);


        }

        [HttpGet]
        public virtual ActionResult PaymentResult()
        {
            MvcEshop.ViewModels.Assignment.PaymentResultViewModel oPaymentConfirmationViewModel =
                new MvcEshop.ViewModels.Assignment.PaymentResultViewModel();


            oPaymentConfirmationViewModel.Email = User.Identity.Name;

            oPaymentConfirmationViewModel.Amount = "1300";


            oPaymentConfirmationViewModel.SysId = MVC.Assignment.Setting.SysId;

            oPaymentConfirmationViewModel.Username = "123456";


            oPaymentConfirmationViewModel.FactorNum =
                System.Guid.NewGuid().ToString().Replace("-", "");
            //
            string timestamp = string.Empty;

            oPaymentConfirmationViewModel.Fp =
                MVC.Assignment.CreateFp(oPaymentConfirmationViewModel.Email, oPaymentConfirmationViewModel.Amount,
                oPaymentConfirmationViewModel.SysId, oPaymentConfirmationViewModel.Username,
                oPaymentConfirmationViewModel.FactorNum, out timestamp);

            oPaymentConfirmationViewModel.TimeStamp = timestamp;

            oPaymentConfirmationViewModel.FinalStatus = "1";
            oPaymentConfirmationViewModel.BillId = "121212";
            oPaymentConfirmationViewModel.SourceBillId = "113333";

            return View(oPaymentConfirmationViewModel);


        }


        public virtual ActionResult TestService()
        {
            MvcEshop.DocumentFinder.RequestIOServiceClient oRequestIOServiceClient =
                           new MvcEshop.DocumentFinder.RequestIOServiceClient();

            //MvcEshop.DocumentFinder.OriginalityReport oFinalReport =
            //    oRequestIOServiceClient.GetFinalReportByRequestID(oAssignmentFile.RequestId.Value);

            MvcEshop.DocumentFinder.Results oResults =
                oRequestIOServiceClient.GetResultsByRequestID(8999);

            int strAddress = Convert.ToInt32
                (oResults.ResultList.FirstOrDefault().ItemNumber);
            //byte intSimilarity =(byte)oResults.ResultTable.Rows[0][2];
            //string address = oResults.ResultTable.Rows[0][6].ToString();

            return View();

        }

        public virtual System.Web.Mvc.ActionResult GenerateUnexpectedError()
        {
            throw (new System.Exception("Some Unexpected Error..."));
        }

        [HttpGet]
        public virtual ActionResult LearningAjax(MvcEshop.ViewModels.TestRole.AjaxViewModel viewModel)
        {
            System.Threading.Thread.Sleep(5000);
            return View();
        }

        [HttpGet]
        public virtual ActionResult LearningPureAjaxWithJson()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult JsonTestForKendoGrid()
        {
            return View();
        }


        //[HttpPost]
        //public JsonResult GetJson03(Person person)
        //{
        //    string strMessage =
        //        string.Format("Full Name: {0} {1}",
        //        person.FirstName, person.LastName).ToString();

        //    var varJsonResult =
        //        Json(new { MyMessage = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet);

        //    return (varJsonResult);

        //}

        [HttpPost]
        public virtual JsonResult GetJson04(string Id)
        {
            //string strMessage =
            //    string.Format("Full Name: {0} {1}",
            //    person.Id, person.LastName).ToString();

            var varJsonResult =
                Json(new { MyMessage = Id }, System.Web.Mvc.JsonRequestBehavior.AllowGet);

            return (varJsonResult);

        }
        public class Person
        {
            public string Id { get; set; }
            public string LastName { get; set; }
        }
        [HttpPost]
        public virtual PartialViewResult GetPartialView(MvcEshop.ViewModels.TestRole.AjaxViewModel viewModel)
        {
            System.Threading.Thread.Sleep(5000);
            string strFullName = string.Format("{0} {1}", viewModel.data, viewModel.data2);
            return (PartialView("_Partial_PartialView", strFullName));
        }

        //[HttpPost]
        //public virtual ActionResult Test1(ViewModel viewModel)
        //{
        //    return View(viewModel);
        //}

        [HttpGet]
        public virtual ActionResult Test1()
        {
            return View();
        }

        public virtual ActionResult TestSms()
        {
            //Kavenegar.SDK.Test.Program.SendSMS(assignmentFile.UploaderUser.CellPhoneNumber, strMainMessage);
            //Infrastructure.SmsPanel.SendSMS("09102205526", "به همانندجو خوش آمدید.");
            Kavenegar.SDK.Test.Program.SendSMS("09102205526", "Welcome to document finder.", "2F524F50565A507664637342482B594F6973685A52513D3D");
            //// Create the thread object. This does not start the thread.
            //Infrastructure.Worker workerObject = new Infrastructure.Worker();
            //System.Threading.Thread workerThread = new System.Threading.Thread(workerObject.DoWork);

            //// Start the worker thread.
            //workerThread.Start();
            //Console.WriteLine("main thread: Starting worker thread...");

           

            //// Loop until worker thread activates. 
            //while (!workerThread.IsAlive) ;

            //// Put the main thread to sleep for 1 millisecond to 
            //// allow the worker thread to do some work:
            //Thread.Sleep(1);

            //// Request that the worker thread stop itself:
            //workerObject.RequestStop();

            //// Use the Join method to block the current thread  
            //// until the object's thread terminates.
            //workerThread.Join();
            //Console.WriteLine("main thread: Worker thread has terminated.");
            
            //CSharp_SOAP.Program.Main();
            return View();
        }

        public virtual ActionResult DownloadPDF()
        {
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[1];
            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("DocFindingReportCode", "Test", false);
            //params[1] = new ReportParameter("Quantity ", p.Quantity , false);

            string path = Path.Combine(Server.MapPath("~/Infrastructure/Reports"), "DocFindingReport.rdlc");
            Microsoft.Reporting.WebForms.LocalReport lr = new Microsoft.Reporting.WebForms.LocalReport();
            lr.ReportPath = path;
            lr.SetParameters(parameters);

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

        public virtual ActionResult TestSendAttachment()
        {
            string strFilePath = 
                Infrastructure.File.GetFilePath
                (Setting.AssignmentFilesPath, "1e3f7c89-4146-4970-ac50-5a575eb709fc.doc");

            Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    "mohammadparsi@rocketmail.com", "به همانندجو خوش آمدید", "This is test", strFilePath);

            return View();
        }

        //[HttpGet]
        //public virtual ActionResult Save()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public virtual ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        //{
        //    // The Name of the Upload component is "files"
        //    if (files != null)
        //    {
        //        foreach (var file in files)
        //        {
        //            // Some browsers send file names with full path.
        //            // We are only interested in the file name.
        //            var fileName = Path.GetFileName(file.FileName);
        //            var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

        //            // The files are not actually saved in this demo
        //            // file.SaveAs(physicalPath);
        //        }
        //    }

        //    // Return an empty string to signify success
        //    return Content("");
        //}

        public virtual ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

       
        //public ActionResult RemoveAndPersist(string[] fileNames)
        //{
        //    // The parameter of the Remove action must be called "fileNames"
        //    if (fileNames != null)
        //    {
        //        foreach (var fullName in fileNames)
        //        {
        //            var fileName = Path.GetFileName(fullName);
        //            var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

        //            SessionUploadInitialFilesRepository.Remove(fileName);

        //            if (System.IO.File.Exists(physicalPath))
        //            {
        //                // The files are not actually removed in this demo
        //                // System.IO.File.Delete(physicalPath);
        //            }
        //        }
        //    }

        //    // Return an empty string to signify success
        //    return Content("");
        //}
        //    DocumentFinder.RequestIOServiceClient oRequestIOServiceClient =
        //        new DocumentFinder.RequestIOServiceClient();

        //    DocumentFinder.RequestMessage oRequestMessage =
        //    new DocumentFinder.RequestMessage();

        //    oRequestMessage.CustomerMail = "mohammadparsi@rocketmail.com";
        //    oRequestMessage.Title = "submission title";
        //    oRequestMessage.TextBody = "text body";

        //    int intResult = oRequestIOServiceClient.PutMessage(oRequestMessage);

        //    //if intresult > 0 then means successfully saved and this is the requestid and must be saved in my system.
        //    // if intresult=0 then means "something is wrong".
        //    // if intresult<0 then it has a different meaning.

        //    oRequestIOServiceClient.GetFinalReportByRequestID(intResult);

        //    //DocumentFinder.OriginalityReport

        //    //oRequestIOServiceClient.GetResultsByRequestID();


        //}
    }
}