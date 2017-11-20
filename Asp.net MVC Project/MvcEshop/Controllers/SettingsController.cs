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
    [Authorize(Roles = "Programmer")]
    public partial class SettingsController : Infrastructure.BaseControllerWithDatabaseContext
    {

        // GET: Settings
        
        // GET: Settings/Edit/5
        [HttpGet]
        public virtual ActionResult Edit()
        {
            MvcEshop.Models.Setting oSetting = 
                DFEntities.Settings.FirstOrDefault();

            return View(oSetting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(Setting setting)
        {
            if (ModelState.IsValid)
            {
                DFEntities.Entry(setting).State = EntityState.Modified;
                DFEntities.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(setting);
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
