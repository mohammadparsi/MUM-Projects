using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;
using MvcEshop.Models;

namespace MvcEshop.Controllers
{

    public partial class AssignmentController : Infrastructure.BaseControllerWithDatabaseContext
    {


        [Authorize(Roles = "Instructor,Student")]
        public virtual ActionResult Index(Guid? id)
        {
            TempData["ClassId"] = id.Value;

            ViewBag.SuccessMessage = null;
            ViewBag.SubmisionFailureMessage = null;

            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                TempData["SuccessMessage"] = null;
            }

            if (TempData["SubmisionFailureMessage"] != null)
            {
                ViewBag.SubmisionFailureMessage = TempData["SubmisionFailureMessage"].ToString();
                TempData["SubmisionFailureMessage"] = null;
            }



            MvcEshop.Models.Class oClass = DFEntities.Classes
                .Where(current => current.ClassId == id.Value
                && current.IsDeleted == false)
                .FirstOrDefault()
                ;

            var varAssignments = oClass.Assignments
                .Where(current => current.IsDeleted == false)
                .OrderByDescending(current => current.StartDate)
                .ToList()
                ;


            return View(varAssignments);
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public virtual ActionResult Create()
        {
            ViewBag.ClassId =
                       Guid.Parse(TempData["ClassId"].ToString());

            TempData["ClassId"] = ViewBag.ClassId;

            return View();
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create
            (MvcEshop.ViewModels.Assignment.CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {

                MvcEshop.Models.Assignment oAssignment =
                    new MvcEshop.Models.Assignment();

                oAssignment.Title = createViewModel.Title;
                oAssignment.PointValue = createViewModel.PointValue;
                oAssignment.StartDate = Infrastructure.Convert.ToMiladi(createViewModel.StartDate);
                oAssignment.DueDate = Infrastructure.Convert.ToMiladi(createViewModel.DueDate);
                oAssignment.PostDate = Infrastructure.Convert.ToMiladi(createViewModel.PostDate);

                Guid guidClassId =
                       Guid.Parse(TempData["ClassId"].ToString());

                TempData["ClassId"] = guidClassId;

                oAssignment.ClassId = guidClassId;

                DFEntities.Assignments.Add(oAssignment);
                DFEntities.SaveChanges();

                return RedirectToAction(MVC.Assignment.Index(guidClassId));
            }

            return View(createViewModel);
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public virtual ActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["AssignmentId"] = id.Value;

            MvcEshop.Models.Assignment oAssignment =
                DFEntities.Assignments
                 .Where(current => current.AssignmentId == id.Value
                     && current.IsDeleted == false)
                 .FirstOrDefault()
                 ;

            if (oAssignment == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Assignment.CreateViewModel oEditViewModel =
                new MvcEshop.ViewModels.Assignment.CreateViewModel();

            oEditViewModel.Title = oAssignment.Title;
            oEditViewModel.PointValue = oAssignment.PointValue;

            oEditViewModel.StartDate =
                Infrastructure.Convert.ToPersian(oAssignment.StartDate);

            oEditViewModel.DueDate =
                Infrastructure.Convert.ToPersian(oAssignment.DueDate); ;

            oEditViewModel.PostDate =
                Infrastructure.Convert.ToPersian(oAssignment.PostDate);

            ViewBag.ClassId =
                       Guid.Parse(TempData["ClassId"].ToString());

            TempData["ClassId"] = ViewBag.ClassId;

            return View(oEditViewModel);
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit
            (MvcEshop.ViewModels.Assignment.CreateViewModel editViewModel)
        {
            Guid guidAssignmentId = Guid.Parse(TempData["AssignmentId"].ToString());

            MvcEshop.Models.Assignment oAssignment =
                DFEntities.Assignments
                 .Where(current => current.AssignmentId == guidAssignmentId
                     && current.IsDeleted == false)
                 .FirstOrDefault()
                 ;

            if (oAssignment == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                oAssignment.Title = editViewModel.Title;
                oAssignment.PointValue = editViewModel.PointValue;

                oAssignment.StartDate =
                    Infrastructure.Convert.ToMiladi(editViewModel.StartDate);

                oAssignment.DueDate =
                    Infrastructure.Convert.ToMiladi(editViewModel.DueDate);

                oAssignment.PostDate =
                    Infrastructure.Convert.ToMiladi(editViewModel.PostDate);

                DFEntities.Entry(oAssignment).State = EntityState.Modified;
                DFEntities.SaveChanges();

                Guid guidClassId =
                      Guid.Parse(TempData["ClassId"].ToString());

                TempData["ClassId"] = guidClassId;

                return RedirectToAction(MVC.Assignment.Index(guidClassId));
            }


            return View(editViewModel);
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MvcEshop.Models.Assignment oAssignment =
                DFEntities.Assignments
                 .Where(current => current.AssignmentId == id.Value
                  && current.IsDeleted == false)
                 .FirstOrDefault()
                 ;

            if (oAssignment == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClassId =
                       Guid.Parse(TempData["ClassId"].ToString());

            TempData["ClassId"] = ViewBag.ClassId;

            return View(oAssignment);
        }


        // POST: Account/Delete/5
        [Authorize(Roles = "Instructor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            MvcEshop.Models.Assignment oAssignment =
                DFEntities.Assignments
                .Where(current => current.AssignmentId == id
                && current.IsDeleted == false)
                .FirstOrDefault()
                ;

            oAssignment.IsDeleted = true;
            DFEntities.Entry(oAssignment).State = EntityState.Modified;
            DFEntities.SaveChanges();

            Guid guidClassId =
                      Guid.Parse(TempData["ClassId"].ToString());

            TempData["ClassId"] = guidClassId;

            return RedirectToAction(MVC.Assignment.Index(guidClassId));
        }



        [Authorize()]
        [HttpGet]
        public virtual ActionResult UploadFile(Guid? id)
        {

            TempData["AssignmentId"] = id.Value;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MvcEshop.Models.Assignment oAssignment = DFEntities.Assignments
                .Where(current => current.AssignmentId == id.Value
                && current.IsDeleted == false)
                .FirstOrDefault()
                ;

            if (oAssignment.IsSubmissionAllowed == false)
            {
                TempData["SubmisionFailureMessage"] =
                    "مهلت بارگذاری فایل برای این تکلیف به پایان رسیده است.";

                System.Guid guidClassId =
                   Guid.Parse(TempData["ClassId"].ToString());

                TempData["ClassId"] = guidClassId;

                return RedirectToAction(MVC.Assignment.Index(guidClassId));
            }

            MvcEshop.ViewModels.Assignment.UploadFileViewModel oUploadFileViewModel =
                new MvcEshop.ViewModels.Assignment.UploadFileViewModel();

            oUploadFileViewModel.TextSendingApproach = @Resources.DisplayNames.Upload;

            ViewBag.IsThisAssignment = true;

            return View(oUploadFileViewModel);
        }


        [Authorize()]
        //UploadFile Action for free-users
        [HttpGet]
        public virtual ActionResult SendDocument()
        {
            //the following session will be used for save and remove in Uploading.
            System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] = new List<FileNameFileNamesInServer>();

            List<Account> lstAccounts = new List<Account>();

            //if the current role of user is "GroupUser" then shows the dropdownlist containing the accounts of creditcards
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
            {
                lstAccounts =
                               MVC.User.GetUserByUsername(User.Identity.Name).CreditCards
                               .Select(current => current.Account)
                               .ToList();
            }

            //if the current role of user is "AccountManager" then shows the dropdownlist containing the accounts it's
            //manager is the current user.
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                lstAccounts =
                               MVC.Account.GetExistingAccounts()
                               .Where(current => current.ManagerUserId == MVC.User.GetUserIdByEmail(User.Identity.Name))
                               .ToList();
            }

            ViewBag.AccountId =
                new SelectList(lstAccounts, "AccountId", "AccountTitle");

            MvcEshop.ViewModels.Assignment.UploadFileViewModel oUploadFileViewModel =
                new MvcEshop.ViewModels.Assignment.UploadFileViewModel();

            oUploadFileViewModel.TextSendingApproach = @Resources.DisplayNames.Upload;

            ViewBag.IsThisAssignment = false;
            return View("UploadFile", oUploadFileViewModel);
        }



        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult UploadFile
            (MvcEshop.ViewModels.Assignment.UploadFileViewModel uploadFileViewModel)
        {

            List<MvcEshop.Models.FileNameFileNamesInServer> lstFinalChosenFiles =
           System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] as List<MvcEshop.Models.FileNameFileNamesInServer>;

            string strUploadedFileName = string.Empty;
            int intContentLength = 0;



            if (string.IsNullOrEmpty(uploadFileViewModel.PaperTitle))
            {
                ModelState.AddModelError
                            ("PaperTitleRequired", string.Format(Resources.Messages.RequiredFieldValidator,
                            Resources.DisplayNames.PaperTitle));

                //goto end;

            }

            if (uploadFileViewModel.MustSearchInInternet == false && uploadFileViewModel.MustSearchInIrandoc == false)
            {
                ModelState.AddModelError
                       ("ChooseAtLeastOneItem", Resources.Messages.CheckBoxListValidator);

                //goto end;
            }

            //if the "upload" radio button has been chosen and on the other hand, no file is chosen.
            if (uploadFileViewModel.TextSendingApproach == Resources.DisplayNames.Upload)
            {

                if (lstFinalChosenFiles.Count == 0)
                {
                    //Error Message to say that "paper file" must be chosen.
                    ModelState.AddModelError
                            ("PaperFile", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                            Resources.DisplayNames.PaperFile));

                    //goto end;

                }

                //check whether the user has chosen only one file.
                else if (lstFinalChosenFiles.Count > 1)
                {
                    ModelState.AddModelError
                                ("PaperFile", Resources.Messages.PleaseChooseOneFile);

                }

                    //if the program enters the following block, it shows that only one file has been chosen and this 
                //is exactly what we want.
                else
                {
                    strUploadedFileName = lstFinalChosenFiles.FirstOrDefault().FileName;
                    intContentLength = lstFinalChosenFiles.FirstOrDefault().TextContentSize;

                    string strExtention = Path.GetExtension(strUploadedFileName);

                    if (strExtention != ".docx" && strExtention != ".doc" && strExtention != ".txt")
                    {
                        ModelState.AddModelError
                                ("PaperFile", string.Format("پسوند فایل مورد نظر بایستی {0}، {1} یا {2} باشد.", "docx", "doc", "txt"));

                        //goto end;
                    }
                    else if (intContentLength > DFEntities.Settings.FirstOrDefault().MaximumPaperFileSizeToUploadInMeg * 1000000)
                    {
                        ModelState.AddModelError
                                ("PaperFile", string.Format("حجم فایل مورد نظر می بایستی حداکثر {0} مگابایت باشد.",
                                DFEntities.Settings.FirstOrDefault().MaximumPaperFileSizeToUploadInMeg));

                        //goto end;
                    }


                }
            }

            //the final text which must be processed
            string strText = string.Empty;

            //if the "Paste into Textbox" radio button has been chosen.
            if (uploadFileViewModel.TextSendingApproach == Resources.DisplayNames.TextBoxPaste)
            {
                //if the paste textbox is empty.
                if (string.IsNullOrEmpty(uploadFileViewModel.Text))
                {
                    //Error Message to say that "Text For Document Finding" is required.
                    ModelState.AddModelError
                            ("Text", string.Format(Resources.Messages.RequiredFieldValidator,
                            Resources.DisplayNames.TextForDocumentFinding));

                    //goto end;

                }

                else if (Infrastructure.Text.CountNonSpaceChars(uploadFileViewModel.Text) > DFEntities
                    .Settings.FirstOrDefault().MaximumNumOfCharsInTextBox)
                {
                    ModelState.AddModelError
                            ("Text", string.Format(Resources.Messages.NumOfWordCharsMustBeLessThan, DFEntities
                    .Settings.FirstOrDefault().MaximumNumOfCharsInTextBox));

                    //goto end;
                }

                else
                {
                    //fill strText with the text pasted into the textbox.
                    strText = uploadFileViewModel.Text;

                    strUploadedFileName = "PasteTextBox" + ".txt";
                    intContentLength = System.Text.ASCIIEncoding.Unicode.GetByteCount(strText);
                }
            }


            //if the role of the user is "groupUser". 
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
            {
                //and has not chosen any accounts from the dropdownlist.
                if (uploadFileViewModel.AccountId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    ModelState.AddModelError
                                        ("AccountDDL", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                                        Resources.DisplayNames.AccountToBeUsed));

                    //goto end;

                }
                else
                {
                    MvcEshop.Models.CreditCard oCreditCard =
                    MVC.User.GetUserByUsername(User.Identity.Name)
                    .CreditCards.Where(current => current.AccountId == uploadFileViewModel.AccountId)
                    .ToList()
                    .FirstOrDefault();

                    //check credit card expiration for groupUser
                    if (oCreditCard.IsExpired)
                    {
                        ModelState.AddModelError
                                            ("", string.Format(Resources.Messages.SthHasFinishedForYou,
                                            Resources.DisplayNames.ExpirationDateForVolumeUse));

                        //goto end;

                    }
                    //check credit card's remaining volume for GroupUser
                    else if (oCreditCard.RemainingVolume < (double)intContentLength / 1000)
                    {
                        ModelState.AddModelError
                                            ("", string.Format(Resources.Messages.TextVolumeIsMoreThanRemainingVolume,
                                            Resources.DisplayNames.TextVolume, Resources.DisplayNames.RemainingVolume));

                        //goto end;

                    }
                    //check creditcard's account's expiration for groupUser
                    else if (oCreditCard.Account.CurrentContractReceipt.IsExpired)
                    {
                        ModelState.AddModelError
                                            ("", string.Format(Resources.Messages.ExpirationDateForVolumeUseHasFinished,
                                            Resources.DisplayNames.ExpirationDateForVolumeUse,
                                            Resources.DisplayNames.Account));

                        //goto end;

                    }
                    //check creditcard's account's remainingVolume for groupUser
                    else if (oCreditCard.Account.CurrentContractReceipt.RemainingVolume < (double)intContentLength / 1000)
                    {
                        ModelState.AddModelError
                                            ("", string.Format(Resources.Messages.TextVolumeIsMoreThanRemainingVolumeOfAccount,
                                            Resources.DisplayNames.TextVolume, Resources.DisplayNames.RemainingVolume,
                                            Resources.DisplayNames.Account));

                        //goto end;

                    }

                }

            }

            //if the role of the user is "AccountManager". 
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                //and has not chosen any accounts from the dropdownlist.
                if (uploadFileViewModel.AccountId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    ModelState.AddModelError
                                        ("AccountDDL", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                                        Resources.DisplayNames.AccountToBeUsed));

                    //goto end;

                }

