using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.Controllers
{
    [Authorize(Roles="SystemAdministrator, Programmer, PageContentManagement")]
    public partial class PageContentController : Infrastructure.BaseControllerWithDatabaseContext
    {

        // GET: PageContent
        public virtual ActionResult Index()
        {
          return View(DFEntities.Pages.ToList());
        }

        [HttpGet]
        public virtual ActionResult EditContent(int pageId)
        {
            TempData["PageId"] = pageId;

            MvcEshop.Models.Page oPage = DFEntities.Pages
                .Where(current => current.PageId == pageId)
                .ToList()
                .FirstOrDefault();

            return View((object)oPage.PageContent);
        }

        [HttpPost]
        public virtual ActionResult EditContent(string editor)
        {
            int intPageId = int.Parse(TempData["PageId"].ToString());

            MvcEshop.Models.Page oPage = DFEntities.Pages
                .Where(current => current.PageId == intPageId)
                .ToList()
                .FirstOrDefault();

            oPage.PageContent = HttpUtility.HtmlDecode(editor);

            DFEntities.Entry(oPage).State = System.Data.Entity.EntityState.Modified;
            DFEntities.SaveChanges();

            return RedirectToAction(MVC.PageContent.Index());
        }


        [HttpGet]
        public virtual ActionResult ChooseNewSlideShowImages()
        {
            System.Web.HttpContext.Current.Session["NoImagesHasBeenUploaded"] = true;
            return View();
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