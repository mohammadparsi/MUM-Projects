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
    [Authorize(Roles="SystemAdministrator")]
    public partial class ContractController : Infrastructure.BaseControllerWithDatabaseContext
    {
        [HttpGet]
        // GET: Contract
        public virtual ActionResult Index()
        {
            var varContracts = 
                DFEntities.Contracts
                .Where(current => current.IsDeleted == false)
                .OrderByDescending(current => current.StartDate)
                .ToList();
            
            return View(varContracts);
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
            ViewBag.CustomerId = 
                new SelectList(MVC.Customer.GetExistingCustomers(), "CustomerId", "Name");

            //DatePickerItemList



            return View();
        }

        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create
            (MvcEshop.ViewModels.Contract.CreateViewModel createViewModel)
        {


            if (ModelState.IsValid)
            {
                MvcEshop.Models.Contract oContract =
                    new MvcEshop.Models.Contract();

                oContract.CustomerId = createViewModel.CustomerId;
                oContract.ExpirationDate = createViewModel.ExpirationDate;
                oContract.IssueDate = createViewModel.IssueDate;
                oContract.PriceInRials = createViewModel.PriceInRials;
                oContract.StartDate = createViewModel.StartDate;
                oContract.VolumeInKBytes = createViewModel.VolumeInKBytes;

                //add the contract to the database
                DFEntities.Contracts.Add(oContract);
                DFEntities.SaveChanges();

                //return to the list of contracts
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(DFEntities.Customers, "CustomerId", "Name");

            return View(createViewModel);
        }

        public MvcEshop.Models.Contract CreateContract(MvcEshop.Models.Contract contract,
            MvcEshop.ViewModels.Contract.CreateViewModel createViewModel)
        {
            return null;
        }

        // GET: Contract/Edit/5
        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["ContractId"] = id.Value;

            Contract oContract = DFEntities.Contracts
                .Where(current=>current.ContractId==id.Value
                 && current.IsDeleted==false)
                 .FirstOrDefault()
                 ;

            if (oContract == null)
            {
                return HttpNotFound();
            }

            MvcEshop.ViewModels.Contract.CreateViewModel oCreateViewModel =
               new MvcEshop.ViewModels.Contract.CreateViewModel();

            oCreateViewModel.CustomerId = oContract.CustomerId;
            oCreateViewModel.ExpirationDate = oContract.ExpirationDate;
            oCreateViewModel.IssueDate = oContract.IssueDate;
            oCreateViewModel.PriceInRials = oContract.PriceInRials;
            oCreateViewModel.StartDate = oContract.StartDate;
            oCreateViewModel.VolumeInKBytes = oContract.VolumeInKBytes;

            ViewBag.CustomerId = new SelectList
                (DFEntities.Customers, "CustomerId", "Name", oCreateViewModel.CustomerId);

            return View(oCreateViewModel);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit
            (MvcEshop.ViewModels.Contract.CreateViewModel createViewModel)
        {
            Guid guidContractId = Guid.Parse(TempData["ContractId"].ToString());

            MvcEshop.Models.Contract oContract = DFEntities.Contracts
                .Where(current => current.ContractId == guidContractId)
                .FirstOrDefault()
                ;

            if (oContract == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {

                oContract.CustomerId = createViewModel.CustomerId;
                oContract.ExpirationDate = createViewModel.ExpirationDate;
                oContract.IssueDate = createViewModel.IssueDate;
                oContract.PriceInRials = createViewModel.PriceInRials;
                oContract.StartDate = createViewModel.StartDate;
                oContract.VolumeInKBytes = createViewModel.VolumeInKBytes;

                DFEntities.Entry(oContract).State = EntityState.Modified;
                DFEntities.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList
                 (DFEntities.Customers, "CustomerId", "Name", createViewModel.CustomerId);
            
            return View(createViewModel);
        }

        // GET: Contract/Delete/5
        public virtual ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = DFEntities.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            Contract oContract = DFEntities.Contracts.Find(id);
            //DFEntities.Contracts.Remove(contract);
            oContract.IsDeleted = true;
          
            DFEntities.Entry(oContract).State = EntityState.Modified;
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
    }
}
