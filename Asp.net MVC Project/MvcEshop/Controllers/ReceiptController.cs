using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcEshop.Models;

namespace MvcEshop.Controllers
{
    [Authorize(Roles = "SystemAdministrator, AccountsManagement")]
    public partial class ReceiptController : Infrastructure.BaseControllerWithDatabaseContext
    {

        [HttpGet]
        public virtual ActionResult Index(Guid? id)
        {
            TempData["AccountId"] = id.Value;

            MvcEshop.Models.Account oAccount = MVC.Account.GetExistingAccounts()
                .Where(current => current.AccountId == id.Value)
                .FirstOrDefault()
                ;

            //receipts for offline-payment or free credit-addition by system administrator for a group like a conference, journal etc.
            var varReceipts = oAccount.Receipts
                .Where(current => current.IsDeleted == false &&
                    (current.ReceiptTypeId == 2 || current.ReceiptTypeId==4))
                .OrderByDescending(current => current.CreateDate)
                .ToList()
                ;

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";

            return View(varReceipts);
        }

        // GET: Contract/Details/5
        public virtual ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contract oContract = DFEntities.Contracts.Find(id);

            if (oContract == null)
            {
                return HttpNotFound();
            }
            return View(oContract);
        }

        // GET: Contract/Create
        public virtual ActionResult Create()
        {
            System.Guid guidAccountId =
                       Guid.Parse(TempData["AccountId"].ToString());

            TempData["AccountId"] = guidAccountId;

            ViewBag.AccountId = guidAccountId;

            MvcEshop.ViewModels.Receipt.CreateViewModel oCreateViewModel =
                new MvcEshop.ViewModels.Receipt.CreateViewModel();

            oCreateViewModel.GiveCreditApproach = @Resources.DisplayNames.Free;
            
            //find account to be used for currentSiteMap.
            MvcEshop.Models.Account oAccount = MVC.Account.GetExistingAccounts()
                .Where(current => current.AccountId == guidAccountId)
                .FirstOrDefault()
                ;

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";

            return View(oCreateViewModel);
        }


        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create
            (MvcEshop.ViewModels.Receipt.CreateViewModel createViewModel,
            HttpPostedFileBase ImageFile)
        {

            ////if the current role of current user is "SystemAdministrator".
            //if (System.Convert.ToInt32
            //(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
            //{
            //    if (createViewModel.PaidAmountInRial!=0 && ImageFile == null)
            //    {
            //        //Error Message to say that "image file" must be chosen.
            //        ModelState.AddModelError
            //                ("", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
            //                Resources.DisplayNames.ReceiptImageFile));

            //        goto end;

            //    }
            //}

            if (createViewModel.MustSearchInInternet == false && createViewModel.MustSearchInIrandoc == false)
            {
                ModelState.AddModelError
                       ("ChooseAtLeastOneItem", Resources.Messages.CheckBoxListValidator);
            }

            //if the "ExpirationDate" is empty.
            if (createViewModel.ExpirationDate == null)
            {
                //Error Message to say that "ExpirationDate" is required.
                ModelState.AddModelError
                        ("ExpirationDateRequired", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                        Resources.DisplayNames.ExpirationDateForVolumeUse));

                //goto end;

            }



            //if the "Free" radio button has been chosen
            if (createViewModel.GiveCreditApproach == Resources.DisplayNames.Free)
            {

                //if the "Allocated Volume" is empty.
                if (createViewModel.AllocatedVolume == null)
                {
                    //Error Message to say that "Allocated Volume" is required.
                    ModelState.AddModelError
                            ("AllocatedVolume", string.Format(Resources.Messages.RequiredFieldValidator,
                            Resources.DisplayNames.AllocatedVolume));

                    //goto end;

                }

                else if (new ValidationController().ValidateDataVolume(createViewModel.AllocatedVolume) == false)
                {
                    //Error Message to say that "AllocatedVolume" is not appropriate as Volume.
                    ModelState.AddModelError
                            ("AllocatedVolume", string.Format(Resources.Messages.VolumeTypeValidator,
                            Resources.DisplayNames.AllocatedVolume));

                }


            }


