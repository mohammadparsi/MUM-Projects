using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcEshop.Models;
using System.Web.Security;
using System.Web.SessionState;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using System.Data.Entity;
using System.Linq;

namespace MvcEshop.Controllers
{

    public partial class UserController : Infrastructure.BaseControllerWithDatabaseContext
    {
        [Authorize(Roles = "SystemAdministrator, Programmer")]
        public virtual ActionResult AllUsers()
        {
            List<User> lstUsers = MVC.User.GetExistingUsers().ToList();

            //define the session to be used in the action "SendGroupEmail()" for sending group email messages.
            System.Web.HttpContext.Current.Session["ListOfUnderControlUsers"] = lstUsers;

            //returns all users for programmer or system administrator in order to manage them.
            return View(lstUsers);
        }

        [HttpGet]
        public virtual ActionResult AddSystemExpert()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult AddSystemExpert
            (MvcEshop.ViewModels.User.AddSystemExpertViewModel addSystemExpertViewModel)
        {

            //System.Web.HttpContext.Current.Session["IsThisActionUsedAsNonActionMethodForUserCreation"] = true;

            //MvcEshop.ViewModels.User.CreateViewModel oCreateViewModel =
            //    new ViewModels.User.CreateViewModel();

            //oCreateViewModel.Email = addSystemExpertViewModel.Email;

            //User oUser = new User();

            User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.Email == addSystemExpertViewModel.Email)
                .ToList()
                .FirstOrDefault();

