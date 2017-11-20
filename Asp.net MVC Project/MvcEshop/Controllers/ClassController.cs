using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace MvcEshop.Controllers
{

    public partial class ClassController : Infrastructure.BaseControllerWithDatabaseContext
    {

        [Authorize(Roles = "Instructor,Student")]
        // GET: Class
        public virtual ActionResult Index()
        {

            List<MvcEshop.Models.Class> lstClasses = null;

            //if the logged-in User is an Instructor
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 4)
            {

                System.Guid guidUserId =
                    MVC.User.FindUserByUsername(User.Identity.Name);



                lstClasses = DFEntities.Classes
                     .Where(current => current.IsDeleted == false
                         && current.InstructorUserId == guidUserId
                     )
                     .OrderByDescending(current => current.StartDate)
                     .ToList();
            }

            //if the logged-in User is an Student
            if (System.Convert.ToInt32
                (System.Web.HttpContext.Current.Session["RoleId"].ToString()) == 5)
            {

                MvcEshop.Models.User oStudent =
                    MVC.User.GetUserByUsername(User.Identity.Name);



                lstClasses = oStudent.ClassesOfTheStudent
                     .Where(current => current.IsDeleted == false)
                     .OrderByDescending(current => current.StartDate)
                     .ToList();
            }


            return View(lstClasses);
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public virtual ActionResult Create()
        {
            ViewBag.AccountId =
               new SelectList(MVC.User.GetExistingAndActiveAccountsByUsername(User.Identity.Name),
                   "AccountId", "AccountTitle");

            return View();
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create
            (MvcEshop.ViewModels.Class.CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {

                MvcEshop.Models.Class oClass =
                    new MvcEshop.Models.Class();

                oClass.AccountId = createViewModel.AccountId;
                oClass.Title = createViewModel.Title;
                oClass.EnrollmentPassword = createViewModel.EnrollmentPassword;
                oClass.StartDate = Infrastructure.Convert.ToMiladi(createViewModel.StartDate);
                oClass.EndDate = Infrastructure.Convert.ToMiladi(createViewModel.EndDate);


                oClass.InstructorUserId =
                    MVC.User.FindUserByUsername(User.Identity.Name);

                oClass.CreatorUserId =
                    MVC.User.FindUserByUsername(User.Identity.Name);

                DFEntities.Classes.Add(oClass);
                DFEntities.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.AccountId =
               new SelectList(MVC.User.GetExistingAndActiveAccountsByUsername(User.Identity.Name),
                   "AccountId", "AccountTitle");


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

            TempData["ClassId"] = id.Value;

            MvcEshop.Models.Class oClass =
                DFEntities.Classes
                 .Where(current => current.ClassId == id.Value
                     && current.IsDeleted == false)
                 .FirstOrDefault()
                 ;

            if (oClass == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Class.CreateViewModel oEditViewModel =
                new MvcEshop.ViewModels.Class.CreateViewModel();

            oEditViewModel.AccountId = oClass.AccountId.Value;
            oEditViewModel.Title = oClass.Title;
            oEditViewModel.EnrollmentPassword = oClass.EnrollmentPassword;

            oEditViewModel.StartDate =
                Infrastructure.Convert.ToPersian(oClass.StartDate);

            oEditViewModel.EndDate =
                Infrastructure.Convert.ToPersian(oClass.EndDate);

            ViewBag.AccountId =
               new SelectList(MVC.User.GetExistingAndActiveAccountsByUsername(User.Identity.Name),
                   "AccountId", "AccountTitle", oEditViewModel.AccountId);

            return View(oEditViewModel);
        }


        [Authorize(Roles = "Instructor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit
            (MvcEshop.ViewModels.Class.CreateViewModel editViewModel)
        {
            Guid guidClassId = Guid.Parse(TempData["ClassId"].ToString());

            TempData["ClassId"] = guidClassId;

            MvcEshop.Models.Class oClass = DFEntities.Classes
                .Where(current => current.ClassId == guidClassId
                && current.IsDeleted == false)
                .FirstOrDefault()
                ;

            if (oClass == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                oClass.AccountId = editViewModel.AccountId;
                oClass.Title = editViewModel.Title;
                oClass.EnrollmentPassword = editViewModel.EnrollmentPassword;

                oClass.StartDate =
                    Infrastructure.Convert.ToMiladi(editViewModel.StartDate);

                oClass.EndDate =
                    Infrastructure.Convert.ToMiladi(editViewModel.EndDate);

                DFEntities.Entry(oClass).State = EntityState.Modified;
                DFEntities.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.AccountId =
               new SelectList(MVC.User.GetExistingAndActiveAccountsByUsername(User.Identity.Name),
                   "AccountId", "AccountTitle", editViewModel.AccountId);


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

            MvcEshop.Models.Class oClass = DFEntities.Classes
                .Where(current => current.ClassId == id.Value
                && current.IsDeleted == false)
                .FirstOrDefault()
                ;

            if (oClass == null)
            {
                return HttpNotFound();
            }

            return View(oClass);
        }


        // POST: Account/Delete/5
        [Authorize(Roles = "Instructor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            MvcEshop.Models.Class oClass = DFEntities.Classes
                .Where(current => current.ClassId == id && current.IsDeleted == false)
                .FirstOrDefault();

            oClass.IsDeleted = true;
            DFEntities.Entry(oClass).State = EntityState.Modified;
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
        public bool DoesClassExists(Guid id)
        {
            bool blnResult = true;

            MvcEshop.Models.Class oClass = DFEntities.Classes
                .Where(current => current.ClassId == id && current.IsDeleted == false)
                .FirstOrDefault();

            if (oClass == null)
            {
                blnResult = false;
            }

            return blnResult;

        }
    }
}