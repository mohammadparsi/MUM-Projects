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

   
    public partial class TariffController : Infrastructure.BaseControllerWithDatabaseContext
    {


   [Authorize(Roles = "SystemAdministrator, TariffManagement")]      
        // GET: Contract/Edit/5
        public virtual ActionResult Edit()
        {

            MvcEshop.Models.Tariff oTariff =
                MVC.User.DFEntities.Tariffs.FirstOrDefault();


            if (oTariff == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Tariff.EditViewModel oEditViewModel =
               new MvcEshop.ViewModels.Tariff.EditViewModel();

            oEditViewModel.IrandocTariffInRials = MVC.User.DFEntities.Resources
                .Where(current => current.Id == 1)
                .Select(current => current.Price)
                .FirstOrDefault()
                ;

            oEditViewModel.InternetTariffInRials = MVC.User.DFEntities.Resources
                .Where(current => current.Id == 2)
                .Select(current => current.Price)
                .FirstOrDefault()
                ;



            ViewBag.TariffTypeId = new SelectList
                (MVC.User.DFEntities.TariffTypes, "Id", "Description", oTariff.TariffTypeId);

            return View(oEditViewModel);
        }



        [Authorize(Roles = "SystemAdministrator, TariffManagement")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit
            (MvcEshop.ViewModels.Tariff.EditViewModel editViewModel)
        {

            MvcEshop.Models.Tariff oTariff =
                MVC.User.DFEntities.Tariffs
                .ToList()
                .FirstOrDefault()
                ;

            if (oTariff == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                oTariff.TariffTypeId = editViewModel.TariffTypeId;

                MvcEshop.Models.Resource oResource1 = MVC.User.DFEntities.Resources
                .Where(current => current.Id == 1)
                .ToList()
                .FirstOrDefault()
                ;

                MvcEshop.Models.Resource oResource2 = MVC.User.DFEntities.Resources
                .Where(current => current.Id == 2)
                .ToList()
                .FirstOrDefault()
                ;

                oResource1.Price = editViewModel.IrandocTariffInRials;
                oResource2.Price = editViewModel.InternetTariffInRials;


                MVC.User.DFEntities.Entry(oTariff).State = EntityState.Modified;
                MVC.User.DFEntities.Entry(oResource1).State = EntityState.Modified;
                MVC.User.DFEntities.Entry(oResource2).State = EntityState.Modified;

                MVC.User.DFEntities.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.TariffTypeId = new SelectList
                (MVC.User.DFEntities.TariffTypes, "Id", "Description", editViewModel.TariffTypeId);

            return View(editViewModel);
        }


        [HttpGet]
        public virtual ActionResult ShowLastTariffStatus()
        {
            MvcEshop.Models.DFEntities oDFEntities = new Models.DFEntities();

            MvcEshop.Models.TariffType oCurrentTariffType =
                oDFEntities.Tariffs
                .ToList()
                .FirstOrDefault()
                .TariffType
                ;

            string strCurrentTariffType = oCurrentTariffType.Description;

            int intCurrentIrandocTariff = oDFEntities.Resources
                .Where(current => current.Id == 1)
                .ToList()
                .FirstOrDefault()
                .Price.Value;

            int intCurrentInternetTariff = oDFEntities.Resources
                .Where(current => current.Id == 2)
                .ToList()
                .FirstOrDefault()
                .Price.Value;

            MvcEshop.ViewModels.Tariff.ShowLastTariffStatus oShowLastTariffStatus =
                new MvcEshop.ViewModels.Tariff.ShowLastTariffStatus();

            oShowLastTariffStatus.CurrentTariffType = strCurrentTariffType;
            oShowLastTariffStatus.InternetTariff = intCurrentInternetTariff;
            oShowLastTariffStatus.IrandocTariff = intCurrentIrandocTariff;

            if (oCurrentTariffType.Id == 2)
            {
                oShowLastTariffStatus.CurrentTariffTypeFullDescription =
                    Resources.Messages.ConstantTariffFullDescription;
            }

            if (oCurrentTariffType.Id == 3)
            {
                oShowLastTariffStatus.CurrentTariffTypeFullDescription =
                    Resources.Messages.FloatTariffFullDescription;
            }

            return View(oShowLastTariffStatus);
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