            if (oUser == null)
            {
                oUser = new User();

                oUser.Email = addSystemExpertViewModel.Email.ToLower();

                string strNotHashedPassword = oUser.Password;
                oUser.Password = Infrastructure.PasswordHash.CreateHash(oUser.Password);

                oUser.IsProfileCompleted = false;

                MVC.User.DFEntities.Users.Add(oUser);
                MVC.User.DFEntities.SaveChanges();

                string strDomain = DFEntities.Settings
                                           .Select(current => current.Domain)
                                           .FirstOrDefault();

                string strActivationLink = @"<a href='" + strDomain +
                    "/User/ActivateUserByActiveCode?activeCode="
                    + oUser.ActiveCode +
                            "'>پیوند فعال‌سازی</a>";


                string strEmailBody = string.Format
                           (Resources.Messages.WelcomeToSystemExpertUserWhoIsNotRegistered,
                           Resources.DisplayNames.SystemExpert, Resources.DisplayNames.Profile,
                           Resources.DisplayNames.Email + Resources.DisplayNames.JunctionLetter,
                           Resources.DisplayNames.Password + Resources.DisplayNames.JunctionLetter,
                           strNotHashedPassword,  strActivationLink);

                strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strEmailBody);

                Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    oUser.Email, "به همانندجو خوش آمدید", strEmailBody, null);

            }

            //give the user the role of "System Expert".
            MyRoleProvider.AddUserToRole(oUser.Email, "SystemExpert");

            //give the responsibilities based on posted viewModel.

            //if (addSystemExpertViewModel.AccountsManagement)
            //{
            //    MyRoleProvider.AddUserToRole(oUser.Email, "AccountsManagement");
            //}

            //if (addSystemExpertViewModel.CustomersManagement)
            //{
            //    MyRoleProvider.AddUserToRole(oUser.Email, "CustomersManagement");
            //}

            //if (addSystemExpertViewModel.TariffManagement)
            //{
            //    MyRoleProvider.AddUserToRole(oUser.Email, "TariffManagement");
            //}

            //if (addSystemExpertViewModel.PageContentManagement)
            //{
            //    MyRoleProvider.AddUserToRole(oUser.Email, "PageContentManagement");
            //}

            return RedirectToAction(MVC.User.AllUsers());
        }



        [HttpGet]
        public virtual ActionResult EditResponsibilities(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["UserId"] = id.Value;

            User oUser = MVC.User.GetUserByUserId(id.Value);

            if (oUser == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.User.EditResponsibilitiesViewModel oEditResponsibilitiesViewModel =
                new ViewModels.User.EditResponsibilitiesViewModel();

            if (MyRoleProvider.IsUserInRole(oUser.Email, "AccountsManagement"))
            {
                oEditResponsibilitiesViewModel.AccountsManagement = true;
            }

            if (MyRoleProvider.IsUserInRole(oUser.Email, "CustomersManagement"))
            {
                oEditResponsibilitiesViewModel.CustomersManagement = true;
            }

            if (MyRoleProvider.IsUserInRole(oUser.Email, "TariffManagement"))
            {
                oEditResponsibilitiesViewModel.TariffManagement = true;
            }

            if (MyRoleProvider.IsUserInRole(oUser.Email, "PageContentManagement"))
            {
                oEditResponsibilitiesViewModel.PageContentManagement = true;
            }

            return View(oEditResponsibilitiesViewModel);
        }



        [HttpPost]
        public virtual ActionResult EditResponsibilities
            (MvcEshop.ViewModels.User.EditResponsibilitiesViewModel editResponsibilitiesViewModel)
        {
            Guid guidUserId = Guid.Parse(TempData["UserId"].ToString());

            MvcEshop.Models.User oUser = GetUserByUserId(guidUserId);

            if (oUser == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                if (editResponsibilitiesViewModel.AccountsManagement == true)
                {
                    MyRoleProvider.AddUserToRole(oUser.Email, "AccountsManagement");
                }
                else
                {
                    MyRoleProvider.RemoveUserFromRole(oUser.Email, "AccountsManagement");
                }


                if (editResponsibilitiesViewModel.CustomersManagement == true)
                {
                    MyRoleProvider.AddUserToRole(oUser.Email, "CustomersManagement");
                }
                else
                {
                    MyRoleProvider.RemoveUserFromRole(oUser.Email, "CustomersManagement");
                }


                if (editResponsibilitiesViewModel.TariffManagement == true)
                {
                    MyRoleProvider.AddUserToRole(oUser.Email, "TariffManagement");
                }
                else
                {
                    MyRoleProvider.RemoveUserFromRole(oUser.Email, "TariffManagement");
                }


                if (editResponsibilitiesViewModel.PageContentManagement == true)
                {
                    MyRoleProvider.AddUserToRole(oUser.Email, "PageContentManagement");
                }
                else
                {
                    MyRoleProvider.RemoveUserFromRole(oUser.Email, "PageContentManagement");
                }

                //give the user the role of "System Expert".
                MyRoleProvider.AddUserToRole(oUser.Email, "SystemExpert");

                return RedirectToAction(MVC.User.AllUsers());
            }

            return View(editResponsibilitiesViewModel);
        }

        // GET: User

        [Authorize(Roles = "SystemAdministrator, InstituteAdministrator, Instructor, AccountManager")]
        public virtual ActionResult Index(Guid? id)
        {
            if (id != null)
            {
                TempData["AccountOrClassId"] = id.Value;

                //the following session variable could be used in other controllers like assignmentController.
                System.Web.HttpContext.Current.Session["AccountOrClassId"] = id.Value;
            }

            var varUsers = MVC.User.GetExistingUsers();

            //check if the current role of the user is "InstituteAdministrator" .
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 3)
            {

                MvcEshop.Models.Account oAccount = DFEntities.Accounts
                    .Where(current => current.AccountId == id.Value
                    && current.IsDeleted == false)
                    .FirstOrDefault();

                varUsers = oAccount.Users
                    .Where(current => current.IsDeleted == false)
                    .ToList();

                System.Web.HttpContext.Current.Session["AccountTitle"] = oAccount.AccountTitle;
            }

            //check if the current role of the user is "AccountManager" or "SystemAdministrator".
            if (System.Convert.ToInt32
                   (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9 ||
                System.Convert.ToInt32
                   (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
            {
                MvcEshop.Models.Account oAccount = DFEntities.Accounts
                      .Where(current => current.AccountId == id.Value
                      && current.IsDeleted == false)
                      .FirstOrDefault();

                varUsers = oAccount.CreditCards
                    .Select(current => current.User)
                    .ToList();

                //get existing users from the all users in this account.
                varUsers = 
                    varUsers.Where(current => current.IsDeleted == false).ToList();

                //define the session to be used for sending group email messages.
                System.Web.HttpContext.Current.Session["ListOfUnderControlUsers"] = varUsers;

                System.Web.HttpContext.Current.Session["AccountTitle"] = oAccount.AccountTitle;

                System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";
            }

            //check if the current role of the user is "Instructor".
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
            {


                MvcEshop.Models.Class oClass = DFEntities.Classes
                    .Where(current => current.ClassId == id.Value
                    && current.IsDeleted == false)
                    .FirstOrDefault();


                varUsers = oClass.Students
                    .Where(current => current.IsDeleted == false)
                    .ToList();

            }

            return View(varUsers);
        }

        // GET: User/Details/5
        public virtual ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = DFEntities.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public virtual ActionResult Create()
        {

            MvcEshop.Models.Gender oGender = DFEntities
                .Genders
                .FirstOrDefault()
                ;

            ViewBag.GenderId = new SelectList
                (DFEntities.Genders, "GenderId", "Description", oGender.GenderId);

            System.Guid guidAccountOrClassId =
                    Guid.Parse(TempData["AccountOrClassId"].ToString());

            TempData["AccountOrClassId"] =guidAccountOrClassId ;

            ViewBag.AccountOrClassId = guidAccountOrClassId;

            //find account to extract account title for currentSiteMap
            MvcEshop.Models.Account oAccount = DFEntities.Accounts
                      .Where(current => current.AccountId == guidAccountOrClassId
                      && current.IsDeleted == false)
                      .FirstOrDefault();

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";


            //the following must be written to initialize the session variable for the beginning of Post Action "Create".
            System.Web.HttpContext.Current.Session["IsThisActionUsedAsNonActionMethodForUserCreation"] = false;

            ////if the role of the current user is "Account Manager".
            //if (System.Convert.ToInt32
            //        (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            //{ return View("AddGroupUser"); }

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(MvcEshop.ViewModels.User.CreateViewModel createViewModel)
        {
            //determine whether this action is being used as a non-action method for just creating a user.
            bool blnIsThisActionUsedAsNonActionMethodForUserCreation =
                Boolean.Parse(System.Web.HttpContext.Current.Session["IsThisActionUsedAsNonActionMethodForUserCreation"].ToString());

            System.Web.HttpContext.Current.Session["IsThisActionUsedAsNonActionMethodForUserCreation"] = false;

            //declare a list of errors to be used after returning to method-call.
            List<string> lstErrors =
                System.Web.HttpContext.Current.Session["ErrorLogForBeingUsedAsNonActionMethod"] as List<string>;

            //if the modelstate is not valid, it shows that "regularExpressionValidation" Has Failed.
            if (blnIsThisActionUsedAsNonActionMethodForUserCreation)
            {
                //validate email expression.
                if (new Infrastructure.RegexUtilities().IsValidEmail(createViewModel.Email) == false)
                {
                    //create "regular expression validation" error message.
                    lstErrors.Add(string.Format(Resources.Messages.RegularExpressionValidator, createViewModel.Email));

                    goto returnToMethodCall;
                }
            }

            if (ModelState.IsValid)
            {

                Guid guidAccountOrClassId =
                    Guid.Parse(TempData["AccountOrClassId"].ToString());

                TempData["AccountOrClassId"] = guidAccountOrClassId;

                bool blnIsUserNew = false;

                MvcEshop.Models.User oUser =
                MVC.User.GetExistingUsers()
                 .Where(current => current.Email == createViewModel.Email.ToLower())
                 .FirstOrDefault()
                 ;

                if (oUser == null)
                {
                    blnIsUserNew = true;
                }

                //email body
                string strEmailBody = null;

                //define the roleName, ItemToJoin, ItemToJoinTitle to use in email body
                string strRoleName = string.Empty;
                string strItemToJoin = string.Empty;
                string strItemToJoinTitle = string.Empty;

                string NotHashedPassword = string.Empty;

                //checks if the client has an User Account.
                //if No
                if (blnIsUserNew)
                {
                    oUser = new MvcEshop.Models.User();
                    //oUser.FirstName = createViewModel.FirstName;
                    //oUser.LastName = createViewModel.LastName;
                    oUser.FirstName = createViewModel.FirstName;
                    oUser.LastName = createViewModel.LastName;
                    oUser.Email = createViewModel.Email.ToLower();

                    NotHashedPassword = oUser.Password;
                    oUser.Password = Infrastructure.PasswordHash.CreateHash(oUser.Password);
                    //oUser.Password = System.Web.Security.Membership.GeneratePassword(8, 0);
                    //oUser.Password = Guid.NewGuid().ToString().Replace("-", "").Substring(1,8);
                    MVC.User.DFEntities.Users.Add(oUser);
                    MVC.User.DFEntities.SaveChanges();
                }

                //else
                //{
                //    ouser
                //}

                //build the email body

                //if the logged-in user is an "institute administrator" OR "Account Manager or System administrator" , 
                //then create a user with the role of "Instructor" and 
                //set roleName to "Instructor" OR then create a user with the role of "GroupUser"
                // and set roleName to "GroupUser".
                if (System.Convert.ToInt32
                    (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 3 ||
                    System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9
                    || System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
                {
                    // find the account
                    MvcEshop.Models.Account oAccount = MVC.User
                        .DFEntities.Accounts
                        .Where(current => current.AccountId == guidAccountOrClassId
                        && current.IsDeleted == false)
                        .FirstOrDefault();


                    //if the user creating the new user is an "Institute Administrator"
                    if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 3)
                    {
                        //add the instructor to the account
                        oAccount.Users.Add(oUser);
                        MVC.User.DFEntities.SaveChanges();

                        MyRoleProvider.AddUserToRole(oUser.Email, "Instructor");
                        strRoleName = Resources.DisplayNames.Instructor;

                    }

                    //if the user creating the new user is an "Account Manager" or "system administrator"
                    if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9 ||
                        System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
                    {
                        MvcEshop.Models.CreditCard oExistingCreditCard = oUser.CreditCards
                            .Where(current => current.AccountId == oAccount.AccountId)
                            .ToList()
                            .FirstOrDefault();

                        //if the user to be added has already a creditcard for this account.
                        if (oExistingCreditCard != null)
                        {

                            if (blnIsThisActionUsedAsNonActionMethodForUserCreation)
                            {
                                lstErrors.Add(string.Format("{0}: {1}", string.Format(Resources.Messages.UserHasBeenAlreadyAddedToThisAccount,
                            Resources.DisplayNames.Email, Resources.DisplayNames.Account), createViewModel.Email));

                                goto returnToMethodCall;
                            }

                            ModelState.AddModelError
                            ("", string.Format(Resources.Messages.UserHasBeenAlreadyAddedToThisAccount,
                            Resources.DisplayNames.Email, Resources.DisplayNames.Account));

                            ViewBag.AccountOrClassId =
                    Guid.Parse(TempData["AccountOrClassId"].ToString());

                            TempData["AccountOrClassId"] = ViewBag.AccountOrClassId;

                            return View(createViewModel);
                        }

                        MvcEshop.Models.CreditCard oCreditCard =
                            new MvcEshop.Models.CreditCard();

                        oCreditCard.Account = oAccount;
                        oCreditCard.User = oUser;

                        if (blnIsThisActionUsedAsNonActionMethodForUserCreation)
                        {
                            oCreditCard.ExpirationDate = Infrastructure.Convert.ToPersian(System.DateTime.Now);

                        }
                        else
                        {
                            oCreditCard.ExpirationDate = createViewModel.ExpirationDate;
                        }

                        oCreditCard.AllocatedVolume = createViewModel.AllocatedVolume;

                        //add the new creditcard to database
                        MVC.User.DFEntities.CreditCards.Add(oCreditCard);
                        MVC.User.DFEntities.SaveChanges();

                        MyRoleProvider.AddUserToRole(oUser.Email, "GroupUser");
                        strRoleName = Resources.DisplayNames.GroupUser;

                    }

                    strItemToJoin = Resources.DisplayNames.Account;
                    strItemToJoinTitle = oAccount.AccountTitle;

                }

                //if the logged-in user is an Instructor, 
                //then create a user with the role of "Student" and 
                //set roleName to "Student".
                if (System.Convert.ToInt32
                    (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
                {
                    // find the Class
                    MvcEshop.Models.Class oClass = MVC.User
                        .DFEntities.Classes
                        .Where(current => current.ClassId == guidAccountOrClassId
                        && current.IsDeleted == false)
                        .FirstOrDefault();

                    //add the student to the class
                    oClass.Students.Add(oUser);
                    MVC.User.DFEntities.SaveChanges();

                    MyRoleProvider.AddUserToRole(oUser.Email, "Student");
                    strRoleName = Resources.DisplayNames.Student;
                    strItemToJoin = Resources.DisplayNames.Class;
                    strItemToJoinTitle = oClass.Title;
                }

                //check whether the client has a user account
                //if no
                if (blnIsUserNew)
                {

                    string strDomain = DFEntities.Settings
                                            .Select(current => current.Domain)
                                            .FirstOrDefault();

                    string strActivationLink = @"<a href='" + strDomain +
                        "/User/ActivateUserByActiveCode?activeCode="
                        + oUser.ActiveCode +
                                "'>پیوند فعال‌سازی</a>";


                    strEmailBody = string.Format
                               (Resources.Messages.WelcomeToUserWhoIsNotRegistered,
                               strRoleName, strItemToJoin,
                               strItemToJoinTitle,
                               Resources.DisplayNames.Email + Resources.DisplayNames.JunctionLetter,
                               Resources.DisplayNames.Password + Resources.DisplayNames.JunctionLetter,
                               NotHashedPassword, Resources.DisplayNames.Profile, strActivationLink);

                }


            //if Yes
                else
                {
                    //build the email body
                    strEmailBody = string.Format
                        (Resources.Messages.WelcomeToUserWhoIsRegistered,
                        strRoleName, strItemToJoin,
                        strItemToJoinTitle);
                }


                strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strEmailBody);

                /*notify the client and send them the join password and
                the temporary password(if he is introduced) for login.*/

                //Must Be Uncommented

                Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    oUser.Email, "به همانندجو خوش آمدید", strEmailBody, null);

                //if the current action is being used as a non-action method, then return to method-call instead of redirectToAction.
                if (blnIsThisActionUsedAsNonActionMethodForUserCreation)
                {
                    goto returnToMethodCall;
                }


                return RedirectToAction(MVC.User.Index(guidAccountOrClassId));
            }



            return View(createViewModel);

        //in the state of using this action as a method, after filling the errorList, the programm goes to following label.
        returnToMethodCall:

            System.Web.HttpContext.Current.Session["ErrorLogForBeingUsedAsNonActionMethod"] = lstErrors;

            return null;

        }





        [HttpGet]
        public virtual ActionResult CompleteInfo(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["UserId"] = id.Value;

            User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.UserId == id.Value)
                .FirstOrDefault()
                ;

            if (oUser == null)
            {
                return HttpNotFound();
            }

            ViewBag.GenderIdList = new SelectList
                (DFEntities.Genders, "GenderId", "Description");

            ViewBag.SecretQuestionIdList = new SelectList
                (DFEntities.SecretQuestions, "SecretQuestionId", "Question");


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CompleteInfo(MvcEshop.ViewModels.User.CompleteInfoViewModel completeInfoViewModel)
        {

            Guid guidUserId = Guid.Parse(TempData["UserId"].ToString());

            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.UserId == guidUserId)
                .FirstOrDefault()
                ;

            if (oUser == null)
            {
                return HttpNotFound();
            }

            if (completeInfoViewModel.SendSms == true && string.IsNullOrEmpty(completeInfoViewModel.CellPhoneNumber))
            {
                ModelState.AddModelError
                       ("", string.Format(Resources.Messages.RequiredFieldValidator,
                       Resources.DisplayNames.CellPhoneNumber));
            }

            if (ModelState.IsValid)
            {

                oUser.FirstName = completeInfoViewModel.FirstName;
                oUser.LastName = completeInfoViewModel.LastName;
                oUser.FatherName = completeInfoViewModel.FatherName;
                oUser.NationalCode = completeInfoViewModel.NationalCode;
                oUser.GenderId = completeInfoViewModel.GenderId;
                oUser.SecretQuestionId = completeInfoViewModel.SecretQuestionId;
                oUser.AdvertisementByIrandoc = completeInfoViewModel.AdvertisementByIrandoc;
                oUser.AdvertisementByOtherOrgs = completeInfoViewModel.AdvertisementByOtherOrgs;
                oUser.Password = Infrastructure.PasswordHash.CreateHash(completeInfoViewModel.Password);
                oUser.SecretAnswer = Infrastructure.PasswordHash.CreateHash(completeInfoViewModel.SecretAnswer);
                oUser.BirthDate = Infrastructure.Convert.ToMiladi(completeInfoViewModel.BirthDate);
                oUser.SendSms = completeInfoViewModel.SendSms;
                oUser.CellPhoneNumber = completeInfoViewModel.CellPhoneNumber;

                bool blnIsProfileCompleted = true;

                if (oUser.IsProfileCompleted == false)
                {
                    //will be used for the next if block
                    blnIsProfileCompleted = false;

                    oUser.IsProfileCompleted = true;
                    MyRoleProvider.RemoveUserFromRole(oUser.Email, "NotCompletedProfile");
                }

                MVC.User.UpdateUser(oUser);
                //DFEntities.Entry(oUser).State = EntityState.Modified;
                //DFEntities.SaveChanges();

                if (blnIsProfileCompleted == false)
                {
                    string strMessage = string.Format
                        ("تکمیل اطلاعات {0} شما با موفقیت انجام شد.", Resources.DisplayNames.Profile);
                    //FormsAuthentication.SetAuthCookie(oUser.Email, true);
                    return RedirectToAction(MVC.User.Message(strMessage));

                }


                return RedirectToAction(MVC.Home.Index());
            }


            ViewBag.GenderIdList = new SelectList
                (DFEntities.Genders, "GenderId", "Description", completeInfoViewModel.GenderId);

            ViewBag.SecretQuestionIdList = new SelectList
               (DFEntities.SecretQuestions, "SecretQuestionId", "Question", completeInfoViewModel.SecretQuestionId);

            return View(completeInfoViewModel);

        }



        [Authorize(Roles = "SystemAdministrator, InstituteAdministrator, Instructor")]
        // GET: User/Delete/5
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User oUser = GetUserByUserId(id.Value);

            if (oUser == null)
            {
                return HttpNotFound();
            }

            //check if the current role of the user is not either "System Administrator" or "Programmer".
            //if (System.Convert.ToInt32
            //    (System.Web.HttpContext.Current.Session["RoleId"].ToString()) > 2)
            //{

            if (TempData["AccountOrClassId"]!=null)
            {
                ViewBag.AccountOrClassId =
                                Guid.Parse(TempData["AccountOrClassId"].ToString());

                TempData["AccountOrClassId"] = ViewBag.AccountOrClassId;
                
            }
            //}

            return View(oUser);

        }


        [Authorize(Roles = "SystemAdministrator, InstituteAdministrator, Instructor, AccountManager")]
        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.UserId == id)
                .FirstOrDefault();




            //DFEntities.Entry(oUser).State = EntityState.Modified;
            //DFEntities.SaveChanges();

            //check if the current role of the user is not either "System Administrator" or "Programmer".
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) > 2)
            {

                //if the current role of user is "Account Manager"
                if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
                {
                    Guid guidAccountId = Guid.Parse(TempData["AccountOrClassId"].ToString());

                    TempData["AccountOrClassId"] = guidAccountId;

                    Account oAccount = MVC.Account.GetExistingAccounts()
                        .Where(current => current.AccountId == guidAccountId)
                        .ToList()
                        .FirstOrDefault();

                    CreditCard oCreditCard = oAccount.CreditCards
                        .Where(current => current.UserId == oUser.UserId)
                        .ToList()
                        .FirstOrDefault();

                    oAccount.CreditCards.Remove(oCreditCard);
                    oUser.CreditCards.Remove(oCreditCard);
                    //MVC.User.UpdateUser(oUser);
                    MVC.User.DFEntities.Entry(oAccount).State = EntityState.Modified;
                    MVC.User.DFEntities.Entry(oUser).State = EntityState.Modified;

                    MVC.User.DFEntities.SaveChanges();
                }

                return RedirectToAction
                    (MVC.User.Index(Guid.Parse(TempData["AccountOrClassId"].ToString())));
            }

            oUser.IsDeleted = true;
            MVC.User.UpdateUser(oUser);



            return RedirectToAction
                (MVC.User.AllUsers());
        }

        [HttpGet]
        public virtual ActionResult CheckEmailUniqueness(string email)
        {
            bool blnResult = true;

            //checks uniqueness as we are editing
            if (TempData["UserId"] != null)
            {
                Guid guidUserId = Guid.Parse(TempData["UserId"].ToString());

                TempData["UserId"] = guidUserId;

                MvcEshop.Models.User oCurrentUser = MVC.User.GetExistingUsers()
                    .Where(current => current.UserId == guidUserId)
                    .FirstOrDefault()
                    ;

                //find the user by the specific email to check if any user exists with this email
                MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                    .Where(current => current.Email == email)
                    .FirstOrDefault()
                    ;

                //check if any user exists with this email and if this email is not the current-user's email
                if (oUser != null && oCurrentUser.Email != email)
                {
                    blnResult = false;
                }
            }

            //checks uniqueness as we are creating a new profile
            else
            {
                //find the user by the specific email to check if any user exists with this email
                MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                    .Where(current => current.Email == email)
                    .FirstOrDefault()
                    ;

                //check if any user exists with this email and if this email is not the current-user's email
                if (oUser != null)
                {
                    blnResult = false;
                }
            }

            return (Json(blnResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
        }

        [HttpGet]
        public virtual ActionResult CheckPasswordCorrectness(string previousPassword)
        {
            bool blnResult = true;

            System.Guid guidUserId = GetUserIdByEmail(User.Identity.Name);

            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.UserId == guidUserId)
                .FirstOrDefault()
                ;

            if (Infrastructure.PasswordHash.ValidatePassword(previousPassword, oUser.Password) == false)
            {
                blnResult = false;
            }

            return (Json(blnResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
        }

        [ValidateInput(false)]
        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login
            (MvcEshop.ViewModels.User.LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                    .Where(current => current.Email == loginViewModel.Email.ToLower())
                    .FirstOrDefault();




                if (oUser != null && Infrastructure.PasswordHash.ValidatePassword(loginViewModel.Password, oUser.Password) == true)
                {
                    if (oUser.IsActive == false)
                    {
                        ModelState.AddModelError
                            ("", string.Format(Resources.Messages.ProfileIsInActive,
                            Resources.DisplayNames.Profile));

                        return View(loginViewModel);
                    }

                    if (oUser.IsLocked.Value)
                    {
                        ModelState.AddModelError
                            ("", "حساب کاربری مورد نظر تا اطلاع ثانوی مسدود می باشد.");

                        return View(loginViewModel);
                    }

                    FormsAuthentication.SetAuthCookie(oUser.Email, loginViewModel.RememberMe);

                    //give the user a default role from it's existing roles

                    Infrastructure.MyRoleProvider oMyRoleProvider =
                                 new Infrastructure.MyRoleProvider();

                    var varRoles = oMyRoleProvider.GetRolesForNavBarDropDownList(oUser.Email);

                    if (System.Web.HttpContext.Current.Session["RoleId"] == null)
                    {
                        System.Web.HttpContext.Current.Session["RoleId"] = varRoles.FirstOrDefault().RoleId;
                    }



                    // check whether the user profile is completed.
                    if (oUser.IsProfileCompleted != true)
                    {

                        MyRoleProvider.AddUserToRole(oUser.Email, "NotCompletedProfile");

                        return RedirectToAction(MVC.User.CompleteInfo(oUser.UserId));

                    }

                    return RedirectToLocal(returnUrl);

                }
                else
                {

                    ModelState.AddModelError("", "کاربری با مشخصات ذیل یافت نشد");

                    MvcEshop.Models.User oExistingUser = MVC.User.GetExistingUsers()
                    .Where(current => current.Email == loginViewModel.Email.ToLower())
                    .FirstOrDefault();

                    //check if the user exists but the password is not correct
                    if (oExistingUser != null)
                    {
                        oExistingUser.NumberOfWrongPasswords++;

                        MVC.User.UpdateUser(oExistingUser);
                        //DFEntities.Entry(oExistingUser).State = EntityState.Modified;
                        //DFEntities.SaveChanges();
                    }

                }
            }

            // If we got this far, something failed, redisplay form
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            //Refresh RoleId for the next Login
            //System.Web.HttpContext.Current.Session["RoleId"] = null;

            //this method clears all session level variable.
            Session.Clear();

            //this method cause the session_end to take place.
            Session.Abandon();


            System.Web.SessionState.SessionIDManager oSessionIDManager =
                new System.Web.SessionState.SessionIDManager();

            string strNewSessionId =
                oSessionIDManager.CreateSessionID(System.Web.HttpContext.Current);

            bool blnRedirected;
            bool blnCookieAdded;

            // this cause a new sessionId to be created after logout.
            oSessionIDManager.SaveSessionID
                (System.Web.HttpContext.Current, strNewSessionId, out blnRedirected, out blnCookieAdded);



            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                ////if the current role of the user is "InstituteAdministrator".
                //if (System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 3)
                //{
                //    return RedirectToAction(MVC.Account.Index());
                //}

                return RedirectToAction(MVC.User.RedirectToAppropriateActionBasedOnCurrentRole());
            }
        }

        [HttpGet]
        public virtual ActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ChangePassword
            (MvcEshop.ViewModels.User.ChangePasswordViewModel changePasswordViewModel)
        {

            MvcEshop.Models.User oUser =
                MVC.User.GetUserByUsername(User.Identity.Name);

            if (oUser == null)
            {
                return HttpNotFound();
            }

            if (Infrastructure.PasswordHash.ValidatePassword(changePasswordViewModel.PreviousPassword, oUser.Password) == false)
            {
                ModelState.AddModelError
                    ("PreviousPassword", Resources.Messages.FieldDataIsNotCorrect);
            }

            if (ModelState.IsValid)
            {
                oUser.Password = Infrastructure.PasswordHash.CreateHash(changePasswordViewModel.NewPassword);

                MVC.User.UpdateUser(oUser);
                //DFEntities.Entry(oUser).State = EntityState.Modified;
                //DFEntities.SaveChanges();

                return RedirectToAction(MVC.Home.Index());
            }

            return View(changePasswordViewModel);
        }


        [NonAction]
        public System.Guid GetUserIdByEmail(string email)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.Email == email)
                .FirstOrDefault()
                ;

            return (oUser.UserId);
        }

        [NonAction]
        public Guid FindUserByUsername(string username)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.Email == username)
               .FirstOrDefault()
               ;

            return oUser.UserId;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DFEntities.Dispose();
            }
            base.Dispose(disposing);
        }

        public virtual ActionResult RedirectToAppropriateActionBasedOnCurrentRole()
        {

            if (Request["RoleId"] != null)
            {
                System.Web.HttpContext.Current.Session["RoleId"] = Request["RoleId"].ToString();
            }

            //checks whether the current role of the user is "Programmer"
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 1)
            {
                return RedirectToAction(MVC.Settings.Edit());
            }

            //checks whether the current role of the user is "SystemAdministrator"
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 2)
            {
                return RedirectToAction(MVC.Account.Index());
            }

            //checks whether the current role of the user is "institute administrator" or "Account Manager"
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 3 ||
                System.Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                return RedirectToAction(MVC.Account.Index());
            }

            //checks whether the current role of the user is "instructor"
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
            {
                return RedirectToAction(MVC.Class.Index());
            }

            //checks whether the current role of the user is "Student"
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 5)
            {
                return RedirectToAction(MVC.Class.Index());
            }

            //checks whether the current role of the user is "GroupUser" or "FreeUser".
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 7 || System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 8)
            {
                return RedirectToAction(MVC.Assignment.PreviousRequests(MVC.User.GetUserIdByEmail(User.Identity.Name)));
            }

            return RedirectToAction(MVC.Home.Index());
        }

        [HttpGet]
        public virtual ActionResult ChangeEmailAddress()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ChangeEmailAddress
            (MvcEshop.ViewModels.User.ChangeEmailAddressViewModel changeEmailAddressViewModel)
        {

            MvcEshop.Models.User oUser =
                MVC.User.GetUserByUsername(User.Identity.Name);

            if (oUser == null)
            {
                return HttpNotFound();
            }


            if (ModelState.IsValid)
            {

                //server_side validation for Email Uniqueness
                //string strEmail = MVC.User.GetExistingUsers()
                //    .Select(current => current.Email)
                //    .Where(current => current.Email == oUser.Email)
                //    .FirstOrDefault()
                //    ;

                //if (string.IsNullOrEmpty(strEmail)==false)
                //{
                //    ModelState.AddModelError("", string.Format
                //        (Resources.Messages.UniquenessValidator, Resources.DisplayNames.NewEmail));
                //}



                oUser.NewEmailConfirmationCode = System.Guid.NewGuid();

                MVC.User.UpdateUser(oUser);
                //DFEntities.Entry(oUser).State = EntityState.Modified;
                //DFEntities.SaveChanges();

                string strDomain = DFEntities.Settings
                    .Select(current => current.Domain)
                    .FirstOrDefault();

                string strConfirmationLink = @"<a href='" + strDomain +
                    "/User/ConfirmNewEmail?newEmailConfirmationCode="
                    + oUser.NewEmailConfirmationCode +
                    "&newEmail=" + changeEmailAddressViewModel.NewEmail +
                            "'>" + Resources.DisplayNames.ConfirmationLink + "</a>";

                string strEmailBody = string.Format
                        (Resources.Messages.EmailMessageForConfirmationLink,
                        Resources.DisplayNames.Email, strConfirmationLink);

                strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strEmailBody);

                //Must Be Uncommented
                //send confirmation link by email
                Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    changeEmailAddressViewModel.NewEmail,
                    Resources.DisplayNames.Confirmation + Resources.DisplayNames.NewEmail,
                    strEmailBody, null);


                string strMessage = string.Format
                    (Resources.Messages.ConfirmationLinkWasSentToTheEmail,
                    Resources.DisplayNames.NewEmail,
                    Resources.DisplayNames.NewEmail + Resources.DisplayNames.JunctionLetter,
                    Resources.DisplayNames.Email);


                //return View("~/Views/Shared/Message.aspx",strMessage);
                return RedirectToAction(MVC.User.Message(strMessage));
            }

            return View(changeEmailAddressViewModel);
        }

        public virtual ActionResult ConfirmNewEmail
            (System.Guid newEmailConfirmationCode, string newEmail)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.NewEmailConfirmationCode == newEmailConfirmationCode
               && current.IsDeleted == false)
               .FirstOrDefault()
               ;

            string strMessage = string.Empty;

            if (oUser == null)
            {
                strMessage = string.Format
                    (Resources.Messages.NewEmailConfirmationCodeIsNotCorrect,
                    Resources.DisplayNames.Email);
            }

            else
            {
                oUser.Email = newEmail;
                oUser.NewEmailConfirmationCode = System.Guid.NewGuid();

                MVC.User.UpdateUser(oUser);
                //DFEntities.Entry(oUser).State = EntityState.Modified;
                //DFEntities.SaveChanges();

                strMessage = string.Format(Resources.Messages.ChangeDoneSuccessfully,
                    Resources.DisplayNames.Email);

                FormsAuthentication.SignOut();

                //Refresh RoleId for the next Login
                //System.Web.HttpContext.Current.Session["RoleId"] = null;

                //this method clears all session level variable.
                Session.Clear();

                //this method cause the session_end to take place.
                Session.Abandon();


                System.Web.SessionState.SessionIDManager oSessionIDManager =
                    new System.Web.SessionState.SessionIDManager();

                string strNewSessionId =
                    oSessionIDManager.CreateSessionID(System.Web.HttpContext.Current);

                bool blnRedirected;
                bool blnCookieAdded;

                // this cause a new sessionId to be created after logout.
                oSessionIDManager.SaveSessionID
                    (System.Web.HttpContext.Current, strNewSessionId, out blnRedirected, out blnCookieAdded);

            }

            return RedirectToAction(MVC.User.Message(strMessage));
        }

        public virtual ActionResult ActivateUserByActiveCode
           (System.Guid activeCode)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.ActiveCode == activeCode
               && current.IsDeleted == false)
               .FirstOrDefault()
               ;

            string strMessage = string.Empty;

            if (oUser == null)
            {
                strMessage = Resources.Messages.ActiveCodeIsNotCorrect;
            }

            else
            {
                oUser.ActiveCode = System.Guid.NewGuid();
                oUser.IsActive = true;


                MVC.User.UpdateUser(oUser);

                strMessage = string.Format(Resources.Messages.ProfileActivationDoneSuccessfully,
                    Resources.DisplayNames.Profile, @"<a href='/Home/Index'>ورود به سامانه</a>");

                FormsAuthentication.SignOut();

                //Refresh RoleId for the next Login
                //System.Web.HttpContext.Current.Session["RoleId"] = null;

                //this method clears all session level variable.
                Session.Clear();

                //this method cause the session_end to take place.
                Session.Abandon();


                System.Web.SessionState.SessionIDManager oSessionIDManager =
                    new System.Web.SessionState.SessionIDManager();

                string strNewSessionId =
                    oSessionIDManager.CreateSessionID(System.Web.HttpContext.Current);

                bool blnRedirected;
                bool blnCookieAdded;

                // this cause a new sessionId to be created after logout.
                oSessionIDManager.SaveSessionID
                    (System.Web.HttpContext.Current, strNewSessionId, out blnRedirected, out blnCookieAdded);

            }

            return RedirectToAction(MVC.User.Message(strMessage));
        }


        [ValidateInput(false)]
        public virtual ActionResult Message(string message)
        {
            return View((object)message);
        }


        [NonAction]
        //get users who are not deleted
        public List<MvcEshop.Models.User> GetExistingUsers()
        {

            List<MvcEshop.Models.User> lstUsers = new List<Models.User>();

            lstUsers = DFEntities.Users
                .Where(current => current.IsDeleted == false)
                .OrderByDescending(current => current.CreateDate)
                .ToList()
                ;

            return lstUsers;
        }


        [NonAction]
        public MvcEshop.Models.Role GetRoleByRoleTitle(string roleTitle)
        {
            MvcEshop.Models.Role oRole = DFEntities.Roles
                .Where(current => current.RoleTitle == roleTitle)
                .FirstOrDefault()
                ;

            return oRole;
        }

        [NonAction]
        public MvcEshop.Models.User GetUserByUsername(string username)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.Email == username.ToLower())
               .FirstOrDefault()
               ;

            return oUser;
        }

        [NonAction]
        public string[] GetRoleTitlesByUsername(string username)
        {

            MvcEshop.Models.User oUser =
                MVC.User.GetUserByUsername(username);

            string[] varRoles = null;

            if (oUser != null)
            {
                varRoles =
                       (from r in oUser.Roles where r!=null select r.RoleTitle).ToArray();
            }

            return varRoles;
        }


        [NonAction]
        public void UpdateUser(MvcEshop.Models.User user)
        {
            //DFEntities.Entry(user).State = EntityState.Added;
            DFEntities.Entry(user).State = EntityState.Modified;
            DFEntities.SaveChanges();
        }

        //[NonAction]
        public virtual ActionResult Activate(Guid id)
        {
            MvcEshop.Models.User oUser = MVC.User.FindoUserByUserId(id);
            oUser.IsActive = true;
            MVC.User.UpdateUser(oUser);

            //check if the current role of the user is not either "System Administrator" or "Programmer".
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) > 2)
            {
                Guid guidId =
                        Guid.Parse(TempData["AccountOrClassId"].ToString());

                TempData["AccountOrClassId"] = guidId;

                return RedirectToAction(MVC.User.Index(guidId));
            }

            return RedirectToAction(MVC.User.AllUsers());
        }

        //[NonAction]
        public virtual ActionResult Deactivate(Guid id)
        {
            MvcEshop.Models.User oUser = MVC.User.FindoUserByUserId(id);
            oUser.IsActive = false;
            MVC.User.UpdateUser(oUser);

            //check if the current role of the user is not either "System Administrator" or "Programmer".
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) > 2)
            {
                Guid guidId =
                        Guid.Parse(TempData["AccountOrClassId"].ToString());
                TempData["AccountOrClassId"] = guidId;

                return RedirectToAction(MVC.User.Index(guidId));
            }

            return RedirectToAction(MVC.User.AllUsers());
        }

        [NonAction]
        public MvcEshop.Models.User FindoUserByUserId(Guid id)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
                .Where(current => current.UserId == id)
                .FirstOrDefault();

            return oUser;
        }

        public virtual ActionResult CreateProfile()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult CreateInstructorProfile()
        {
            ViewBag.GenderId = new SelectList
                (DFEntities.Genders, "GenderId", "Description", 1);

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateInstructorProfile
            (MvcEshop.ViewModels.User.CreateInstructorProfileViewModel
            createInstructorProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                //find the account
                MvcEshop.Models.Account oAccount = MVC.Account.GetExistingAccounts()
                    .Where(current => current.AccountId == createInstructorProfileViewModel.AccountId)
                    .FirstOrDefault();

                if (oAccount != null &&
                    oAccount.JoinPassword == createInstructorProfileViewModel.JoinPassword)
                {



                    //create a new user profile for the instructor
                    MvcEshop.Models.User oUser = new MvcEshop.Models.User();

                    oUser.FirstName = createInstructorProfileViewModel.FirstName;
                    oUser.LastName = createInstructorProfileViewModel.LastName;
                    oUser.Password = createInstructorProfileViewModel.Password;
                    //oUser.SecretQuestion = createInstructorProfileViewModel.SecretQuestion;
                    //oUser.SecretAnswer = createInstructorProfileViewModel.SecretAnswer;
                    oUser.Email = createInstructorProfileViewModel.Email.ToLower();
                    oUser.GenderId = createInstructorProfileViewModel.GenderId;
                    oUser.IsProfileCompleted = true;
                    //add the instructor to the system.
                    //DFEntities.Users.Add(oUser);
                    //DFEntities.SaveChanges();
                    //MVC.User.UpdateUser(oUser);



                    //add the instructor to that account
                    oAccount.Users.Add(oUser);
                    MVC.Account.DFEntities.SaveChanges();

                    MyRoleProvider.AddUserToRole(oUser.Email, "Instructor");

                    string strDomain = DFEntities.Settings
                        .Select(current => current.Domain)
                        .FirstOrDefault();

                    string strActivationLink = @"<a href='" + strDomain +
                        "/User/ActivateUserByActiveCode?activeCode="
                        + oUser.ActiveCode +
                                "'>پیوند فعال‌سازی</a>";

                    string strEmailBody = string.Format
                            (Resources.Messages.EmailMessageForProfileActivation,
                            Resources.DisplayNames.Profile, strActivationLink);

                    strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                    DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                    strEmailBody);

                    //Must Be Uncommented
                    //send activation link by email
                    Infrastructure.Communication.SendEmail
                        (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                        Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                        createInstructorProfileViewModel.Email,
                        string.Format("پیوند {0}", Resources.DisplayNames.Activate), strEmailBody, null);


                    string strMessage = string.Format
                        (Resources.Messages.ProfileCreatedSuccessfully,
                        Resources.DisplayNames.Profile, Resources.DisplayNames.Email);

                    return RedirectToAction(MVC.User.Message(strMessage));
                }

                else
                {
                    ModelState.AddModelError("", string.Format
                        ("{0} با این {1} و {2} وجود ندارد.", Resources.DisplayNames.Account,
                        Resources.DisplayNames.Id, Resources.DisplayNames.JoinPassword));


                }
                //oUser.Accounts.Add(oAccount);
                //MVC.User.UpdateUser(oUser);

            }

            else
            {
                if (this.ModelState["AccountId"].Errors.Count == 1
                    && this.ModelState["AccountId"].Errors[0].ErrorMessage.Contains
                    (string.Format("is not valid for {0}", Resources.DisplayNames.AccountId)))
                {
                    this.ModelState["AccountId"].Errors.Clear();

                    this.ModelState["AccountId"].Errors.Add
                            (string.Format(Resources.Messages.RegularExpressionValidator,
                            Resources.DisplayNames.AccountId));
                }
            }

            ViewBag.GenderId = new SelectList
                (DFEntities.Genders, "GenderId", "Description",
                createInstructorProfileViewModel.GenderId);

            return View(createInstructorProfileViewModel);
        }


        [HttpGet]
        public virtual ActionResult CreateStudentProfile()
        {
            ViewBag.GenderId = new SelectList
                (DFEntities.Genders, "GenderId", "Description", 1);

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateStudentProfile
            (MvcEshop.ViewModels.User.CreateStudentProfileViewModel
            createStudentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                //find the class
                MvcEshop.Models.Class oClass = MVC.Class.DFEntities.Classes
                    .Where(current => current.ClassId ==
                     createStudentProfileViewModel.ClassId
                     && current.IsDeleted == false)
                    .FirstOrDefault();

                if (oClass != null &&
                    oClass.EnrollmentPassword == createStudentProfileViewModel.EnrollmentPassword)
                {



                    //create a new user profile for the instructor
                    MvcEshop.Models.User oUser = new MvcEshop.Models.User();

                    oUser.FirstName = createStudentProfileViewModel.FirstName;
                    oUser.LastName = createStudentProfileViewModel.LastName;
                    oUser.Password = createStudentProfileViewModel.Password;
                    //oUser.SecretQuestion = createStudentProfileViewModel.SecretQuestion;
                    //oUser.SecretAnswer = createStudentProfileViewModel.SecretAnswer;
                    oUser.Email = createStudentProfileViewModel.Email.ToLower();
                    oUser.GenderId = createStudentProfileViewModel.GenderId;
                    oUser.IsProfileCompleted = true;
                    //add the instructor to the system.
                    //DFEntities.Users.Add(oUser);
                    //DFEntities.SaveChanges();
                    //MVC.User.UpdateUser(oUser);



                    //add the Student to that class
                    oClass.Students.Add(oUser);
                    MVC.Class.DFEntities.SaveChanges();

                    MyRoleProvider.AddUserToRole(oUser.Email, "Student");

                    string strDomain = DFEntities.Settings
                        .Select(current => current.Domain)
                        .FirstOrDefault();

                    string strActivationLink = @"<a href='" + strDomain +
                        "/User/ActivateUserByActiveCode?activeCode="
                        + oUser.ActiveCode +
                                "'>پیوند فعال‌سازی</a>";

                    string strEmailBody = string.Format
                            (Resources.Messages.EmailMessageForProfileActivation,
                            Resources.DisplayNames.Profile, strActivationLink);

                    strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                    DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                    strEmailBody);

                    //Must Be Uncommented
                    //send activation link by email
                    Infrastructure.Communication.SendEmail
                        (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                        Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                        createStudentProfileViewModel.Email,
                        string.Format("پیوند {0}", Resources.DisplayNames.Activate), strEmailBody, null);


                    string strMessage = string.Format
                        (Resources.Messages.ProfileCreatedSuccessfully,
                        Resources.DisplayNames.Profile, Resources.DisplayNames.Email);

                    return RedirectToAction(MVC.User.Message(strMessage));
                }

                else
                {
                    ModelState.AddModelError("", string.Format
                        ("{0} با این {1} و {2} وجود ندارد.", Resources.DisplayNames.Class,
                        Resources.DisplayNames.Id, Resources.DisplayNames.EnrollmentPassword));


                }
                //oUser.Accounts.Add(oAccount);
                //MVC.User.UpdateUser(oUser);

            }

            else
            {
                if (this.ModelState["ClassId"].Errors.Count == 1
                    && this.ModelState["ClassId"].Errors[0].ErrorMessage.Contains
                    (string.Format("is not valid for {0}", Resources.DisplayNames.ClassId)))
                {
                    this.ModelState["ClassId"].Errors.Clear();

                    this.ModelState["ClassId"].Errors.Add
                            (string.Format(Resources.Messages.RegularExpressionValidator,
                            Resources.DisplayNames.ClassId));
                }
            }


            ViewBag.GenderId = new SelectList
                (DFEntities.Genders, "GenderId", "Description",
                createStudentProfileViewModel.GenderId);

            return View(createStudentProfileViewModel);
        }


        [Authorize()]
        [HttpGet]
        public virtual ActionResult JoinAccount()
        {

            return View();
        }


        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult JoinAccount
            (MvcEshop.ViewModels.User.JoinAccountViewModel
            joinAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                //find the account
                MvcEshop.Models.Account oAccount = MVC.Account.GetExistingAccounts()
                    .Where(current => current.AccountId == joinAccountViewModel.AccountId)
                    .FirstOrDefault();

                if (oAccount != null &&
                    oAccount.JoinPassword == joinAccountViewModel.JoinPassword)
                {

                    //find the current user
                    MvcEshop.Models.User oUser =
                        MVC.Account.DFEntities.Users
                        .Where(current => current.Email == User.Identity.Name
                        && current.IsDeleted == false)
                        .FirstOrDefault()
                        ;

                    //add the instructor to that account
                    oAccount.Users.Add(oUser);
                    MVC.Account.DFEntities.SaveChanges();

                    //if the user is not an Instructor
                    if (MyRoleProvider.IsUserInRole(oUser.Email, "Instructor") == false)
                    {
                        MyRoleProvider.AddUserToRole(oUser.Email, "Instructor");
                    }

                    string strMessage = string.Format
                        (Resources.Messages.JoinedAccountSuccessfully,
                        Resources.DisplayNames.Instructor, Resources.DisplayNames.Account,
                        oAccount.AccountTitle);

                    return RedirectToAction(MVC.User.Message(strMessage));
                }

                else
                {
                    ModelState.AddModelError("", string.Format
                        ("{0} با این {1} و {2} وجود ندارد.", Resources.DisplayNames.Account,
                        Resources.DisplayNames.Id, Resources.DisplayNames.JoinPassword));


                }
                //oUser.Accounts.Add(oAccount);
                //MVC.User.UpdateUser(oUser);

            }

            else
            {
                if (this.ModelState["AccountId"].Errors.Count == 1
                    && this.ModelState["AccountId"].Errors[0].ErrorMessage.Contains
                    (string.Format("is not valid for {0}", Resources.DisplayNames.AccountId)))
                {
                    this.ModelState["AccountId"].Errors.Clear();

                    this.ModelState["AccountId"].Errors.Add
                            (string.Format(Resources.Messages.RegularExpressionValidator,
                            Resources.DisplayNames.AccountId));
                }
            }

            return View(joinAccountViewModel);
        }



        [Authorize()]
        [HttpGet]
        public virtual ActionResult EnrollInClass()
        {

            return View();
        }


        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EnrollInClass
            (MvcEshop.ViewModels.User.EnrollInClassViewModel
            enrollInClassViewModel)
        {
            if (ModelState.IsValid)
            {
                //find the class
                MvcEshop.Models.Class oClass = MVC.User.DFEntities.Classes
                    .Where(current => current.ClassId == enrollInClassViewModel.ClassId
                    && current.IsDeleted == false)
                    .FirstOrDefault();

                if (oClass != null &&
                    oClass.EnrollmentPassword == enrollInClassViewModel.EnrollmentPassword)
                {

                    //find the current user
                    MvcEshop.Models.User oUser =
                        MVC.User.DFEntities.Users
                        .Where(current => current.Email == User.Identity.Name
                        && current.IsDeleted == false)
                        .FirstOrDefault()
                        ;

                    //add the student to that class
                    oClass.Students.Add(oUser);
                    MVC.User.DFEntities.SaveChanges();

                    //if the user is not an Student
                    if (MyRoleProvider.IsUserInRole(oUser.Email, "Student") == false)
                    {
                        MyRoleProvider.AddUserToRole(oUser.Email, "Student");
                    }

                    string strMessage = string.Format
                        (Resources.Messages.JoinedAccountSuccessfully,
                        Resources.DisplayNames.Student, Resources.DisplayNames.Class,
                        oClass.Title);

                    return RedirectToAction(MVC.User.Message(strMessage));
                }

                else
                {
                    ModelState.AddModelError("", string.Format
                        ("{0} با این {1} و {2} وجود ندارد.", Resources.DisplayNames.Class,
                        Resources.DisplayNames.Id, Resources.DisplayNames.EnrollmentPassword));


                }
                //oUser.Accounts.Add(oAccount);
                //MVC.User.UpdateUser(oUser);

            }

            else
            {
                if (this.ModelState["ClassId"].Errors.Count == 1
                    && this.ModelState["ClassId"].Errors[0].ErrorMessage.Contains
                    (string.Format("is not valid for {0}", Resources.DisplayNames.ClassId)))
                {
                    this.ModelState["ClassId"].Errors.Clear();

                    this.ModelState["ClassId"].Errors.Add
                            (string.Format(Resources.Messages.RegularExpressionValidator,
                            Resources.DisplayNames.ClassId));
                }
            }

            return View(enrollInClassViewModel);
        }

        [HttpGet]
        public virtual ActionResult RecoverMyPassword()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult RecoverMyPassword
            (MvcEshop.ViewModels.User.RecoverMyPasswordViewModel recoverMyPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                User oUser =
                    MVC.User.GetUserByUsername(recoverMyPasswordViewModel.Email);

                string strNewPassword =
                    System.Guid.NewGuid().ToString().Replace("-", "").Substring(1, 8);

                oUser.Password =
                    Infrastructure.PasswordHash.CreateHash(strNewPassword);

                oUser.NumberOfWrongPasswords = 0;

                MVC.User.DFEntities.Entry(oUser).State = EntityState.Modified;
                MVC.User.DFEntities.SaveChanges();

                string strEmailMessage = string.Format(Resources.Messages.EmailMessageForPasswordRecovery
                    , Resources.DisplayNames.NewPassword + Resources.DisplayNames.JunctionLetter,
                    strNewPassword);

                string strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strEmailMessage);

                //notify the client and send them the password 

                Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    recoverMyPasswordViewModel.Email, Resources.DisplayNames.RecoverPassword, strEmailBody, null);

                string strMessage = string.Format(Resources.Messages.ItemWasSentSuccessfullyToEmail,
                    Resources.DisplayNames.Password, Resources.DisplayNames.Email +
                    Resources.DisplayNames.JunctionLetter);

                return RedirectToAction(MVC.User.Message(strMessage));
            }

            return View(recoverMyPasswordViewModel);
        }


        [HttpGet]
        public virtual ActionResult SignUp()
        {

            ViewBag.GenderIdList = new SelectList
                (DFEntities.Genders, "GenderId", "Description");

            ViewBag.SecretQuestionIdList = new SelectList
                (DFEntities.SecretQuestions, "SecretQuestionId", "Question");

            //MvcEshop.ViewModels.User.SignUpViewModel signUpViewModel =
            //    new ViewModels.User.SignUpViewModel();

            //signUpViewModel.AdvertisementByIrandoc = true;
            //signUpViewModel.BirthDate = null;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SignUp(MvcEshop.ViewModels.User.SignUpViewModel signUpViewModel)
        {
            if (signUpViewModel.SendSms==true && string.IsNullOrEmpty(signUpViewModel.CellPhoneNumber))
            {
                 ModelState.AddModelError
                        ("", string.Format(Resources.Messages.RequiredFieldValidator,
                        Resources.DisplayNames.CellPhoneNumber));
            }

            if (ModelState.IsValid)
            {

                MvcEshop.Models.User oUser =
                   new MvcEshop.Models.User();

                oUser.FirstName = signUpViewModel.FirstName;
                oUser.LastName = signUpViewModel.LastName;
                oUser.FatherName = signUpViewModel.FatherName;
                oUser.NationalCode = signUpViewModel.NationalCode;
                oUser.GenderId = signUpViewModel.GenderId;
                oUser.SecretQuestionId = signUpViewModel.SecretQuestionId;
                oUser.AdvertisementByIrandoc = signUpViewModel.AdvertisementByIrandoc;
                oUser.AdvertisementByOtherOrgs = signUpViewModel.AdvertisementByOtherOrgs;
                oUser.Password = Infrastructure.PasswordHash.CreateHash(signUpViewModel.Password);
                oUser.SecretAnswer = Infrastructure.PasswordHash.CreateHash(signUpViewModel.SecretAnswer);
                oUser.BirthDate = Infrastructure.Convert.ToMiladi(signUpViewModel.BirthDate);
                oUser.Email = signUpViewModel.Email.ToLower();
                oUser.SendSms = signUpViewModel.SendSms;
                oUser.CellPhoneNumber = signUpViewModel.CellPhoneNumber;
                oUser.IsProfileCompleted = true;

                MVC.User.DFEntities.Users.Add(oUser);
                MVC.User.DFEntities.SaveChanges();

                //give the role of "FreeUser" to this user
                MyRoleProvider.AddUserToRole(oUser.Email, "FreeUser");

                //building email body
                string strEmailBody = null;

                string strDomain = DFEntities.Settings
                                        .Select(current => current.Domain)
                                        .FirstOrDefault();

                string strActivationLink = @"<a href='" + strDomain +
                    "/User/ActivateUserByActiveCode?activeCode="
                    + oUser.ActiveCode +
                            "'>پیوند فعال‌سازی</a>";


                strEmailBody = string.Format
                           (Resources.Messages.WelcomeFreeUser,
                           Resources.DisplayNames.FreeUser, Resources.DisplayNames.System,
                           Resources.DisplayNames.Email + Resources.DisplayNames.JunctionLetter,
                           Resources.DisplayNames.Password + Resources.DisplayNames.JunctionLetter,
                           signUpViewModel.Password, Resources.DisplayNames.Profile, strActivationLink);

                strEmailBody = string.Format(Resources.Messages.EmailMessageLayout,
                DFEntities.Settings.FirstOrDefault().ImageUrlForMailMessageHeader,
                strEmailBody);

                /*notify the client and send them the join password and
                the temporary password(if he is introduced) for login.*/

                //Must Be Uncommented

                Infrastructure.Communication.SendEmail
                    (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                    Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                    oUser.Email, "به همانندجو خوش آمدید", strEmailBody, null);

                string strMessage = string.Format
                        (Resources.Messages.ProfileCreatedSuccessfully,
                        Resources.DisplayNames.Profile, Resources.DisplayNames.Email);

                return RedirectToAction(MVC.User.Message(strMessage));

            }


            ViewBag.GenderIdList = new SelectList
                (DFEntities.Genders, "GenderId", "Description", signUpViewModel.GenderId);

            ViewBag.SecretQuestionIdList = new SelectList
               (DFEntities.SecretQuestions, "SecretQuestionId", "Question", signUpViewModel.SecretQuestionId);

            return View(signUpViewModel);

        }


        [NonAction]
        public List<MvcEshop.Models.Account> GetExistingAndActiveAccountsByUsername(string username)
        {
            List<MvcEshop.Models.Account> lstAccounts =
                         MVC.User.GetUserByUsername(username)
                         .Accounts
                         .Where(current => current.IsDeleted == false
                          && current.IsActive == true)
                         .ToList()
                         ;

            return lstAccounts;

        }

        [HttpGet]
        public virtual ActionResult EditCredit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["UserId"] = id.Value;

            User oUser = MVC.User.GetUserByUserId(id.Value);

            if (oUser == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.User.EditCreditViewModel oEditCreditViewModel =
                new MvcEshop.ViewModels.User.EditCreditViewModel();

            System.Guid guidAccountId =
                Guid.Parse(TempData["AccountOrClassId"].ToString());

            TempData["AccountOrClassId"] = guidAccountId;

            MvcEshop.Models.CreditCard oCreditCard = oUser.CreditCards
                .Where(current => current.AccountId == guidAccountId)
                .ToList()
                .FirstOrDefault();

            oEditCreditViewModel.AllocatedVolume = oCreditCard.AllocatedVolume;
            oEditCreditViewModel.ExpirationDate = oCreditCard.ExpirationDate;

            ViewBag.AccountOrClassId = guidAccountId;

            System.Web.HttpContext.Current.Session["CurrentSiteMapForUserFullName"] =
                oUser.FullName;

            return View(oEditCreditViewModel);
        }



        [HttpPost]
        public virtual ActionResult EditCredit(MvcEshop.ViewModels.User.EditCreditViewModel editCreditViewModel)
        {

            Guid guidUserId = Guid.Parse(TempData["UserId"].ToString());
            TempData["UserId"] = guidUserId;

            MvcEshop.Models.User oUser = GetUserByUserId(guidUserId);

            if (oUser == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                Guid guidAccountId =
                  Guid.Parse(TempData["AccountOrClassId"].ToString());

                TempData["AccountOrClassId"] = guidAccountId;

                MvcEshop.Models.CreditCard oCreditCard = oUser.CreditCards
                   .Where(current => current.AccountId == guidAccountId)
                   .ToList()
                   .FirstOrDefault();

                oCreditCard.AllocatedVolume = editCreditViewModel.AllocatedVolume;
                oCreditCard.ExpirationDate = editCreditViewModel.ExpirationDate;

                MVC.User.UpdateUser(oUser);

                return RedirectToAction(MVC.User.Index(guidAccountId));
            }

            return View(editCreditViewModel);
        }



        [NonAction]
        public User GetUserByUserId(Guid userId)
        {
            MvcEshop.Models.User oUser = MVC.User.GetExistingUsers()
               .Where(current => current.UserId == userId)
               .FirstOrDefault()
               ;

            return oUser;
        }

        [HttpGet]
        public virtual ActionResult GroupEnrollment()
        {
            ////id is considered as current account's accountId
            //if (id != null)
            //{
            //    TempData["AccountOrClassId"] = id.Value;

            //    //the following session variable could be used in other controllers like assignmentController.
            //    System.Web.HttpContext.Current.Session["AccountOrClassId"] = id.Value;
            //}

            //the following session will be used for save and remove in Uploading.
            System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] = new List<FileNameFileNamesInServer>();

            //retreive accountId to be used for extracting current account's object.
            System.Guid guidAccountId =
                System.Guid.Parse(System.Web.HttpContext.Current.Session["AccountOrClassId"].ToString());

            System.Web.HttpContext.Current.Session["AccountOrClassId"] = guidAccountId;

            //find account to extract account title for currentSiteMap
            MvcEshop.Models.Account oAccount = DFEntities.Accounts
                      .Where(current => current.AccountId == guidAccountId
                      && current.IsDeleted == false)
                      .FirstOrDefault();

            System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";

            return View();
        }


        [HttpPost]
        public virtual PartialViewResult GroupEnrollmentOperation()
        {




            List<MvcEshop.Models.FileNameFileNamesInServer> lstFinalChosenFiles =
         System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] as List<MvcEshop.Models.FileNameFileNamesInServer>;

            MvcEshop.ViewModels.User.MessageContentViewModel oMessageContentViewModel =
                new ViewModels.User.MessageContentViewModel();

            if (lstFinalChosenFiles.Count == 0)
            {
                ////Error Message to say that "file" must be chosen.
                //ModelState.AddModelError
                //        ("", string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                //        Resources.DisplayNames.File));


                oMessageContentViewModel.Message = string.Format(Resources.Messages.RequiredFieldValidatorForDropDownList,
                        Resources.DisplayNames.File);

                oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.failure;

                goto end;

            }

            FileNameFileNamesInServer oFileItem = lstFinalChosenFiles.FirstOrDefault();

            if (System.IO.Path.GetExtension(oFileItem.FileName) != ".xlsx" &&
                System.IO.Path.GetExtension(oFileItem.FileName) != ".xls")
            {
                //ModelState.AddModelError
                //        ("", string.Format("پسوند فایل مورد نظر بایستی {0} یا {1} باشد.", ".xls", ".xlsx"));

                oMessageContentViewModel.Message = string.Format("پسوند فایل مورد نظر بایستی {0} یا {1} باشد.", ".xls", ".xlsx");

                oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.failure;

                goto end;

            }

            //string strFileNameWithoutExtension = System.Guid.NewGuid().ToString();

            string strFilePath = string.Format
                            (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                            + oFileItem.FileNameInServer;

            //excelFile.SaveAs(strFilePath);

            try
            {
                DFEntities.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + "EmailList" + "]");
            }
            catch (Exception ex)
            {
            }

            Infrastructure.ExcellUtility.ImportFromExcelToSqlServer
                (strFilePath.Replace(@"\", "\\"), System.IO.Path.GetFileNameWithoutExtension(strFilePath), "EmailList", "EmailList");

            System.Web.HttpContext.Current.Session["ErrorLogForBeingUsedAsNonActionMethod"] = new List<string>();

            foreach (MvcEshop.Models.EmailList emailRecord in DFEntities.EmailLists)
            {

                System.Web.HttpContext.Current.Session["IsThisActionUsedAsNonActionMethodForUserCreation"] = true;

                MvcEshop.ViewModels.User.CreateViewModel oCreateUserViewModel =
                    new MvcEshop.ViewModels.User.CreateViewModel();

                if (emailRecord.Email == null)
                {
                    continue;
                }

                oCreateUserViewModel.Email = emailRecord.Email.ToLower();

                Create(oCreateUserViewModel);

            }

            //get the list of errors filled by action "Create" in "UserController".
            List<string> lstErrors =
                System.Web.HttpContext.Current.Session["ErrorLogForBeingUsedAsNonActionMethod"] as List<string>;

            //MvcEshop.ViewModels.User.MessageContentViewModel oMessageContentViewModel =
            //    new ViewModels.User.MessageContentViewModel();

            //if all users were created successfully.
            if (lstErrors.Count == 0)
            {
                oMessageContentViewModel.Message =
                    string.Format(Resources.Messages.SthDoneSuccessfully, Resources.DisplayNames.GroupEnrollment);

                oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.success;

                goto end;
            }

            //if some users could not be created successfully.
            if (lstErrors.Count != 0)
            {

                oMessageContentViewModel.Message =
                    string.Format("<div>{0}</div>", Resources.Messages.ErrorInCreationOfSomeUsers);

                //creates error-list in html.
                foreach (string error in lstErrors)
                {
                    oMessageContentViewModel.Message =
                        string.Concat(oMessageContentViewModel.Message, string.Format("<div>{0}</div>", error));
                }

                oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.failure;

                goto end;
            }


        end:

            Guid guidAccountId =
                    Guid.Parse(System.Web.HttpContext.Current.Session["AccountOrClassId"].ToString());

            System.Web.HttpContext.Current.Session["AccountOrClassId"] = guidAccountId;




            //Test

            //MvcEshop.ViewModels.User.MessageContentViewModel oMessageContentViewModel =
            //    new ViewModels.User.MessageContentViewModel();

            //oMessageContentViewModel.Message = "Test done Successfully.";
            //oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.success;

            return PartialView("_Partial_MessageBox", oMessageContentViewModel);
        }



        [HttpGet]
        public virtual ActionResult SendGroupEmail()
        {
            //check if the current role of the user is "account manager" .
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 9)
            {
                Guid guidAccountId =
                        System.Guid.Parse(System.Web.HttpContext.Current.Session["AccountOrClassId"].ToString());

                System.Web.HttpContext.Current.Session["AccountOrClassId"] = guidAccountId;

                //find account to extract account title for currentSiteMap
                MvcEshop.Models.Account oAccount = DFEntities.Accounts
                          .Where(current => current.AccountId == guidAccountId
                          && current.IsDeleted == false)
                          .FirstOrDefault();

                System.Web.HttpContext.Current.Session["CurrentSiteMap"] = oAccount.AccountTitle + " " + ">";
                
            }

            return View();
        }

        [HttpPost]
        public virtual PartialViewResult SendGroupEmail(string editor)
        {
            List<User> lstUsers =
                System.Web.HttpContext.Current.Session["ListOfUnderControlUsers"] as List<User>;

            System.Web.HttpContext.Current.Session["ListOfUnderControlUsers"] = lstUsers;

            string strEmailBody = HttpUtility.HtmlDecode(editor);

            foreach (User user in lstUsers)
            {
                Infrastructure.Communication.SendEmail
                            (Setting.SmtpServer, Setting.SmtpServerPort, Setting.SenderEmailAddress,
                            Setting.SenderEmailPassword, Resources.DisplayNames.CommercialNameOfTheSystem,
                            user.Email, "پیغام", strEmailBody, null);

            }

            string strSuccessMessage =
                string.Format(Resources.Messages.SthDoneSuccessfully, Resources.DisplayNames.GroupEmailSending);

            MvcEshop.ViewModels.User.MessageContentViewModel oMessageContentViewModel =
                new ViewModels.User.MessageContentViewModel();

            oMessageContentViewModel.Message = strSuccessMessage;
            oMessageContentViewModel.MessageType = Infrastructure.Enums.MessageTypes.success;
            //Guid guidAccountId =
            //          Guid.Parse(System.Web.HttpContext.Current.Session["AccountOrClassId"].ToString());

            //return RedirectToAction(MVC.User.Index(guidAccountId));
            return PartialView("_Partial_MessageBox", oMessageContentViewModel);
        }

    }
}