                else
                {
                    MvcEshop.Models.Account oSelectedAccount = MVC.Account.GetExistingAccounts()
                            .Where(current => current.AccountId == uploadFileViewModel.AccountId)
                            .ToList()
                            .FirstOrDefault();

                    //check creditcard's account's expiration for AccountManager
                    if (oSelectedAccount.CurrentContractReceipt.IsExpired)
                    {
                        ModelState.AddModelError
                                            ("", string.Format(Resources.Messages.ExpirationDateForVolumeUseHasFinished,
                                            Resources.DisplayNames.ExpirationDateForVolumeUse,
                                            Resources.DisplayNames.Account));

                        //goto end;

                    }

                    //check creditcard's account's remainingVolume for AccountManager
                    else if (oSelectedAccount.CurrentContractReceipt.RemainingVolume < (double)intContentLength / 1000)
                    {
                        ModelState.AddModelError
                                            ("", string.Format(Resources.Messages.TextVolumeIsMoreThanRemainingVolumeOfAccount,
                                            Resources.DisplayNames.TextVolume, Resources.DisplayNames.RemainingVolume,
                                            Resources.DisplayNames.Account));

                        //goto end;

                    }

                }

            }


            if (ModelState.IsValid)
            {

                Guid? guidAssignmentId = null;

                //if the current role of user is "Student"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 5)
                {
                    guidAssignmentId = Guid.Parse(TempData["AssignmentId"].ToString());

                    TempData["AssignmentId"] = guidAssignmentId;
                }

                MvcEshop.Models.AssignmentFile oAssignmentFile =
                        new MvcEshop.Models.AssignmentFile();

                oAssignmentFile.SubmissionTitle = uploadFileViewModel.PaperTitle;
                oAssignmentFile.FileName = strUploadedFileName;
                oAssignmentFile.AssignmentId = guidAssignmentId;
                oAssignmentFile.UploaderUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);


                //if (PaperFile != null)
                //{
                //    PaperFile.SaveAs(string.Format
                //                (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                //                + strFileName);

                //}
                string strFileNameInServer = string.Empty;

