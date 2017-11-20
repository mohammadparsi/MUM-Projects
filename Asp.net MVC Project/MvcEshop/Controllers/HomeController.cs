using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Postal;
using SendMail;
using System.Net.Mail;

namespace MvcEshop.Controllers
{
    [Infrastructure.Attributes.Internationalization]
    public partial class HomeController : Infrastructure.BaseControllerWithDatabaseContext
    {
        public virtual ActionResult Index()
        {
            //string text = "A class is the most powerful data type in C#. Like a structure, " +
            //          "a class defines the data and behavior of the data type. ";
            //// WriteAllText creates a file, writes the specified string to the file,
            //// and then closes the file.    You do NOT need to call Flush() or Close().
            //System.IO.File.WriteAllText(@"F:\Important Files\DocumentFinderForConferenceRelease\Project_94_10_28\Assignments\WriteText.txt", text);

            return RedirectToAction(MVC.User.Login());
        }

        [HttpPost]
        public virtual ActionResult Index(string roleTitleInPersian)
        {
            
            //string strRoleTitleInPersian = System.Web.HttpContext.Current.Session["RoleId"].ToString();
            return View();
        }

        

        public virtual ActionResult About()
        {

            return View();
        }

        [HttpGet]
        public virtual ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult ContactUs
            (MvcEshop.ViewModels.Home.ContactUsViewModel oContactUsViewModel)
        {

            if (ModelState.IsValid)
            {
                MvcEshop.Models.ContactUsInfo oContactUsInfo =
                    new MvcEshop.Models.ContactUsInfo();

                oContactUsInfo.EmailAddress = oContactUsViewModel.Email;
                oContactUsInfo.Message = oContactUsViewModel.Message;

                DFEntities.ContactUsInfoes.Add(oContactUsInfo);
                DFEntities.SaveChanges();

            }


            return View(oContactUsViewModel);
        }



        //public virtual ActionResult GenerateExceptionError()
        //{
        //    throw (new System.Exception("some new unexpected error ..."));
        //}

       
        [HttpGet]
        public virtual ActionResult FAQs()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Rights()
        {
            return View();
        }


        public virtual ActionResult ajax()
        {
            return View();
        }

        public virtual ActionResult MembershipGuide()
        {
            return View();
        }

     
       
    }
}