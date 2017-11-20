using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MvcEshop.Controllers
{

    [Authorize(Roles = "SystemAdministrator, InstituteAdministrator, AccountManager, AccountsManagement")]
    public partial class AccountController : Infrastructure.BaseControllerWithDatabaseContext
    {

        // GET: Account
        [HttpGet]
        public virtual ViewResult Index()
        {
            var varAccounts = 
                DFEntities.Accounts
                .Where(current=>current.IsDeleted==false && current.Customer.IsConfirmed==true)
                .OrderByDescending(current=>current.CreateDate)
                .ToList();

            //if the role of the current user is "institute administrator" or "account manager"
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString())==3 ||
                System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                System.Guid guidUserId=
                    MVC.User.GetUserIdByEmail(User.Identity.Name);

                varAccounts = varAccounts
                    .Where(current => current.ManagerUserId == guidUserId && current.Customer.IsConfirmed==true)
                    .ToList();
            }

            return View(varAccounts);
        }

        // GET: Account/Details/5
        [HttpGet]
        public virtual ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MvcEshop.Models.Account oAccount =
                DFEntities.Accounts
                .Where(current => current.AccountId == id.Value)
                .FirstOrDefault()
                ;

            if (oAccount == null)
            {
                return HttpNotFound();
            }
            return View(oAccount);
        }

        //Must Be Uncommented
        //Attention:

        //the following region will be used for the next project,
        //because in this project, only one account creation is done automatically
        //for every customer once a customer is created and as a result,
        //account creation can not be done manually by any user.
        //but in the next project we will have sub-accounts which users can join
        //and then they can create a sub-account(account table in db) manually by
        //the following actions and it's corresponding views.
        #region Account/Create(post,get)
        // GET: Account/Create
        //[HttpGet]
        //public virtual ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Account/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual ActionResult Create(MvcEshop.ViewModels.Account.CreateViewModel createViewModel)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        createViewModel.AccountId = System.Guid.NewGuid();

        //        CreateAccount(createViewModel);
        //        //      MvcEshop.Models.Account oAccount =
        //        //          new MvcEshop.Models.Account();

        //        //      oAccount.AccountTitle = createViewModel.AccountTitle;
        //        //      oAccount.JoinPassword = System.Web.Security.Membership.GeneratePassword(8, 0); 

        //        //      MvcEshop.Models.User oUser =
        //        //          DFEntities.Users
        //        //           .Where(current => current.Email == createViewModel.Email.ToLower())
        //        //           .FirstOrDefault()
        //        //           ;

        //        //      if (oUser == null)
        //        //      {
        //        //          oUser = new MvcEshop.Models.User();
        //        //          //oUser.FirstName = createViewModel.FirstName;
        //        //          //oUser.LastName = createViewModel.LastName;
        //        //          oUser.Email = createViewModel.Email.ToLower();
        //        //          DFEntities.Users.Add(oUser);
        //        //              DFEntities.SaveChanges();
        //        //      }

        //        //      oAccount.ManagerUserId = oUser.UserId;
        //        //      DFEntities.Accounts.Add(oAccount);
        //        //      DFEntities.SaveChanges();

        //        //      /*send email to the account manager to give him/her JoinPassword 
        //        //and temporary entrance Password.*/

        //        //      //build the email body
        //        //      string strEmailBody = string.Format(Resources.Messages.WelcomeToAccountManager,
        //        //          oAccount.AccountTitle, oAccount.JoinPassword);

        //        //      Infrastructure.Communication.SendEmail("smtp.gmail.com", 587, "mohammadparsi88@gmail.com",
        //        //          "22640387", "مدیر سیستم", oUser.Email, "به همانندجو خوش آمدید", strEmailBody);


        //        return RedirectToAction("Index");
        //    }

        //    return View(createViewModel);
        //} 
        #endregion

        [NonAction]
        public void CreateAccount
            (MvcEshop.ViewModels.Account.CreateViewModel createViewModel,
            System.Guid customerId, string username)
        {
            MvcEshop.Models.Account oAccount =
                    new MvcEshop.Models.Account();

            oAccount.AccountId = createViewModel.AccountId;
            oAccount.AccountTitle = createViewModel.AccountTitle;

            oAccount.JoinPassword =
                System.Guid.NewGuid().ToString().Replace("-", "").Substring(1, 8);

            if (string.IsNullOrEmpty(username)==false)
            {
                oAccount.CreatorUserId = MVC.User.GetUserIdByEmail(username); 
            }
            oAccount.CustomerId = customerId;

            string strEmailBody =
                MakeUserTheManagerOfSpecificAccountByUserEmail(oAccount, createViewModel.Email.ToLower());

            DFEntities.Accounts.Add(oAccount);
            DFEntities.SaveChanges();

            //find customer by customerId
            MvcEshop.Models.Customer oCustomer = DFEntities.Customers
                .Where(current => current.CustomerId == customerId)
                .FirstOrDefault()
                ;

            //add the account to the corresponding customer
            oCustomer.Accounts.Add(oAccount);
            DFEntities.SaveChanges();


            /*notify the account manager and send them the join password and
                the temporary password(if he is introduced) for login.*/

            //Must Be Uncommented

            Infrastructure.Communication.SendEmail
                (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                createViewModel.Email, "به همانندجو خوش آمدید", strEmailBody, null);

        }



        [NonAction]

        //make user the manager of specific account by user email and returns
        // the appropriate Message to send by email
        public string MakeUserTheManagerOfSpecificAccountByUserEmail
            (MvcEshop.Models.Account account, string email)
        {
            MvcEshop.Models.User oUser =
                MVC.User.GetExistingUsers()
                 .Where(current => current.Email == email)
                 .FirstOrDefault()
                 ;

            //email body
            string strEmailBody = null;

            //checks if the account manager has an User Account.
            //if No
            if (oUser == null)
            {
                oUser = new MvcEshop.Models.User();
                //oUser.FirstName = createViewModel.FirstName;
                //oUser.LastName = createViewModel.LastName;
                oUser.Email = email;

                string strPassword = oUser.Password;
                oUser.Password = Infrastructure.PasswordHash.CreateHash(oUser.Password);

                DFEntities.Users.Add(oUser);
                DFEntities.SaveChanges();

                //build the email body
string strDomain = DFEntities.Settings
                        .Select(current => current.Domain)
                        .FirstOrDefault();

                    string strActivationLink = @"<a href='" + strDomain +
                        "/User/ActivateUserByActiveCode?activeCode="
                        + oUser.ActiveCode +
                                "'>پیوند فعال‌سازی</a>";

                //The following used to be used in previous system. but in the new system, an account does not have any "join pass".
                //strEmailBody = string.Format
                //   (Resources.Messages.WelcomeToAccountManagerWhoIsNotRegistered,
                //   Resources.DisplayNames.AccountManager, account.AccountTitle,
                //   Resources.DisplayNames.JoinPassword, account.JoinPassword,
                //   Resources.DisplayNames.Email+Resources.DisplayNames.JunctionLetter,
                //   Resources.DisplayNames.Password + Resources.DisplayNames.JunctionLetter, 
                //   oUser.Password, Resources.DisplayNames.Profile, strActivationLink);

                    strEmailBody = string.Format
                       (Resources.Messages.WelcomeToAccountManagerWhoIsNotRegistered,
                       Resources.DisplayNames.AccountManager, account.AccountTitle,
                       string.Empty, string.Empty,
                       Resources.DisplayNames.Email + Resources.DisplayNames.JunctionLetter,
                       Resources.DisplayNames.Password + Resources.DisplayNames.JunctionLetter,
                       strPassword, Resources.DisplayNames.Profile, strActivationLink);


            }

            //if Yes
            else
            {
                //build the email body
                strEmailBody = string.Format
                    (Resources.Messages.WelcomeToAccountManagerWhoIsRegistered,
                    Resources.DisplayNames.AccountManager, account.AccountTitle);
            }

            account.ManagerUserId = oUser.UserId;

            //This used to be used for the previous system, but in the new system, we have accountManager instead of Institute administrator.
            ////add the user to role "InstituteAdministrator"
            //MyRoleProvider.AddUserToRole(oUser.Email, "InstituteAdministrator");

            //add the user to role "AccountManager"
            MyRoleProvider.AddUserToRole(oUser.Email, "AccountManager");

            //DFEntities.Entry(oUser).State = EntityState.Modified;
            //DFEntities.SaveChanges();
            strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strEmailBody);

            return strEmailBody;

        }


        // GET: Account/Edit/5
        public virtual ActionResult Edit(Guid? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["AccountId"] = id.Value;

            MvcEshop.Models.Account oAccount =
                DFEntities.Accounts
                 .Where(current => current.AccountId == id.Value)
                 .FirstOrDefault()
                 ;

            if (oAccount == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Account.EditViewModel oEditViewModel =
                new ViewModels.Account.EditViewModel();
            oEditViewModel.AccountTitle = oAccount.AccountTitle;
            //oEditViewModel.JoinPassword = oAccount.JoinPassword;
            //oEditViewModel.IsActive = oAccount.IsActive;
            //oEditViewModel.IsDeleted = oAccount.IsDeleted;

            MvcEshop.Models.User oAccountManager = MVC.User.GetExistingUsers()
                .Where(current => current.UserId == oAccount.ManagerUserId)
                .FirstOrDefault()
                ;

            if (oAccountManager != null)
            {
                oEditViewModel.Email = oAccountManager.Email;
            }

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";

            return View(oEditViewModel);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(MvcEshop.ViewModels.Account.EditViewModel editViewModel)
        {
            Guid guidAccountId = Guid.Parse(TempData["AccountId"].ToString());

            MvcEshop.Models.Account oAccount = DFEntities.Accounts
                .Where(current => current.AccountId == guidAccountId)
                .FirstOrDefault()
                ;

            if (oAccount == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                oAccount.AccountTitle = editViewModel.AccountTitle;
                //oAccount.JoinPassword = editViewModel.JoinPassword;
                //oAccount.IsActive = editViewModel.IsActive;
                //oAccount.IsDeleted = editViewModel.IsDeleted;

                EditAccountManagerEmail(oAccount, editViewModel.Email.ToLower());

                DFEntities.Entry(oAccount).State = EntityState.Modified;
                DFEntities.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(editViewModel);
        }

        [NonAction]
        public void EditAccountManagerEmail(MvcEshop.Models.Account account, string newEmail)
        {



            //find the manager of the account
            MvcEshop.Models.User oAccountManager =
                    MVC.User.GetExistingUsers()
                     .Where(current => current.UserId == account.ManagerUserId)
                     .FirstOrDefault()
                     ;

            //if the field of Account Manager Email has been changed
            if ((oAccountManager == null) ||
                (oAccountManager != null && oAccountManager.Email != newEmail))
            {

                string strEmailBody =
                    MakeUserTheManagerOfSpecificAccountByUserEmail(account, newEmail);

                //DFEntities.Entry(account).State = EntityState.Added;
                DFEntities.SaveChanges();

                /*notify the account manager and send them the join password and
                the temporary password(if he is introduced) for login.*/

                //Must Be Uncommented

                Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    newEmail, "به همانندجو خوش آمدید", strEmailBody, null);

            }
        }


        // GET: Account/Delete/5
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MvcEshop.Models.Account oAccount = DFEntities.Accounts
                .Where(current => current.AccountId == id.Value)
                .FirstOrDefault()
                ;

            if (oAccount == null)
            {
                return HttpNotFound();
            }
            return View(oAccount);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            MvcEshop.Models.Account oAccount = DFEntities.Accounts
                  .Where(current => current.AccountId == id)
                  .FirstOrDefault()
                  ;

            oAccount.IsDeleted = true;
            DFEntities.Entry(oAccount).State = EntityState.Modified;
            //DFEntities.Accounts.Remove(oAccount);
            DFEntities.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DFEntities.Dispose();
            }
            base.Dispose(disposing);
        }

        [NonAction]
        public bool DoesAccountExists(Guid id)
        {
            bool blnResult = true;

            MvcEshop.Models.Account oAccount = GetExistingAccounts()
                .Where(current => current.AccountId == id)
                .FirstOrDefault();

            if (oAccount==null)
            {
                blnResult = false;
            }

            return blnResult;
                
        }


        [NonAction]
        public List<MvcEshop.Models.Account> GetExistingAccounts()
        {
            List<MvcEshop.Models.Account> lstAccounts = MVC.User.DFEntities.Accounts
                .Where(current => current.IsDeleted == false)
                .ToList();

            return lstAccounts;
                
        }
    }
}