                if (uploadFileViewModel.TextSendingApproach == Resources.DisplayNames.Upload)
                {
                    strFileNameInServer = lstFinalChosenFiles.FirstOrDefault().FileNameInServer;
                }
                //if the "Paste into Textbox" radio button has been chosen.
                else
                {
                    strFileNameInServer = Guid.NewGuid().ToString() +
                        Path.GetExtension(strUploadedFileName);

                    System.IO.File.WriteAllText(string.Format
                                (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                                + strFileNameInServer, strText);
                }

                oAssignmentFile.FileNameInServer = strFileNameInServer;
                oAssignmentFile.UploadDate = System.DateTime.Now;

                //determine resources to compare based on what user has been checked in the upload form.
                oAssignmentFile.SearchInIrandoc = uploadFileViewModel.MustSearchInIrandoc;
                oAssignmentFile.SearchInInternet = uploadFileViewModel.MustSearchInInternet;


                //if the current role of user is "FreeUser"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 8)
                {
                    //oAssignmentFile.NotPaidFor = true;
                    oAssignmentFile.IsDeleted = true;
                }

                DFEntities.AssignmentFiles.Add(oAssignmentFile);
                DFEntities.SaveChanges();



                //add the volume of doc-sending text to the UsedVolume field based on different roles.

                //if the role of the user is either "groupUser" or "account manager". 
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7 ||
                    System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
                {
                    //initialize the oUsedAccount with the account that account manager has selected from dropdownlist.
                    MvcEshop.Models.Account oUsedAccount = MVC.Account.GetExistingAccounts()
                        .Where(current => current.AccountId == uploadFileViewModel.AccountId)
                        .ToList()
                        .FirstOrDefault();

                    //if the role of user is "groupUser", then add the volume of the doc-sending text to the used-volume of credit-card
                    if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
                    {
                        MvcEshop.Models.CreditCard oCreditCard =
                        MVC.User.GetUserByUsername(User.Identity.Name)
                        .CreditCards.Where(current => current.AccountId == uploadFileViewModel.AccountId)
                        .ToList()
                        .FirstOrDefault();

                        oCreditCard.UsedVolume = oCreditCard.UsedVolume + (double)intContentLength / 1000;

                        MVC.User.DFEntities.Entry(oCreditCard).State = EntityState.Modified;
                        MVC.User.DFEntities.SaveChanges();

                        //set the oUsedAccount to the Account of credit-card used by groupUser(if the current role of user is groupUser).
                        oUsedAccount = oCreditCard.Account;
                    }

                    //add the volume of the doc-sending text to the used-volume of CurrentContractReceipt of used account.
                    oUsedAccount.CurrentContractReceipt.UsedVolume =
                        oUsedAccount.CurrentContractReceipt.UsedVolume + (double)intContentLength / 1000;


                    //check whether the Used-To-Allocated Volume Percentage has reached to the field "UsedToAllocatedVolumePercentage"
                    //if so, then send a notification email.

                    if ((oUsedAccount.CurrentContractReceipt.UsedToAllocatedRatio() * 100) >= oUsedAccount.Customer.UsedToAllocatedVolumePercentageToNotify)
                    {
                        System.Globalization.CultureInfo oCultureInfo =
              new System.Globalization.CultureInfo("fa-IR");

                        System.Threading.Thread.CurrentThread.CurrentUICulture = oCultureInfo;

                        string strMainMessage =
                            string.Format(Resources.Messages.UsedToAllocatedVolumePercentageHasFinishedForYourAccount,
                           oUsedAccount.Customer.UsedToAllocatedVolumePercentageToNotify, Resources.DisplayNames.AllocatedVolume, Resources.DisplayNames.Account,
                           oUsedAccount.AccountTitle);

                        string strFullMessage =
                           string.Format(Resources.Messages.EmailMessageDefaultGreetingForUser, strMainMessage);

                        string strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                            DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                            strFullMessage);

                        Infrastructure.Communication.SendEmail
                            (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                            Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                            oUsedAccount.Manager.Email, Resources.DisplayNames.Caution, strEmailBody, null);
                    }


                    //do the process of adding a new receipt for this doc-sending.
                    Receipt oReceipt = new Receipt();

                    //set ReceiptType to "Operational" to be used as report in the future.
                    oReceipt.ReceiptTypeId = 3;
                    oReceipt.UsedVolume = (double)intContentLength / 1000;
                    oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);
                    oReceipt.AssignmentFileId = oAssignmentFile.FileId;
                    oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);
                    oReceipt.AccountId = oUsedAccount.AccountId;

                    //add a receipt for this doc-sending process.
                    oUsedAccount.Receipts.Add(oReceipt);

                    MVC.User.DFEntities.Entry(oUsedAccount).State = EntityState.Modified;
                    MVC.User.DFEntities.SaveChanges();
                }

                TempData["AssignmentFileId"] = oAssignmentFile.FileId;

                TempData["UploadedFileId"] = oAssignmentFile.FileId;


                //if the current role of user is "Student"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 5)
                {

                    Guid guidClassId =
                          Guid.Parse(TempData["ClassId"].ToString());

                    TempData["ClassId"] = guidClassId;

                    MvcEshop.Models.Assignment oAssignment = DFEntities.Assignments
                        .Where(current => current.AssignmentId == guidAssignmentId)
                        .FirstOrDefault();

                    string strMessage = string.Format
                        (Resources.Messages.AssignmentFileUploadedSuccessfully,
                        Resources.DisplayNames.SubmissionTitle, oAssignmentFile.SubmissionTitle,
                        oAssignmentFile.FileName, oAssignment.Title,
                        Resources.DisplayNames.Instructor);

                    TempData["SuccessMessage"] = strMessage;


                    return RedirectToAction(MVC.Assignment.Index(guidClassId));

                }

                

                //if the current role of user is "GroupUser" or "AccountManager"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7 || System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
                {

                    string strMessage = string.Format(Resources.Messages.TextUploadedSuccessfully,
                        @"<a href='/Assignment/PreviousRequests/" + MVC.User.GetUserByUsername(User.Identity.Name).UserId.ToString() +
                        "' >" + Resources.DisplayNames.ShowResult + "</a>");

                    return RedirectToAction(MVC.User.Message(strMessage));
                }

                //if the current role of user is "FreeUser"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 8)
                {
                    return RedirectToAction
                        (MVC.Assignment.PaymentConfirmation(uploadFileViewModel.MustSearchInIrandoc,
                        uploadFileViewModel.MustSearchInInternet, oAssignmentFile.FileId.ToString()));
                }

            }


        end:

            List<Account> lstAccounts = new List<Account>();

            //if the current role of user is "GroupUser" then shows the dropdownlist containing the accounts of creditcards
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
            {
                lstAccounts =
                               MVC.User.GetUserByUsername(User.Identity.Name).CreditCards
                               .Select(current => current.Account)
                               .ToList();
            }

            //if the current role of user is "AccountManager" then shows the dropdownlist containing the accounts it's
            //manager is the current user.
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                lstAccounts =
                               MVC.Account.GetExistingAccounts()
                               .Where(current => current.ManagerUserId == MVC.User.GetUserIdByEmail(User.Identity.Name))
                               .ToList();
            }

            ViewBag.AccountId =
                new SelectList(lstAccounts, "AccountId", "AccountTitle");


            ViewBag.IsThisAssignment = false;

            return View(uploadFileViewModel);
        }



        [Authorize(Roles = "AccountManager, GroupUser")]
        [HttpGet]
        public virtual ActionResult FolderUpload()
        {
            System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] = new List<FileNameFileNamesInServer>();

            List<Account> lstAccounts = new List<Account>();

            //if the current role of user is "GroupUser" then shows the dropdownlist containing the accounts of creditcards
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
            {
                lstAccounts =
                               MVC.User.GetUserByUsername(User.Identity.Name).CreditCards
                               .Select(current => current.Account)
                               .ToList();
            }

            //if the current role of user is "AccountManager" then shows the dropdownlist containing the accounts it's
            //manager is the current user.
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                lstAccounts =
                               MVC.Account.GetExistingAccounts()
                               .Where(current => current.ManagerUserId == MVC.User.GetUserIdByEmail(User.Identity.Name))
                               .ToList();
            }

            ViewBag.AccountId =
                new SelectList(lstAccounts, "AccountId", "AccountTitle");

            MvcEshop.ViewModels.Assignment.UploadFileViewModel oUploadFileViewModel =
                new MvcEshop.ViewModels.Assignment.UploadFileViewModel();

            return View();

        }

        [HttpPost]
        public virtual ActionResult FolderUpload
            (MvcEshop.ViewModels.Assignment.FolderUploadViewModel folderUploadViewModel)
        {

            List<MvcEshop.Models.FileNameFileNamesInServer> lstFinalChosenFiles =
             System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] as List<MvcEshop.Models.FileNameFileNamesInServer>;

            if (folderUploadViewModel.MustSearchInInternet == false && folderUploadViewModel.MustSearchInIrandoc == false)
            {
                ModelState.AddModelError
                       ("ChooseAtLeastOneItem", Resources.Messages.CheckBoxListValidator);

            }

            if (lstFinalChosenFiles.Count == 0)
            {
                //Error Message to say that "files" must be chosen.
                ModelState.AddModelError
                        ("Files", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                        Resources.DisplayNames.File + Resources.DisplayNames.Plural));

            }


            //Check Remaining volume and expiration.

            int intTotalVolume = 0;

            foreach (var fileItem in lstFinalChosenFiles)
            {
                intTotalVolume = intTotalVolume + fileItem.TextContentSize;
            }

            //if the role of the user is "groupUser". 
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
            {


                MvcEshop.Models.CreditCard oCreditCard =
                MVC.User.GetUserByUsername(User.Identity.Name)
                .CreditCards.Where(current => current.AccountId == folderUploadViewModel.AccountId)
                .ToList()
                .FirstOrDefault();

                //check credit card expiration for groupUser
                if (oCreditCard.IsExpired)
                {
                    ModelState.AddModelError
                                        ("", string.Format(Resources.Messages.SthHasFinishedForYou,
                                        Resources.DisplayNames.ExpirationDateForVolumeUse));

                    //goto end;

                }
                //check credit card's remaining volume for GroupUser
                else if (oCreditCard.RemainingVolume < (double)intTotalVolume / 1000)
                {
                    ModelState.AddModelError
                                        ("", string.Format(Resources.Messages.TextVolumeIsMoreThanRemainingVolume,
                                        Resources.DisplayNames.TextVolume, Resources.DisplayNames.RemainingVolume));

                    //goto end;

                }
                //check creditcard's account's expiration for groupUser
                else if (oCreditCard.Account.CurrentContractReceipt.IsExpired)
                {
                    ModelState.AddModelError
                                        ("", string.Format(Resources.Messages.ExpirationDateForVolumeUseHasFinished,
                                        Resources.DisplayNames.ExpirationDateForVolumeUse,
                                        Resources.DisplayNames.Account));

                    //goto end;

                }
                //check creditcard's account's remainingVolume for groupUser
                else if (oCreditCard.Account.CurrentContractReceipt.RemainingVolume < (double)intTotalVolume / 1000)
                {
                    ModelState.AddModelError
                                        ("", string.Format(Resources.Messages.TextVolumeIsMoreThanRemainingVolumeOfAccount,
                                        Resources.DisplayNames.TextVolume, Resources.DisplayNames.RemainingVolume,
                                        Resources.DisplayNames.Account));

                    //goto end;

                }



            }


            if (ModelState.IsValid)
            {
                foreach (var item in lstFinalChosenFiles)
                {
                    MvcEshop.Models.AssignmentFile oAssignmentFile =
                            new MvcEshop.Models.AssignmentFile();

                    oAssignmentFile.SubmissionTitle = "عنوان وارد نشده است.";
                    oAssignmentFile.FileName = item.FileName;
                    oAssignmentFile.UploaderUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);
                    oAssignmentFile.FileNameInServer = item.FileNameInServer;
                    oAssignmentFile.UploadDate = System.DateTime.Now;

                    //determine resources to compare based on what user has been checked in the upload form.
                    oAssignmentFile.SearchInIrandoc = folderUploadViewModel.MustSearchInIrandoc;
                    oAssignmentFile.SearchInInternet = folderUploadViewModel.MustSearchInInternet;

                    DFEntities.AssignmentFiles.Add(oAssignmentFile);
                    DFEntities.SaveChanges();


                    //add the volume of doc-sending text to the UsedVolume field based on different roles.

                    //if the role of the user is either "groupUser" or "account manager". 
                    if (System.Convert.ToInt32
                    (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7 ||
                        System.Convert.ToInt32
                    (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
                    {
                        //initialize the oUsedAccount with the account that account manager has selected from dropdownlist.
                        MvcEshop.Models.Account oUsedAccount = MVC.Account.GetExistingAccounts()
                            .Where(current => current.AccountId == folderUploadViewModel.AccountId)
                            .ToList()
                            .FirstOrDefault();

                        //if the role of user is "groupUser", then add the volume of the doc-sending text to the used-volume of credit-card
                        if (System.Convert.ToInt32
                    (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
                        {
                            MvcEshop.Models.CreditCard oCreditCard =
                            MVC.User.GetUserByUsername(User.Identity.Name)
                            .CreditCards.Where(current => current.AccountId == folderUploadViewModel.AccountId)
                            .ToList()
                            .FirstOrDefault();

                            oCreditCard.UsedVolume = oCreditCard.UsedVolume + (double)item.TextContentSize / 1000;

                            MVC.User.DFEntities.Entry(oCreditCard).State = EntityState.Modified;
                            MVC.User.DFEntities.SaveChanges();

                            //set the oUsedAccount to the Account of credit-card used by groupUser(if the current role of user is groupUser).
                            oUsedAccount = oCreditCard.Account;
                        }

                        //add the volume of the doc-sending text to the used-volume of CurrentContractReceipt of used account.
                        oUsedAccount.CurrentContractReceipt.UsedVolume =
                            oUsedAccount.CurrentContractReceipt.UsedVolume + (double)item.TextContentSize / 1000;

                        //do the process of adding a new receipt for this doc-sending.
                        Receipt oReceipt = new Receipt();

                        //set ReceiptType to "Operational" to be used as report in the future.
                        oReceipt.ReceiptTypeId = 3;
                        oReceipt.UsedVolume = (double)item.TextContentSize / 1000;
                        oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);
                        oReceipt.AssignmentFileId = oAssignmentFile.FileId;
                        oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);

                        //add a receipt for this doc-sending process.
                        oUsedAccount.Receipts.Add(oReceipt);

                        MVC.User.DFEntities.Entry(oUsedAccount).State = EntityState.Modified;
                        MVC.User.DFEntities.SaveChanges();
                    }

                }

                //if the current role of user is "GroupUser" or "AccountManager"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7 || System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
                {

                    string strMessage = string.Format(Resources.Messages.TextUploadedSuccessfully,
                        @"<a href='/Assignment/PreviousRequests/" + MVC.User.GetUserByUsername(User.Identity.Name).UserId.ToString() +
                        "' >" + Resources.DisplayNames.ShowResult + "</a>");

                    return RedirectToAction(MVC.User.Message(strMessage));
                }

            }

            List<Account> lstAccounts = new List<Account>();

            //if the current role of user is "GroupUser" then shows the dropdownlist containing the accounts of creditcards
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7)
            {
                lstAccounts =
                               MVC.User.GetUserByUsername(User.Identity.Name).CreditCards
                               .Select(current => current.Account)
                               .ToList();
            }

            //if the current role of user is "AccountManager" then shows the dropdownlist containing the accounts it's
            //manager is the current user.
            if (System.Convert.ToInt32
            (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                lstAccounts =
                               MVC.Account.GetExistingAccounts()
                               .Where(current => current.ManagerUserId == MVC.User.GetUserIdByEmail(User.Identity.Name))
                               .ToList();
            }

            ViewBag.AccountId =
                new SelectList(lstAccounts, "AccountId", "AccountTitle");

            return View(folderUploadViewModel);
        }


        //public string SysId { get; set; }
        //public string Username { get; set; }
        //public string Email { get; set; }
        //public string Amount { get; set; }
        //public string Fp { get; set; }
        //public string TimeStamp { get; set; }
        //public string SourceBillId { get; set; }
        //public string BillId { get; set; }
        //public string FactorNum { get; set; }
        //public string FinalStatus { get; set; }



        [HttpPost]
        public virtual ActionResult GetPaymentResult
            //(MvcEshop.ViewModels.Assignment.PaymentResultViewModel paymentResultViewModel)
            (string SysId, string Username, string Email, string Amount, string Fp, string TimeStamp,
            string SourceBillId, string BillId, string FactorNum, string FinalStatus)
        {


            //if (FinalStatus == "0")
            //{
            //    //MvcEshop.Models.Receipt oReceipt =
            //    //new MvcEshop.Models.Receipt();

            //    //Online Payment for one person(free User)
            //    oReceipt.PaymentTypeId = 1;


            //    //oReceipt.AssignmentFileId = FactorNum;

            //    oReceipt.BillId = BillId;
            //    oReceipt.SourceBillId = SourceBillId;

            //    oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);

            //    oReceipt.CurrentIrandocTariff = MVC.User.DFEntities.Resources
            //            .Where(current => current.Id == 1)
            //            .FirstOrDefault()
            //            .Price.Value;

            //    oReceipt.CurrentInternetTariff = MVC.User.DFEntities.Resources
            //        .Where(current => current.Id == 2)
            //        .FirstOrDefault()
            //        .Price.Value;

            //    oReceipt.PaidAmountInRial = System.Convert.ToInt32(Amount);

            //    //oReceipt.FinalStatus = false;

            //    oReceipt.FactorNum = FactorNum;



            //    oReceipt.Fp = Fp;
            //    oReceipt.TimeStamp = TimeStamp;

            //    DFEntities.Receipts.Add(oReceipt);
            //    DFEntities.SaveChanges();

            //    //we do this because in this way, in the future, during resendingUnsentRequests, this file will be
            //    //checked to see whether the user has paid for or not. When we set it to false, this file will be 
            //    //sent to DFService without any problem.
            //    //oReceipt.AssignmentFile.NotPaidFor = false;

            //    DFEntities.Entry(oReceipt).State = EntityState.Modified;
            //    DFEntities.SaveChanges();

            //    return RedirectToAction(MVC.User.Message("با شکست روبه رو شد."));
            //}

            //if (FinalStatus == "1")
            //{
            //    //MvcEshop.Models.Receipt oReceipt =
            //    //new MvcEshop.Models.Receipt();

            //    //Online Payment for one person(free User)
            //    oReceipt.PaymentTypeId = 1;


            //    //oReceipt.AssignmentFileId = FactorNum;

            //    oReceipt.BillId = BillId;
            //    oReceipt.SourceBillId = SourceBillId;

            //    oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);

            //    oReceipt.CurrentIrandocTariff = MVC.User.DFEntities.Resources
            //            .Where(current => current.Id == 1)
            //            .FirstOrDefault()
            //            .Price.Value;

            //    oReceipt.CurrentInternetTariff = MVC.User.DFEntities.Resources
            //        .Where(current => current.Id == 2)
            //        .FirstOrDefault()
            //        .Price.Value;

            //    oReceipt.PaidAmountInRial = System.Convert.ToInt32(Amount);

            //    //oReceipt.FinalStatus = false;

            //    oReceipt.FactorNum = FactorNum;



            //    oReceipt.Fp = Fp;
            //    oReceipt.TimeStamp = TimeStamp;

            //    DFEntities.Receipts.Add(oReceipt);
            //    DFEntities.SaveChanges();

            //    //we do this because in this way, in the future, during resendingUnsentRequests, this file will be
            //    //checked to see whether the user has paid for or not. When we set it to false, this file will be 
            //    //sent to DFService without any problem.
            //    //oReceipt.AssignmentFile.NotPaidFor = false;

            //    DFEntities.Entry(oReceipt).State = EntityState.Modified;
            //    DFEntities.SaveChanges();
            //    return RedirectToAction(MVC.User.Message("پرداخت موفق بود."));
            //}

            //return RedirectToAction(MVC.User.Message("هیچ کدام"));
            string strMessage = string.Empty;
            //System.Web.HttpContext.Current.Session["IsPaymentSuccessful"] = true;

            //SourceBillId is actually the fileId because we had set FactorNum to fileId in Action "PaymentConfirmation" 
            //and OPMS returned value of 
            // FactorNum(fileId) as sourceBillId .
            Guid guidUploadedFileId =
                Guid.Parse(SourceBillId.ToString().ToUpper());

            AssignmentFile oFile = DFEntities.AssignmentFiles
                .Where(current => current.FileId == guidUploadedFileId)
                .ToList()
                .FirstOrDefault();
            //GetAssignmentFileById(guidUploadedFileId);



            //TempData["UploadedFileId"] = guidUploadedFileId;

            if (System.Convert.ToInt32(FinalStatus) == 0)
            {
                ////create a new fileId to be used as a new factorNum in the future for repaying.
                //oFile.FileId = System.Guid.NewGuid();

                //DFEntities.Entry(oFile).State = EntityState.Modified;
                //DFEntities.SaveChanges();

                strMessage = Resources.Messages.FailureInOnlinePayment;
                //string.Format(
                //"<div> <a class='btn btn-primary btn-xs' href='/Assignment/PaymentConfirmation?mustSearchInIrandoc={0}&mustSearchInInternet={1}&fileId={2}'>پرداخت</a></div>",
                //oFile.SearchInIrandoc.ToString(), oFile.SearchInInternet.ToString(), oFile.FileId
                //);

                ViewBag.IsPaymentSuccessful = false;
                //System.Web.HttpContext.Current.Session["IsPaymentSuccessful"] = false;
                return RedirectToAction(MVC.User.Message(strMessage));


                //return RedirectToAction(MVC.User.Message("ناموفق بود."));
            }


            //oFile.NotPaidFor = false;
            oFile.IsDeleted = false;
            //oReceipt.AssignmentFile = oFile;

            DFEntities.Entry(oFile).State = EntityState.Modified;
            DFEntities.SaveChanges();
            //if (paymentResultViewModel.FinalStatus==false)
            //{
            //    strMessage = "پرداخت با موفقیت انجام نشد. از فهرست فایل های ارسالی، فایل مورد نظر را یافته و سپس مجددا برای آن فرآیند پرداخت را انجام دهید."; 

            //    return RedirectToAction(MVC.User.Message(strMessage));
            //}
            MvcEshop.Models.Receipt oReceipt =
               new MvcEshop.Models.Receipt();


            //Online Payment for one person(free User)
            oReceipt.PaymentTypeId = 1;


            oReceipt.AssignmentFileId = guidUploadedFileId;

            oReceipt.BillId = BillId;
            oReceipt.SourceBillId = SourceBillId;

            oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);

            oReceipt.CurrentIrandocTariff = MVC.User.DFEntities.Resources
                    .Where(current => current.Id == 1)
                    .FirstOrDefault()
                    .Price.Value;

            oReceipt.CurrentInternetTariff = MVC.User.DFEntities.Resources
                .Where(current => current.Id == 2)
                .FirstOrDefault()
                .Price.Value;

            oReceipt.PaidAmountInRial = System.Convert.ToInt32(Amount);

            oReceipt.FinalStatus = true;

            oReceipt.FactorNum = FactorNum;



            oReceipt.Fp = Fp;
            oReceipt.TimeStamp = TimeStamp;

            DFEntities.Receipts.Add(oReceipt);
            DFEntities.SaveChanges();

            //we do this because in this way, in the future, during resendingUnsentRequests, this file will be
            //checked to see whether the user has paid for or not. When we set it to false, this file will be 
            //sent to DFService without any problem.

            //oReceipt.AssignmentFile.NotPaidFor = false;



            strMessage =
                string.Format(Resources.Messages.SuccessInOnlinePayment,
                Resources.DisplayNames.ShowRequests);

            return RedirectToAction(MVC.User.Message(strMessage));


        }


        [HttpGet]
        public virtual ActionResult PaymentConfirmation(bool? mustSearchInIrandoc, bool? mustSearchInInternet, string fileId)
        {
            MvcEshop.ViewModels.Assignment.PaymentConfirmationViewModel oPaymentConfirmationViewModel =
                new MvcEshop.ViewModels.Assignment.PaymentConfirmationViewModel();

            //Guid guidUploadedFileId =
            //    Guid.Parse(TempData["UploadedFileId"].ToString());

            //TempData["UploadedFileId"] = guidUploadedFileId;

            oPaymentConfirmationViewModel.Email = User.Identity.Name;

            oPaymentConfirmationViewModel.Amount =
                PaymentAmountBasedOnAssignmentFile(Guid.Parse(fileId), mustSearchInIrandoc.Value, mustSearchInInternet.Value).ToString();

            oPaymentConfirmationViewModel.SysId = Setting.SysId;

            oPaymentConfirmationViewModel.Username =
                MVC.User.GetUserIdByEmail(User.Identity.Name).ToString();

            oPaymentConfirmationViewModel.FactorNum = fileId;
            //System.Guid.NewGuid().ToString().Replace("-", "");
            //
            string timestamp = string.Empty;

            oPaymentConfirmationViewModel.Fp =
                CreateFp(oPaymentConfirmationViewModel.Email, oPaymentConfirmationViewModel.Amount,
                oPaymentConfirmationViewModel.SysId, oPaymentConfirmationViewModel.Username,
                oPaymentConfirmationViewModel.FactorNum, out timestamp);

            oPaymentConfirmationViewModel.TimeStamp = timestamp;

            return View(oPaymentConfirmationViewModel);


        }


        [NonAction]
        public string CreateFp(string email, string amount, string sysId, string username, string factorNum, out string timestamp)
        {
            timestamp = Infrastructure.Utility.GetTimestamp(DateTime.Now);

            //string fp = HMAC_MD5(tKey, email + "^" + amount + "^" + sysId + "^" + username + "^" + timestamp + "^" + factornum);

            string fp = Infrastructure.Utility.HMAC_MD5
                (Setting.TransactionKeyForOPMS, email + "^" + amount + "^" + sysId + "^" + username + "^" + timestamp + "^" + factorNum);

            return fp;
        }

        [Authorize(Roles = "AccountManager, SystemAdministrator")]
        [HttpGet]
        public virtual ActionResult ReceivedReports(Guid? id)
        {
            //the NToN relation between tables "AssignmentFiles" and "Accounts" has been created to 
            //connect Sent-Files(sent-Reports) to the corresponding account.
            //as you can see in action "AddReceiver" in ReportController that the assignmentFile is added
            //to the Account.AssignmentFiles list. Thus, every assignmentFile in this list is considered as
            //Received Report.
            List<MvcEshop.Models.AssignmentFile> lstFiles = new List<MvcEshop.Models.AssignmentFile>();
            MvcEshop.Models.Account oAccount = MVC.Account.GetExistingAccounts()
                .Where(current => current.AccountId == id.Value)
                .FirstOrDefault();

            return View(oAccount.AssignmentFiles);
        }

        //[Authorize(Roles = "Instructor")]
        [HttpGet]
        public virtual ActionResult Inbox(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<MvcEshop.Models.AssignmentFile> lstFiles = new List<MvcEshop.Models.AssignmentFile>();

            //this viewbag is used for showing Message "Report is not ready yet" which is used for users having different roles
            // and click on "magnifier icon".
            ViewBag.FailureMessage = null;

            if (TempData["FailureMessage"] != null)
            {
                ViewBag.FailureMessage = TempData["FailureMessage"].ToString();
                TempData["FailureMessage"] = null;
            }


            //if the role of current user is not instructor, consider id as uploaderUserId
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) != 4)
            {
                //if the role of current user is "account manager" or "system administrator".
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9 ||
                    System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
                {
                    Guid guidAccountId =
                             Guid.Parse(System.Web.HttpContext.Current.Session["AccountOrClassId"].ToString());

                    System.Web.HttpContext.Current.Session["AccountOrClassId"] = guidAccountId;

                    List<System.Guid> lstAssignmentFileIds = new List<System.Guid>();

                    //select assignmentfiles of "Operational" receipts for this account.
                    lstAssignmentFileIds = (from r in DFEntities.Receipts
                                            where r.CreatorUserId == id && r.AccountId == guidAccountId &&
                                            r.ReceiptTypeId == 3
                                            select r.AssignmentFileId.Value)
                               .ToList();

                    foreach (System.Guid assignmentFileId in lstAssignmentFileIds)
                    {
                        lstFiles.Add(GetAssignmentFileById(assignmentFileId));
                    }

                    MvcEshop.Models.User oUser = MVC.User.GetUserByUserId(id.Value);

                    System.Web.HttpContext.Current.Session["CurrentSiteMapForUserFullName"] =
                oUser.FullName;

                    return View(lstFiles);
                }


            }

            //Guid guidAssignmentId = Guid.Parse(TempData["AssignmentId"].ToString());

            //TempData["AssignmentId"] = guidAssignmentId;



            lstFiles =
                DFEntities.AssignmentFiles
                .Where(current => current.AssignmentId == id)
                .OrderByDescending(current => current.UploadDate)
                .ToList();

            System.Web.HttpContext.Current.Session["AssignmentId"] = id;


            return View(lstFiles);


        }


        public virtual ActionResult PreviousRequests(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            List<AssignmentFile> lstFiles = new List<AssignmentFile>();

            //checks whether the current User Id equals the id passed to inbox() in order to prevent hacking.
            if (MVC.User.GetUserIdByEmail(User.Identity.Name) == id.Value)
            {

                lstFiles =
                DFEntities.AssignmentFiles
                .Where(current => current.UploaderUserId == id.Value && current.IsDeleted == false)
                .OrderByDescending(current => current.UploadDate)
                .ToList();

                ViewBag.Title = Resources.DisplayNames.ShowRequests;

                //this viewbag is used for showing Message "Report is not ready yet" which is used for users having different roles
                // and click on "magnifier icon".
                ViewBag.FailureMessage = null;

                if (TempData["FailureMessage"] != null)
                {
                    ViewBag.FailureMessage = TempData["FailureMessage"].ToString();
                    TempData["FailureMessage"] = null;
                }

                return View("Inbox", lstFiles);
            }

            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }



        [Authorize()]

        //[Authorize(Roles = "Instructor")]
        public void Download(string fileNameInServer)
        {

            //bool blnIsCurrentUserAllowedToDownloadTheFile = false;

            Guid guidUserId =
                MVC.User.GetUserIdByEmail(User.Identity.Name);

            //if the current role of the user is "account manager".
            if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                MvcEshop.Models.AssignmentFile oAssignmentFile = DFEntities.AssignmentFiles
                       .Where(current => current.FileNameInServer == fileNameInServer)
                       .FirstOrDefault();

                ////check if the file to be downloaded is related to the appropriate account.
                //if (oAssignmentFile.Receipts.FirstOrDefault().AccountId ==
                //    System.Guid.Parse(System.Web.HttpContext.Current.Session["AccountOrClassId"].ToString()))
                //{
                Infrastructure.File.WriteFileOnClientForDownload
                    (DFEntities.Settings.FirstOrDefault().AssignmentFilesPath, fileNameInServer, Response);
                //}

            }


            //if the current role of the user is "Instructor".
            if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
            {
                MvcEshop.Models.Assignment oAssignment = DFEntities.AssignmentFiles
                       .Where(current => current.FileNameInServer == fileNameInServer)
                       .Select(current => current.Assignment)
                       .FirstOrDefault();

                MvcEshop.Models.User oInstructor = oAssignment.Class.Instructor;

                if (guidUserId == oInstructor.UserId && oInstructor.IsDeleted == false
                    && oInstructor.IsActive == true)
                {
                    Infrastructure.File.WriteFileOnClientForDownload
                        (DFEntities.Settings.FirstOrDefault().AssignmentFilesPath, fileNameInServer, Response);
                }

            }

            //if the current role of the user is "system administrator".
            if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
            {
                Infrastructure.File.WriteFileOnClientForDownload
                        (DFEntities.Settings.FirstOrDefault().AssignmentFilesPath, fileNameInServer, Response);
            }


            //User oCurrentUser =
            //    //MVC.User.GetUserByUsername(User.Identity.Name);
            //    DFEntities.Users
            //    .Where(current => current.IsDeleted == false && current.Email == User.Identity.Name)
            //    .ToList()
            //    .FirstOrDefault();

            //AssignmentFile oFile = oCurrentUser.AssignmentFiles
            //    .Where(current => current.IsDeleted == false &&
            //     current.FileNameInServer == fileNameInServer)
            //    .ToList()
            //    .FirstOrDefault();

            //if (oFile != null)
            //{
            Infrastructure.File.WriteFileOnClientForDownload
                (DFEntities.Settings.FirstOrDefault().AssignmentFilesPath, fileNameInServer, Response);
            //}
        }


        public void PublicDownload(string fileNameInServer)
        {
            Infrastructure.File.WriteFileOnClientForDownload
                    (DFEntities.Settings.FirstOrDefault().AssignmentFilesPath, fileNameInServer, Response);
        }

        public void DownloadFromProjectFolders(string projectFolderPath, string fileName)
        {
            Infrastructure.File.WriteFileOnClientForDownload
                    (System.Web.Hosting.HostingEnvironment.MapPath
                    (projectFolderPath), fileName, Response);
        }

        public void DownloadEmailListExcelFile()
        {
            DownloadFromProjectFolders("~/Content/Files/DataEntryFiles/", "EmailListForGroupEnrollment.xlsx");
        }

        public void DownloadPDFReport(Guid assignmentFileId)
        {
            AssignmentFile oAssignmentFile =
                MVC.Assignment.GetAssignmentFileById(assignmentFileId);

            string strPDFFilePath = string.Empty;

            //at first we have to create the report and then download the report.
            MVC.Report.CreatePDFReportFileByAssignmentFileIdAndSaveInServer(assignmentFileId, out strPDFFilePath);

            //download the report.
            Infrastructure.File.WriteFileOnClientForDownload
                    (DFEntities.Settings.FirstOrDefault().AssignmentFilesPath,
                    oAssignmentFile.PDFReportFileNameInServer, Response);
        }

        //This is a new showresult, the previous "showresult" could be seen in the next comment block.
        [HttpGet]
        [ValidateInput(false)]
        public virtual ActionResult ShowResult(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            MvcEshop.Models.AssignmentFile oAssignmentFile = DFEntities.AssignmentFiles
                       .Where(current => current.FileId == id.Value)
                       .FirstOrDefault();

            if (oAssignmentFile.Request == null || String.IsNullOrEmpty(oAssignmentFile.Request.FinalReport))
            {
                TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

                //if the current role of the user is not instructor.
                if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) != 4)
                {
                    return RedirectToAction
                        (MVC.Assignment.PreviousRequests(MVC.User.GetUserIdByEmail(User.Identity.Name)));
                }

                //if the current role of the user is "Instructor".
                if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
                {
                    return RedirectToAction
                    (MVC.Assignment.Inbox(oAssignmentFile.Assignment.AssignmentId));
                }
            }

            ClassLibrary4.TextSpliter oTextSpliter =
                    new ClassLibrary4.TextSpliterSingle(oAssignmentFile.Request.FinalReport,
                        DFEntities.Settings.FirstOrDefault().VolumeToShowPerPage);

            string strFirstPart = oTextSpliter.firstPart;

            System.Web.HttpContext.Current.Session["FinalReport"] = oTextSpliter;

            MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
                new ViewModels.Assignment.ShowResultViewModel();

            oShowResultViewModel =
               new ViewModels.Assignment.ShowResultViewModel
                   (strFirstPart, oAssignmentFile.Request.SimilarityPercentage.Value,
                   oAssignmentFile.Request.Results, oTextSpliter.currentPos + 1,
                   oTextSpliter.totalParts, oAssignmentFile.FileNameInServer,
                   oAssignmentFile.PDFReportFileNameInServer, oAssignmentFile.FileId);

            System.Web.HttpContext.Current.Session["ShowResultViewModelObject"] =
                oShowResultViewModel;

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] =
               oAssignmentFile.SubmissionTitle + " " + ">";

            return View(oShowResultViewModel);


        }



        //[HttpGet]
        //[ValidateInput(false)]
        //public virtual ActionResult ShowResult(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    //ClassLibrary4.TextSpliter oFinalReport =
        //    //            (ClassLibrary4.TextSpliter)System.Web.HttpContext.Current.Session["FinalReport"];

        //    //if (oFinalReport==null)
        //    //{
        //    MvcEshop.Models.AssignmentFile oAssignmentFile = DFEntities.AssignmentFiles
        //               .Where(current => current.FileId == id.Value)
        //               .FirstOrDefault();

        //    /*checks whether the request to web service was sent before 
        //     and we have an created Request object property */
        //    if (oAssignmentFile.Request != null)
        //    {

        //        /*and it's final-report 
        //        field in db is null or empty that means the final-report
        //        has not been received from web service yet.*/
        //        if (string.IsNullOrEmpty(oAssignmentFile.Request.FinalReport))
        //        {

        //            MvcEshop.DocumentFinder.RequestIOServiceClient oRequestIOServiceClient =
        //                    new MvcEshop.DocumentFinder.RequestIOServiceClient();

        //            MvcEshop.DocumentFinder.OriginalityReport oFinalReport = null;

        //            try
        //            {
        //                //connect to web service and try to get the final-report
        //                oFinalReport =
        //                    oRequestIOServiceClient.GetFinalReportByRequestID
        //                    (oAssignmentFile.Request.RequestId);

        //            }

        //            catch (Exception ex)
        //            {

        //                Infrastructure.Communication.SendEmail
        //               (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
        //               Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
        //               DFEntities.Settings.FirstOrDefault().SupportTeamEmail, "خطا در سرویس همانندجویی",
        //               string.Format("{0} {1}", ex.Message, oAssignmentFile.FileId));

        //                TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

        //                //if the current role of the user is "free user".
        //                if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 8)
        //                {
        //                    return RedirectToAction
        //                        (MVC.Assignment.Inbox(MVC.User.GetUserIdByEmail(User.Identity.Name)));
        //                }

        //                return RedirectToAction
        //                    (MVC.Assignment.Inbox(oAssignmentFile.Assignment.AssignmentId));

        //            }


        //            //MvcEshop.DocumentFinder.Results oResults =
        //            //    oRequestIOServiceClient.GetResultsByRequestID(8999);
        //            //string intSimilarity= oResults.ResultTable.Rows[0]["subjectSimilarity"].ToString();
        //            //string address = oResults.ResultTable.Rows[0]["Address"].ToString();        


        //            //checks the ResultCode, if the ResultCode=-1, the docfinder service is 
        //            //not responding and the client has to come back later and click on ShowResult Icon
        //            //again.
        //            //but if the ResultCode=0, the DocFinder service is doing it's best and is responding
        //            //but we don't know whether docFinding is finished for this file or not.

        //            if (oFinalReport.ResultCode == -1)
        //            {
        //                TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

        //                //if the current role of the user is "free user".
        //                if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 8)
        //                {
        //                    return RedirectToAction
        //                        (MVC.Assignment.Inbox(MVC.User.GetUserIdByEmail(User.Identity.Name)));
        //                }

        //                return RedirectToAction
        //                    (MVC.Assignment.Inbox(oAssignmentFile.Assignment.AssignmentId));
        //            }

        //            else if (oFinalReport.ResultCode == 0)
        //            {
        //                //if the following is true, it means that report for this file is not ready yet.
        //                if (string.IsNullOrEmpty(oFinalReport.HtmlReport))
        //                {
        //                    TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

        //                    //if the current role of the user is "free user".
        //                    if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 8)
        //                    {
        //                        return RedirectToAction
        //                            (MVC.Assignment.Inbox(MVC.User.GetUserIdByEmail(User.Identity.Name)));
        //                    }

        //                    return RedirectToAction
        //                    (MVC.Assignment.Inbox(oAssignmentFile.Assignment.AssignmentId));
        //                }

        //                //it means that the report for this file is ready.
        //                else
        //                {
        //                    oAssignmentFile.Request.FinalReport = oFinalReport.HtmlReport;

        //                    oAssignmentFile.Request.SimilarityPercentage =
        //                        oFinalReport.SimilarityPercent;

        //                    //get results and copy references

        //                    MvcEshop.DocumentFinder.Results oResults =
        //                       oRequestIOServiceClient.GetResultsByRequestID(oAssignmentFile.Request.RequestId);

        //                    foreach (var result in oResults.ResultList)
        //                    {

        //                        MvcEshop.Models.Result objResult =
        //                            new MvcEshop.Models.Result();

        //                        objResult.Address = result.Address;

        //                        objResult.SimilarityAmountOrder =
        //                            Convert.ToInt32(result.ItemNumber);

        //                        objResult.SimilarityPercentage = result.SubjectSimilarity;

        //                        oAssignmentFile.Request.Results.Add(objResult);
        //                    }


        //                    DFEntities.Entry(oAssignmentFile).State = EntityState.Modified;
        //                    DFEntities.SaveChanges();

        //                    ClassLibrary4.TextSpliter oTextSpliter =
        //            new ClassLibrary4.TextSpliterSingle(oAssignmentFile.Request.FinalReport,
        //                DFEntities.Settings.FirstOrDefault().VolumeToShowPerPage);

        //                    string strFirstPart = oTextSpliter.firstPart;

        //                    System.Web.HttpContext.Current.Session["FinalReport"] = oTextSpliter;

        //                    MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
        //                        new ViewModels.Assignment.ShowResultViewModel
        //                            (strFirstPart, oAssignmentFile.Request.SimilarityPercentage.Value,
        //                            oAssignmentFile.Request.Results, oTextSpliter.currentPos + 1,
        //                            oTextSpliter.totalParts);

        //                    System.Web.HttpContext.Current.Session["ShowResultViewModelObject"] =
        //                        oShowResultViewModel;

        //                    return View(oShowResultViewModel);

        //                }
        //            }

        //            MvcEshop.Models.DocFinderServiceErrorLog oErrorLog =
        //                new MvcEshop.Models.DocFinderServiceErrorLog();

        //            oErrorLog.AssignmentFileId = oAssignmentFile.FileId;
        //            oErrorLog.ErrorText = "ResultCode is neither -1 nor 0 ";

        //            DFEntities.DocFinderServiceErrorLogs.Add(oErrorLog);
        //            DFEntities.SaveChanges();

        //            //send email to the support team
        //            Infrastructure.Communication.SendEmail
        //                   (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
        //                   Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
        //                   DFEntities.Settings.FirstOrDefault().SupportTeamEmail,
        //                   "خطا در سرویس همانندجویی",
        //                   string.Format("ResultCode is neither -1 nor 0 For RequestId {0}",
        //                   oAssignmentFile.Request.RequestId));

        //            //send the client to the assignment inbox and show the appropriate message to them.
        //            TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

        //            return RedirectToAction
        //            (MVC.Assignment.Inbox(oAssignmentFile.Assignment.AssignmentId));

        //        }

        //        else
        //        {
        //            ClassLibrary4.TextSpliter oTextSpliter =
        //            new ClassLibrary4.TextSpliterSingle(oAssignmentFile.Request.FinalReport,
        //                DFEntities.Settings.FirstOrDefault().VolumeToShowPerPage);

        //            string strFirstPart = oTextSpliter.firstPart;

        //            System.Web.HttpContext.Current.Session["FinalReport"] = oTextSpliter;

        //            MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
        //                        new ViewModels.Assignment.ShowResultViewModel
        //                            (strFirstPart, oAssignmentFile.Request.SimilarityPercentage.Value,
        //                            oAssignmentFile.Request.Results, oTextSpliter.currentPos + 1,
        //                            oTextSpliter.totalParts);

        //            System.Web.HttpContext.Current.Session["ShowResultViewModelObject"] =
        //                oShowResultViewModel;

        //            return View(oShowResultViewModel);

        //        }
        //    }

        //    //}

        //    else
        //    {

        //        ////send email to the support team
        //        //Infrastructure.Communication.SendEmail
        //        //       (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
        //        //       Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
        //        //       DFEntities.Settings.FirstOrDefault().SupportTeamEmail, "خطا در سرویس همانندجویی",
        //        //       "Request object property is null");


        //        //send the client to the assignment inbox and show the appropriate message to them.
        //        TempData["FailureMessage"] = Resources.Messages.ResultIsNotReadyYet;

        //        //if the current role of the user is "Instructor".
        //        if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
        //        {
        //            return RedirectToAction
        //        (MVC.Assignment.Inbox(oAssignmentFile.Assignment.AssignmentId));

        //        }

        //        return RedirectToAction
        //            (Inbox(MVC.User.GetUserIdByEmail(User.Identity.Name)));
        //    }
        //}

        #region commented

        #endregion


        [HttpGet]
        public virtual ActionResult GetNextPage()
        {
            string strCurrentPart = string.Empty;

            ClassLibrary4.TextSpliter oFinalReport =
                        (ClassLibrary4.TextSpliter)System.Web.HttpContext.Current.Session["FinalReport"];

            strCurrentPart = oFinalReport.nextPart;

            //System.Web.HttpContext.Current.Session["FinalReport"] = oFinalReport;

            MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
                (MvcEshop.ViewModels.Assignment.ShowResultViewModel)
                 System.Web.HttpContext.Current.Session["ShowResultViewModelObject"];

            oShowResultViewModel.FinalReport = strCurrentPart;
            oShowResultViewModel.CurrentPos = oFinalReport.currentPos + 1;
            oShowResultViewModel.TotalParts = oFinalReport.totalParts;

            return View("ShowResult", oShowResultViewModel);

        }

        [HttpGet]
        public virtual ActionResult GetPreviousPage()
        {
            string strCurrentPart = string.Empty;

            ClassLibrary4.TextSpliter oFinalReport =
                        (ClassLibrary4.TextSpliter)System.Web.HttpContext.Current.Session["FinalReport"];

            strCurrentPart = oFinalReport.previousPart;

            //System.Web.HttpContext.Current.Session["FinalReport"] = oFinalReport;

            MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
               (MvcEshop.ViewModels.Assignment.ShowResultViewModel)
                System.Web.HttpContext.Current.Session["ShowResultViewModelObject"];

            oShowResultViewModel.FinalReport = strCurrentPart;
            oShowResultViewModel.CurrentPos = oFinalReport.currentPos + 1;
            oShowResultViewModel.TotalParts = oFinalReport.totalParts;

            return View("ShowResult", oShowResultViewModel);

        }

        [HttpGet]
        public virtual ActionResult GetFirstPage()
        {
            string strCurrentPart = string.Empty;

            ClassLibrary4.TextSpliter oFinalReport =
                        (ClassLibrary4.TextSpliter)System.Web.HttpContext.Current.Session["FinalReport"];

            strCurrentPart = oFinalReport.firstPart;

            //System.Web.HttpContext.Current.Session["FinalReport"] = oFinalReport;

            MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
               (MvcEshop.ViewModels.Assignment.ShowResultViewModel)
                System.Web.HttpContext.Current.Session["ShowResultViewModelObject"];

            oShowResultViewModel.FinalReport = strCurrentPart;
            oShowResultViewModel.CurrentPos = oFinalReport.currentPos + 1;
            oShowResultViewModel.TotalParts = oFinalReport.totalParts;

            return View("ShowResult", oShowResultViewModel);

        }

        [HttpGet]
        public virtual ActionResult GetLastPage()
        {
            string strCurrentPart = string.Empty;

            ClassLibrary4.TextSpliter oFinalReport =
                        (ClassLibrary4.TextSpliter)System.Web.HttpContext.Current.Session["FinalReport"];

            strCurrentPart = oFinalReport.lastPart;

            //System.Web.HttpContext.Current.Session["FinalReport"] = oFinalReport;

            MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
               (MvcEshop.ViewModels.Assignment.ShowResultViewModel)
                System.Web.HttpContext.Current.Session["ShowResultViewModelObject"];

            oShowResultViewModel.FinalReport = strCurrentPart;
            oShowResultViewModel.CurrentPos = oFinalReport.currentPos + 1;
            oShowResultViewModel.TotalParts = oFinalReport.totalParts;

            return View("ShowResult", oShowResultViewModel);

        }


        [HttpGet]
        public virtual ActionResult CheckPlagiarism()
        {
            return View();
        }


        [HttpPost]
        public virtual ActionResult CheckPlagiarism
            (MvcEshop.ViewModels.Home.ShowResultForEveryOneViewModel showResultForEveryOneViewModel)
        {

            if (showResultForEveryOneViewModel.ReportCode == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            MvcEshop.Models.AssignmentFile oAssignmentFile = DFEntities.AssignmentFiles
                       .Where(current => current.ReportCode == showResultForEveryOneViewModel.ReportCode)
                       .FirstOrDefault();

            //if (oAssignmentFile.Request == null || String.IsNullOrEmpty(oAssignmentFile.Request.FinalReport))
            //{
            //    ModelState.AddModelError("", Resources.Messages.ResultIsNotReadyYet);

            //}

            if (oAssignmentFile== null)
            {
                ModelState.AddModelError
                    ("CodeIsNotCorrect", string.Format(Resources.Messages.RegularExpressionValidator, Resources.DisplayNames.ReportCode));

            }


            if (ModelState.IsValid)
            {
                ClassLibrary4.TextSpliter oTextSpliter =
                            new ClassLibrary4.TextSpliterSingle(oAssignmentFile.Request.FinalReport,
                                DFEntities.Settings.FirstOrDefault().VolumeToShowPerPage);

                string strFirstPart = oTextSpliter.firstPart;

                System.Web.HttpContext.Current.Session["FinalReport"] = oTextSpliter;

                MvcEshop.ViewModels.Assignment.ShowResultViewModel oShowResultViewModel =
                    new ViewModels.Assignment.ShowResultViewModel();

                oShowResultViewModel =
                   new ViewModels.Assignment.ShowResultViewModel
                       (strFirstPart, oAssignmentFile.Request.SimilarityPercentage.Value,
                       oAssignmentFile.Request.Results, oTextSpliter.currentPos + 1,
                       oTextSpliter.totalParts, oAssignmentFile.FileNameInServer,
                       oAssignmentFile.PDFReportFileNameInServer, oAssignmentFile.FileId);

                System.Web.HttpContext.Current.Session["ShowResultViewModelObject"] =
                    oShowResultViewModel;


                return View("ShowResult", oShowResultViewModel);

            }

            return View(showResultForEveryOneViewModel);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DFEntities.Dispose();
            }
            base.Dispose(disposing);
        }


       
        public void FollowUpUnreceivedReports()
        {

            System.Globalization.CultureInfo oCultureInfo =
              new System.Globalization.CultureInfo("fa-IR");

            System.Threading.Thread.CurrentThread.CurrentUICulture = oCultureInfo;


            List<AssignmentFile> lstRequestsWithNoReceivedFinalReport = DFEntities
                .AssignmentFiles.Where(current => current.IsDeleted == false
                    && current.Request != null
                    && current.Request.FinalReport == null)
                    .ToList();

            //foreach (var item in lstRequestsWithNoReceivedFinalReport)
            //{
            //    item.IsDeleted = true;
            //    DFEntities.Entry(item).State = EntityState.Modified;
            //    DFEntities.SaveChanges();
            //}

            if (lstRequestsWithNoReceivedFinalReport.Count != 0)
            {
                foreach (AssignmentFile assignmentFile in lstRequestsWithNoReceivedFinalReport)
                {
                    if (String.Equals(GetFinalReportByFileIdAndSaveInDatabase(assignmentFile.FileId), Resources.Messages.ReportIsReady))
                    {
                        //send email notification.

                        string strMainMessage =
                            string.Format(Resources.Messages.ReportIsReady, Resources.DisplayNames.DocFindingReport,
                            Resources.DisplayNames.Title, assignmentFile.SubmissionTitle);

                        string strFullMessage =
                            string.Format(Resources.Messages.EmailMessageDefaultGreetingForUser, strMainMessage);

                        string strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                            DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                            strFullMessage);

                        Infrastructure.Communication.SendEmail
                            (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                            Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                            assignmentFile.UploaderUser.Email, Resources.DisplayNames.ShowResult, strEmailBody, null);

                        //send sms notification if programmer or system administrator has set "SendSms" to true.
                        //and also the user has chosen sendSms option.
                        if (Setting.SendSms == true && assignmentFile.UploaderUser.SendSms == true)
                        {
                            Kavenegar.SDK.Test.Program.SendSMS(assignmentFile.UploaderUser.CellPhoneNumber, strMainMessage,
                                Setting.KeyForSMSServiceCommunication);
                        }

                    }
                }
            }
        }



        [NonAction]
        public string GetFinalReportByFileIdAndSaveInDatabase(Guid id)
        {
            System.Globalization.CultureInfo oCultureInfo =
              new System.Globalization.CultureInfo("fa-IR");

            System.Threading.Thread.CurrentThread.CurrentUICulture = oCultureInfo;

            string strResponse = string.Empty;

            MvcEshop.Models.AssignmentFile oAssignmentFile = DFEntities.AssignmentFiles
                       .Where(current => current.FileId == id)
                       .FirstOrDefault();

            /*checks whether the request to web service was sent before 
             and we have an created Request object property */
            if (oAssignmentFile.Request != null)
            {

                /*and it's final-report 
                field in db is null or empty that means the final-report
                has not been received from web service yet.*/
                if (string.IsNullOrEmpty(oAssignmentFile.Request.FinalReport))
                {
                    MvcEshop.DocumentFinder.OriginalityReport oFinalReport = null;

                    MvcEshop.DocumentFinder.RequestIOServiceClient oRequestIOServiceClient =
                                new MvcEshop.DocumentFinder.RequestIOServiceClient();
                    try
                    {

                        //connect to web service and try to get the final-report
                        oFinalReport =
                            oRequestIOServiceClient.GetFinalReportByRequestID
                            (oAssignmentFile.Request.RequestId);

                    }

                    catch (Exception ex)
                    {

                        Infrastructure.Communication.SendEmail
                       (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                       Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                       DFEntities.Settings.FirstOrDefault().SupportTeamEmail, "خطا در سرویس همانندجویی",
                       string.Format("{0} {1}", ex.Message, oAssignmentFile.FileId), null);

                    }

                    //checks the ResultCode, if the ResultCode=-1, the docfinder service is 
                    //not responding and the client has to come back later and click on ShowResult Icon
                    //again.
                    //but if the ResultCode=0, the DocFinder service is doing it's best and is responding
                    //but we don't know whether docFinding is finished for this file or not.

                    if (oFinalReport.ResultCode == -1)
                    {
                        strResponse = Resources.Messages.ResultIsNotReadyYet;

                        return strResponse;
                    }

                    else if (oFinalReport.ResultCode == 0)
                    {
                        //if the following is true, it means that report for this file is not ready yet.
                        if (string.IsNullOrEmpty(oFinalReport.HtmlReport))
                        {
                            strResponse = Resources.Messages.ResultIsNotReadyYet;

                            return strResponse;
                        }

                        //it means that the report for this file is ready.
                        else
                        {
                            oAssignmentFile.Request.FinalReport = oFinalReport.HtmlReport;

                            oAssignmentFile.Request.SimilarityPercentage =
                                oFinalReport.SimilarityPercent;

                            //get results and copy references

                            MvcEshop.DocumentFinder.Results oResults =
                               oRequestIOServiceClient.GetResultsByRequestID(oAssignmentFile.Request.RequestId);

                            foreach (var result in oResults.ResultList)
                            {

                                MvcEshop.Models.Result objResult =
                                    new MvcEshop.Models.Result();

                                objResult.Address = result.Address;

                                objResult.SimilarityAmountOrder =
                                    Convert.ToInt32(result.ItemNumber);

                                objResult.SimilarityPercentage = result.SubjectSimilarity;

                                oAssignmentFile.Request.Results.Add(objResult);
                            }


                            DFEntities.Entry(oAssignmentFile).State = EntityState.Modified;
                            DFEntities.SaveChanges();

                            strResponse = Resources.Messages.ReportIsReady;

                            return strResponse;
                        }
                    }
                }
            }

            strResponse = Resources.Messages.NoRequestCreated;

            return strResponse;
        }


        
        public void ResendUnsentRequests()
        {


            List<AssignmentFile> lstUnsentRequests = new List<AssignmentFile>();

            lstUnsentRequests = DFEntities
                .AssignmentFiles.Where(current => current.Request == null
                && current.IsDeleted == false)
                .ToList();

            if (lstUnsentRequests.Count != 0)
            {
                string strText = string.Empty;

                foreach (AssignmentFile item in lstUnsentRequests)
                {
                    //if (item.NotPaidFor != null && item.NotPaidFor == true)
                    //{
                    //    continue;
                    //}

                    strText = GetTextByFileNameInServer
                        (item.FileNameInServer);

                    SendRequestToDFService(item, strText);
                }

            }
        }

        [NonAction]
        public void SendRequestToDFService(AssignmentFile assignmentFile, string text)
        {
            try
            {
                DocumentFinder.RequestIOServiceClient oRequestIOServiceClient =
                            new DocumentFinder.RequestIOServiceClient();

                DocumentFinder.RequestMessage oRequestMessage =
                new DocumentFinder.RequestMessage();

                oRequestMessage.CustomerMail = DFEntities.Settings
                    .FirstOrDefault().CustomerEmailForDFService;

                oRequestMessage.Title = "test";
                oRequestMessage.TextBody = text;

                int intResult = oRequestIOServiceClient.PutMessage(oRequestMessage);

                if (intResult == -1)
                {
                    throw new Exception("Web service is not responding, maybe the information you are passing to communicate with web service is not correct or the information has been changed");
                }


                MvcEshop.Models.Request oRequest = new MvcEshop.Models.Request();
                oRequest.RequestId = intResult;
                assignmentFile.Request = oRequest;

                DFEntities.Entry(assignmentFile).State = EntityState.Modified;
                DFEntities.SaveChanges();
            }

            catch (Exception ex)
            {

                Infrastructure.Communication.SendEmail
               (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
               Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
               DFEntities.Settings.FirstOrDefault().SupportTeamEmail, "خطا در سرویس همانندجویی",
               string.Format("{0} {1}", ex.Message, assignmentFile.FileId), null);

            }


        }



        [NonAction]
        public AssignmentFile GetAssignmentFileById(System.Guid id)
        {
            AssignmentFile oAssignmentFile = DFEntities.AssignmentFiles
                .Where(current => current.FileId == id && current.IsDeleted == false)
                .ToList()
                .FirstOrDefault()
                ;

            return oAssignmentFile;

        }



        [NonAction]
        public string GetTextByFileNameInServer(string fileNameInServer)
        {
            string extension = Path.GetExtension(fileNameInServer);

            string strText = string.Empty;

            if (extension == ".doc" || extension == ".txt")
            {
                //Get the text content of the doc file
                strText = Infrastructure.DocToText.GetText(string.Format
                    (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                    + fileNameInServer);

            }

            if (extension == ".docx")
            {
                //Get the text content of the docx file
                Infrastructure.DocxToText oDocxToText = new Infrastructure.DocxToText(string.Format
                    (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                    + fileNameInServer);

                strText = oDocxToText.ExtractText();

            }

            return strText;
        }



        [NonAction]
        public int PaymentAmountBasedOnAssignmentFile
            (Guid assignmentFileId, bool mustSearchInIrandoc, bool mustSearchInInternet)
        {

            //Infrastructure.BusinessLogic.SingleTariff oSingleTariff =
            //    Infrastructure.BusinessLogic.SingleTariff.SingleTariffInstance;

            Infrastructure.BusinessLogic.SingleTariff.AssignmentFileId = assignmentFileId;
            //oSingleTariff.DFEntities = DFEntities;

            int intPaymentAmount = 0;

            //calculate the payment-amount based on Tariff Type
            switch (Infrastructure.BusinessLogic.SingleTariff.Tariff.TariffTypeId)
            {
                //رایگان
                case 1:
                    intPaymentAmount = new Infrastructure.BusinessLogic.Free().CalculatePrice();
                    break;

                //مقطوع              
                case 2:
                    intPaymentAmount = new Infrastructure.BusinessLogic.Constent().CalculatePrice();
                    break;

                //شناور
                case 3:
                    intPaymentAmount = new Infrastructure.BusinessLogic.Float().CalculatePrice(mustSearchInIrandoc, mustSearchInInternet);
                    break;
            }

            return intPaymentAmount;



        }

    }
}
