using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.Controllers
{
    public partial class CheckPlagiarismController : Infrastructure.BaseControllerWithDatabaseContext
    {
        // GET: CheckPlagiarism
        public virtual ActionResult Index()
        {
            return RedirectToAction("CheckPlagiarism", "Assignment");
        }
    }
}