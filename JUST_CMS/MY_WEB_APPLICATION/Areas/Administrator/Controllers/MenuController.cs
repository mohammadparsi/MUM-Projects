//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
//{
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.Administrator.Menu)]
//	public partial class MenuController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public MenuController()
//		{
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Index)]
//		public virtual System.Web.Mvc.ActionResult Index()
//		{
//			var varMenus =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Get()
//				.Include(current => current.Role)
//				.Where(current => current.CultureLcid == CultureLcid)
//				.OrderByDescending(current => current.UpdateDateTime)
//				.ToList()
//				;

//			return (View(varMenus));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Details)]
//		public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
//		{
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oMenu));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create()
//		{
//			// **************************************************
//			var varRoles =
//				UnitOfWork.RoleRepository.GetActiveByCultureLcidAndLessThanCode
//				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode);

//			ViewBag.RoleId =
//				new System.Web.Mvc.SelectList(varRoles, "Id", "Name");
//			// **************************************************

//			// **************************************************
//			Models.Cms.Menu oMenu =
//				new Models.Cms.Menu();
//			// **************************************************

//			return (View(oMenu));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.Menu menu)
//		{
//			// **************************************************
//			menu.InsertDateTime = Infrastructure.Utility.Now;
//			menu.UpdateDateTime = Infrastructure.Utility.Now;
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Insert(menu);

//				UnitOfWork.Save();

//				return (RedirectToAction(MVC.Administrator.Menu.Index()));
//			}

//			// **************************************************
//			var varRoles =
//				UnitOfWork.RoleRepository.GetActiveByCultureLcidAndLessThanCode
//				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode);

//			ViewBag.RoleId =
//				new System.Web.Mvc.SelectList
//					(varRoles, "Id", "Name", menu.RoleId);
//			// **************************************************

//			return (View(menu));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(System.Guid id)
//		{
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			// **************************************************
//			var varRoles =
//				UnitOfWork.RoleRepository.GetActiveByCultureLcidAndLessThanCode
//				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode);

//			ViewBag.RoleId =
//				new System.Web.Mvc.SelectList
//					(varRoles, "Id", "Name", oMenu.RoleId);
//			// **************************************************

//			return (View(oMenu));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.Menu menu)
//		{
//			menu.UpdateDateTime = Infrastructure.Utility.Now;

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Update(menu);

//				UnitOfWork.Save();

//				return (RedirectToAction(MVC.Administrator.Menu.Index()));
//			}

//			// **************************************************
//			var varRoles =
//				UnitOfWork.RoleRepository.GetActiveByCultureLcidAndLessThanCode
//				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode);

//			ViewBag.RoleId =
//				new System.Web.Mvc.SelectList
//					(varRoles, "Id", "Name", menu.RoleId);
//			// **************************************************

//			return (View(menu));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult Delete(System.Guid id)
//		{
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oMenu));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ActionName("Delete")]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
//		{
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			UnitOfWork.CmsUnitOfWork.MenuRepository.Delete(oMenu);

//			UnitOfWork.Save();

//			return (RedirectToAction(MVC.Administrator.Menu.Index()));
//		}
//	}
//}
