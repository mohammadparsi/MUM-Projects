using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcEshop.Models;
using System.Reflection;

namespace MvcEshop.Controllers
{

    
    public partial class CustomerController : Infrastructure.BaseControllerWithDatabaseContext
    {


    [Authorize(Roles = "SystemAdministrator, CustomersManagement")]    
        // GET: Customer
        public virtual ActionResult Index()
        {
            //DFEntities = new DFEntities();

            var varCustomers =
                DFEntities.Customers
                .Where(current => current.IsDeleted == false && current.IsConfirmed==true)
                .OrderByDescending(current=>current.Accounts.FirstOrDefault().CreateDate)
                .ToList();

            ViewBag.IsForConfirmation = false;
            //oDFEntities.Dispose();

            return View(varCustomers);
        }

       
       

        //[HttpPost]
        //public virtual GetMessageBoxPartialView
        //    (MvcEshop.ViewModels.Customer.GetCheckedReportReceivers getCheckedReportReceivers)
        //{
           
	
        //}
       

        // GET: Customer/Details/5
        public virtual ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = DFEntities.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

            //[Authorize(Roles = "SystemAdministrator")]
        // GET: Customer/Create
        public virtual ActionResult Create()
        {
            ViewBag.CustomerTypeIdList = new SelectList
             (DFEntities.CustomerTypes, "CustomerTypeId", "Name");

            //if the user is outside of system.
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                //the view will be rendered in the light of introduction by any clients.
                ViewBag.IsIntroduce = true;

            }
            else
            {
                //the view will be rendered in the light of Customer Addition by system administrator.
                ViewBag.IsIntroduce = false;
            }

            
            return View();
        }


        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[Authorize(Roles = "SystemAdministrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create
            (MvcEshop.ViewModels.Customer.CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {

                MvcEshop.Models.Customer oCustomer = new MvcEshop.Models.Customer();

                oCustomer.Name = createViewModel.Name;
                oCustomer.Email = createViewModel.Email.ToLower();
                oCustomer.NotificationEmail = createViewModel.NotificationEmail;
                oCustomer.Phone = createViewModel.Phone;
                oCustomer.ContactPerson = createViewModel.ContactPerson;
                oCustomer.CustomerTypeId = createViewModel.CustomerTypeId;
                oCustomer.UsedToAllocatedVolumePercentageToNotify = createViewModel.UsedToAllocatedVolumePercentageToNotify;

                //if the user is outside of system.
                if (String.IsNullOrEmpty(User.Identity.Name))
                {
                    oCustomer.IsConfirmed = false;
                     
                }
                else
                {

                    //check if the current role of the user is "System Administrator".
                    if (System.Convert.ToInt32
                        (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
                    {
                        oCustomer.IsConfirmed = true;
                    }
                }
                    // add the customer to database
                DFEntities.Customers.Add(oCustomer);
                DFEntities.SaveChanges();


                //create an account for the customer

                //create a CreateViewModel as an input for the Method "CreateAccount"
                MvcEshop.ViewModels.Account.CreateViewModel oAccountCreateViewModel =
                    new MvcEshop.ViewModels.Account.CreateViewModel();

                //fill the input properties
                oAccountCreateViewModel.AccountId = System.Guid.NewGuid();
                oAccountCreateViewModel.AccountTitle = oCustomer.Name;
                oAccountCreateViewModel.Email = createViewModel.AccountManagerEmail.ToLower();

                //create the corresponding account and add it to the customer's accounts
                MVC.Account.CreateAccount
                    (oAccountCreateViewModel, oCustomer.CustomerId, User.Identity.Name);

                //if the user is outside of system.
                if (String.IsNullOrEmpty(User.Identity.Name))
                {
                    string strMessage = string.Format
                           (Resources.Messages.SthDoneSuccessfully,
                           Resources.DisplayNames.Introduce + " " +
                           Resources.DisplayNames.Customer);

                  return RedirectToAction(MVC.User.Message(strMessage));
                   
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }

            //if the user is outside of system.
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                //the view will be rendered in the light of introduction by any clients.
                ViewBag.IsIntroduce = true;

            }
            else
            {
                //the view will be rendered in the light of Customer Addition by system administrator.
                ViewBag.IsIntroduce = false;
            }


            ViewBag.CustomerTypeIdList = new SelectList
             (DFEntities.CustomerTypes, "CustomerTypeId", "Name", createViewModel.CustomerTypeId);
            
            return View(createViewModel);
        }


        // GET: Customer/Edit/5
        [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["CustomerId"] = id.Value;

            Customer oCustomer = DFEntities.Customers.Find(id);

            if (oCustomer == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Customer.CreateViewModel oCreateViewModel =
              new MvcEshop.ViewModels.Customer.CreateViewModel();

            oCreateViewModel.Name = oCustomer.Name;
            oCreateViewModel.Email = oCustomer.Email;
            oCreateViewModel.NotificationEmail = oCustomer.NotificationEmail;
            oCreateViewModel.ContactPerson = oCustomer.ContactPerson;
            oCreateViewModel.Phone = oCustomer.Phone;
            oCreateViewModel.CustomerTypeId = oCustomer.CustomerTypeId;
            oCreateViewModel.UsedToAllocatedVolumePercentageToNotify = oCustomer.UsedToAllocatedVolumePercentageToNotify;
                
                //find the Account Manager Email in the corresponding account

            //find the corresponding account for the customer
            MvcEshop.Models.Account oAccount = DFEntities.Accounts
               .Where(current => current.CustomerId == oCustomer.CustomerId)
               .FirstOrDefault()
               ;

            //find the Manager of the account
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.UserId == oAccount.ManagerUserId)
               .FirstOrDefault()
               ;

            if (oUser!=null)
            {
                oCreateViewModel.AccountManagerEmail = oUser.Email; 
            }

            ViewBag.CustomerTypeIdList = new SelectList
             (DFEntities.CustomerTypes, "CustomerTypeId", "Name", oCreateViewModel.CustomerTypeId);
            
            return View(oCreateViewModel);

        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit
            (MvcEshop.ViewModels.Customer.CreateViewModel createViewModel)
        {

            Guid guidCustomerId = Guid.Parse(TempData["CustomerId"].ToString());
            TempData["CustomerId"] = guidCustomerId;

            MvcEshop.Models.Customer oCustomer = DFEntities.Customers
                .Where(current => current.CustomerId == guidCustomerId)
                .FirstOrDefault()
                ;

            if (oCustomer == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                //find the account of the Customer.
                MvcEshop.Models.Account oAccount = DFEntities.Accounts
                .Where(current => current.CustomerId == oCustomer.CustomerId)
                .FirstOrDefault()
                ;

                //if the name of the Customer changes
                if (oCustomer.Name != createViewModel.Name)
                {
                    oCustomer.Name = createViewModel.Name;

                    //change the title of the corresponding account based on 
                    //the changes of the customer name
                    oAccount.AccountTitle = oCustomer.Name;
                }

                //do neccesary things after editing of the account manager email
                MVC.Account.EditAccountManagerEmail(oAccount, createViewModel.AccountManagerEmail);

                oCustomer.ContactPerson = createViewModel.ContactPerson;
                oCustomer.Email = createViewModel.Email.ToLower();
                oCustomer.Name = createViewModel.Name;
                oCustomer.Phone = createViewModel.Phone;
                oCustomer.NotificationEmail = createViewModel.NotificationEmail;
                oCustomer.CustomerTypeId = createViewModel.CustomerTypeId;
                oCustomer.UsedToAllocatedVolumePercentageToNotify = createViewModel.UsedToAllocatedVolumePercentageToNotify;

                DFEntities.Entry(oCustomer).State = EntityState.Modified;
                DFEntities.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.CustomerTypeIdList = new SelectList
                 (DFEntities.CustomerTypes, "CustomerTypeId", "Name", createViewModel.CustomerTypeId);
            

            return View(createViewModel);
        }


            [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        // GET: Customer/Delete/5
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = DFEntities.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            Customer oCustomer = DFEntities.Customers.Find(id);

            oCustomer.IsDeleted = true;

            DFEntities.Entry(oCustomer).State = EntityState.Modified;
            DFEntities.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public virtual ActionResult CheckNameUniqueness(string name)
        {
             bool blnResult = true;

             //checks uniqueness as we are editing
             if (TempData["CustomerId"] != null)
             {
                 Guid guidCustomerId = Guid.Parse(TempData["CustomerId"].ToString());

                 TempData["CustomerId"] = guidCustomerId;

                 MvcEshop.Models.Customer oCurrentCustomer = MVC.Customer.GetExistingCustomers()
                     .Where(current => current.CustomerId == guidCustomerId)
                     .FirstOrDefault()
                     ;

                 //find the customer by the name to check if any user exists with this name
                 MvcEshop.Models.Customer oCustomer = MVC.Customer.GetExistingCustomers()
                    .Where(current => current.Name == name)
                    .FirstOrDefault()
                    ;

                 //check if any user exists with this email and if this email is not the current-user's email
                 if (oCustomer != null && oCurrentCustomer.Name != name)
                 {
                     blnResult = false;
                 }
             }

            return (Json(blnResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
        }


        public List<MvcEshop.Models.Customer> GetExistingCustomers()
        {
            List<MvcEshop.Models.Customer> lstCustomers = MVC.User.DFEntities.Customers
                .Where(current => current.IsDeleted == false)
                .ToList()
                ;

            return lstCustomers;
        }


        // GET: Customer/Edit/5
        [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        public virtual ActionResult ConfirmList()
        {
            List<Customer> lstUnconfirmedCustomers = GetExistingCustomers()
                .Where(current => current.IsConfirmed == false)
                .ToList();

            ViewBag.IsForConfirmation = true;

            return  View("Index", lstUnconfirmedCustomers);

        }


        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        public virtual ActionResult Confirm
            (System.Guid id)
        {

            Customer oCustomer = GetExistingCustomers()
                .Where(current => current.CustomerId == id)
                .ToList()
                .FirstOrDefault();

            oCustomer.IsConfirmed = true;

            MVC.User.DFEntities.Entry(oCustomer).State = EntityState.Modified;
            MVC.User.DFEntities.SaveChanges();

            return RedirectToAction(MVC.Customer.ConfirmList());
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "SystemAdministrator, CustomersManagement")]
        public virtual ActionResult DeleteImmediately
            (System.Guid id)
        {

            Customer oCustomer = GetExistingCustomers()
                .Where(current => current.CustomerId == id)
                .ToList()
                .FirstOrDefault();

            oCustomer.IsDeleted = true;

            MVC.User.DFEntities.Entry(oCustomer).State = EntityState.Modified;
            MVC.User.DFEntities.SaveChanges();

            return RedirectToAction(MVC.Customer.ConfirmList());
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