            //if the "Based On Tariff" radio button has been chosen.
            if (createViewModel.GiveCreditApproach == Resources.DisplayNames.BasedOnTariff)
            {
                if (ImageFile == null)
                {
                    //Error Message to say that "image file" must be chosen.
                    ModelState.AddModelError
                            ("ImageFile", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                            Resources.DisplayNames.ReceiptImageFile));

                    //goto end;

                }

                else
                {
                    switch (System.IO.Path.GetExtension(ImageFile.FileName))
                    {
                        //if the extension is a image extension.
                        case ".bmp":
                        case ".dib":
                        case ".jpg":
                        case ".jpeg":
                        case ".jpe":
                        case ".jfif":
                        case ".gif":
                        case ".tif":
                        case ".tiff":
                        case ".png":
                            break;

                        default:

                            ModelState.AddModelError
                                ("ImageFile", Resources.Messages.FileMustBeImage);

                            break;
                    }

                }

                //if the PaidAmountInRial is empty.
                if (createViewModel.PaidAmountInRial == null)
                {
                    //Error Message to say that "PaidAmountInRial" is required.
                    ModelState.AddModelError
                            ("PaidAmountInRial", string.Format(Resources.Messages.RequiredFieldValidator,
                            Resources.DisplayNames.PaidAmountInRial));

                    //goto end;

                }

                else if (new ValidationController().ValidateMoney(createViewModel.PaidAmountInRial) == false)
                {
                    //Error Message to say that "PaidAmountInRial" is not appropriate as money.
                    ModelState.AddModelError
                            ("PaidAmountInRial", string.Format(Resources.Messages.MoneyTypeValidator,
                            Resources.DisplayNames.PaidAmountInRial));

                }


                //if the PaymentDate is empty.
                if (createViewModel.PaymentDate == null)
                {
                    //Error Message to say that "PaymentDate" is required.
                    ModelState.AddModelError
                            ("PaymentDateRequired", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                            Resources.DisplayNames.PaymentDate));

                    //goto end;

                }

                //checks the volume of image file.

                //if (Infrastructure.Text.CountNonSpaceChars(uploadFileViewModel.Text) > DFEntities
                //    .Settings.FirstOrDefault().MaximumNumOfCharsInTextBox)
                //{
                //    ModelState.AddModelError
                //            ("", string.Format(Resources.Messages.NumOfWordCharsMustBeLessThan, DFEntities
                //    .Settings.FirstOrDefault().MaximumNumOfCharsInTextBox));

                //    goto end;
                //}

            }


            if (ModelState.IsValid)
            {


                Guid guidAccountId = Guid.Parse(TempData["AccountId"].ToString());

                TempData["AccountId"] = guidAccountId;

                MvcEshop.Models.Receipt oReceipt =
                    new MvcEshop.Models.Receipt();

                //add the new receipt to the receipts of that account.
                oReceipt.AccountId = guidAccountId;

                oReceipt.CurrentIrandocTariff = MVC.User.DFEntities.Resources
                    .Where(current => current.Id == 1)
                    .FirstOrDefault()
                    .Price.Value;

                oReceipt.CurrentInternetTariff = MVC.User.DFEntities.Resources
                    .Where(current => current.Id == 2)
                    .FirstOrDefault()
                    .Price.Value;

                

                if (createViewModel.GiveCreditApproach == Resources.DisplayNames.BasedOnTariff)
                {
                    oReceipt.PaidAmountInRial = int.Parse(createViewModel.PaidAmountInRial);
                    //calculate allocated volume based on current tariff and then put it into the new receipt's allocatedVolume. 
                    oReceipt.AllocatedVolume =
                        Infrastructure.BusinessLogic.Float.CalculateVolume
                        (oReceipt.PaidAmountInRial, createViewModel.MustSearchInIrandoc,
                        createViewModel.MustSearchInInternet, oReceipt.CurrentIrandocTariff,
                        oReceipt.CurrentInternetTariff);
                    //oReceipt.AllocatedVolume =(double)oReceipt.PaidAmountInRial / oReceipt.CurrentIrandocTariff; 

                    //set receipt type to "Offline-payment"
                    oReceipt.ReceiptTypeId = 2;

                    //save the file for this receipt.
                    MvcEshop.Models.File oFile =
                                new MvcEshop.Models.File();

                    oFile.FileName = ImageFile.FileName;
                    oFile.UploaderUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);

                    string strFileName = Guid.NewGuid().ToString() +
                            System.IO.Path.GetExtension(ImageFile.FileName);

                    ImageFile.SaveAs(string.Format
                        (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                        + strFileName);

                    oFile.FileNameInServer = strFileName;
                    oReceipt.ImageFile = oFile;

                   
                    oReceipt.PaymentDate = createViewModel.PaymentDate.Value;
                }

                //if the receipt is a free credit receipt.
                else
                {
                    oReceipt.AllocatedVolume = Double.Parse(createViewModel.AllocatedVolume);

                    //set receipt type to "Free Credit"
                    oReceipt.ReceiptTypeId = 4;
                }

                oReceipt.CreatorUserId = MVC.User.GetUserIdByEmail(User.Identity.Name);
                oReceipt.ExpirationDateForUse = createViewModel.ExpirationDate;

                oReceipt.MustSearchInIrandoc = createViewModel.MustSearchInIrandoc;
                oReceipt.MustSearchInInternet = createViewModel.MustSearchInInternet;

                

                //rereive the corresponding account.
                //MvcEshop.Models.Account oAccount = MVC.Account.GetExistingAccounts()
                //    .Where(current => current.AccountId == guidAccountId)
                //    .FirstOrDefault();

                //you must do the following before any change to oAccount in order to apply changes in sql database.
                //MVC.User.DFEntities.Entry(oAccount).State = EntityState.Modified;

                MVC.User.DFEntities.Receipts.Add(oReceipt);

                //add a receipt for the corresponding account.
                //oAccount.Receipts.Add(oReceipt);

                //update the database context.

                MVC.User.DFEntities.SaveChanges();

                //return to the list of receipts
                return RedirectToAction(MVC.Receipt.Index(guidAccountId));

            }


        end:

            return View(createViewModel);
        }


        // GET: Contract/Edit/5
        public virtual ActionResult EditOffLinePaymentReceipt(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["ReceiptId"] = id.Value;

            Receipt oReceipt = MVC.User.DFEntities.Receipts
                .Where(current => current.ReceiptId == id && current.IsDeleted == false)
                .ToList()
                .FirstOrDefault();

            if (oReceipt == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Receipt.EditOffLinePaymentReceiptViewModel oEditOffLinePaymentViewModel =
                new ViewModels.Receipt.EditOffLinePaymentReceiptViewModel();

            oEditOffLinePaymentViewModel.ExpirationDate = oReceipt.ExpirationDateForUse;
            oEditOffLinePaymentViewModel.PaidAmountInRial = oReceipt.PaidAmountInRial;
            oEditOffLinePaymentViewModel.PaymentDate = oReceipt.PaymentDate;
            oEditOffLinePaymentViewModel.MustSearchInIrandoc = oReceipt.MustSearchInIrandoc.Value;
            oEditOffLinePaymentViewModel.MustSearchInInternet = oReceipt.MustSearchInInternet.Value;


            ViewBag.AccountId = Guid.Parse(TempData["AccountId"].ToString());

            TempData["AccountId"] = ViewBag.AccountId;

            return View(oEditOffLinePaymentViewModel);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EditOffLinePaymentReceipt
            (MvcEshop.ViewModels.Receipt.EditOffLinePaymentReceiptViewModel editOffLinePaymentViewModel)
        {
            Guid guidReceiptId = Guid.Parse(TempData["ReceiptId"].ToString());

            Receipt oReceipt = MVC.User.DFEntities.Receipts
                .Where(current => current.ReceiptId == guidReceiptId && current.IsDeleted == false)
                .ToList()
                .FirstOrDefault();

            if (oReceipt == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                oReceipt.ExpirationDateForUse = editOffLinePaymentViewModel.ExpirationDate;
                oReceipt.PaymentDate = editOffLinePaymentViewModel.PaymentDate.Value;
                oReceipt.PaidAmountInRial = editOffLinePaymentViewModel.PaidAmountInRial;
                //calculate allocated volume based on current tariff and then put it into the new receipt's allocatedVolume. 
                oReceipt.AllocatedVolume =
                    Infrastructure.BusinessLogic.Float.CalculateVolume
                    (editOffLinePaymentViewModel.PaidAmountInRial, oReceipt.MustSearchInIrandoc.Value,
                    oReceipt.MustSearchInInternet.Value, oReceipt.CurrentIrandocTariff,
                    oReceipt.CurrentInternetTariff);

                MVC.User.DFEntities.Entry(oReceipt).State = EntityState.Modified;
                MVC.User.DFEntities.SaveChanges();

                Guid guidAccountId = Guid.Parse(TempData["AccountId"].ToString());
                TempData["AccountId"] = guidAccountId;

                return RedirectToAction(MVC.Receipt.Index(guidAccountId));    
            }

            return View(editOffLinePaymentViewModel);
        }


        public virtual ActionResult EditFreeCreditReceipt(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["ReceiptId"] = id.Value;

            Receipt oReceipt = MVC.User.DFEntities.Receipts
                .Where(current => current.ReceiptId == id && current.IsDeleted == false)
                .ToList()
                .FirstOrDefault();

            if (oReceipt == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Receipt.EditFreeCreditReceiptViewModel oEditFreeCreditReceiptViewModel =
                new ViewModels.Receipt.EditFreeCreditReceiptViewModel();

            oEditFreeCreditReceiptViewModel.ExpirationDate = oReceipt.ExpirationDateForUse;
            oEditFreeCreditReceiptViewModel.AllocatedVolume = oReceipt.AllocatedVolume;
            
            ViewBag.AccountId = Guid.Parse(TempData["AccountId"].ToString());

            TempData["AccountId"] = ViewBag.AccountId;

            return View(oEditFreeCreditReceiptViewModel);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EditFreeCreditReceipt
            (MvcEshop.ViewModels.Receipt.EditFreeCreditReceiptViewModel editFreeCreditReceiptViewModel)
        {
            Guid guidReceiptId = Guid.Parse(TempData["ReceiptId"].ToString());

            Receipt oReceipt = MVC.User.DFEntities.Receipts
                .Where(current => current.ReceiptId == guidReceiptId && current.IsDeleted == false)
                .ToList()
                .FirstOrDefault();

            if (oReceipt == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                oReceipt.ExpirationDateForUse = editFreeCreditReceiptViewModel.ExpirationDate;
                oReceipt.AllocatedVolume = editFreeCreditReceiptViewModel.AllocatedVolume;

                MVC.User.DFEntities.Entry(oReceipt).State = EntityState.Modified;
                MVC.User.DFEntities.SaveChanges();

                Guid guidAccountId = Guid.Parse(TempData["AccountId"].ToString());
                TempData["AccountId"] = guidAccountId;

                return RedirectToAction(MVC.Receipt.Index(guidAccountId));
            }

            return View(editFreeCreditReceiptViewModel);
        }


        // GET: Contract/Delete/5
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            ViewBag.AccountId =
                           Guid.Parse(TempData["AccountId"].ToString());

            TempData["AccountId"] = ViewBag.AccountId;

            return View();
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Receipt oReceipt = MVC.User.DFEntities.Receipts
                .Where(current => current.ReceiptId == id && current.IsDeleted == false)
                .ToList()
                .FirstOrDefault();

            if (oReceipt == null)
            {
                return HttpNotFound();
            }

            oReceipt.IsDeleted = true;

            MVC.User.DFEntities.Entry(oReceipt).State = EntityState.Modified;
            MVC.User.DFEntities.SaveChanges();

            Guid guidAccountId = Guid.Parse(TempData["AccountId"].ToString());

            TempData["AccountId"] = guidAccountId;

            return RedirectToAction(MVC.Receipt.Index(guidAccountId));
        }


        [NonAction]
        public IQueryable<MvcEshop.Models.Receipt> GetExistingReceipts()
        {
            IQueryable<MvcEshop.Models.Receipt> lstReceipts = MVC.User.DFEntities.Receipts
                .Where(current => current.IsDeleted == false)
                ;

            return lstReceipts;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DFEntities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
