//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
//{
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.Administrator.MenuItem)]
//	public partial class MenuItemController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public MenuItemController()
//		{
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.IndexOfRoot)]
//		public virtual System.Web.Mvc.ActionResult IndexOfRoot(System.Guid menuId)
//		{
//			// **************************************************
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.GetById(menuId);

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.Menu = oMenu;
//			// **************************************************

//			var varMenuItems =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.Get()
//				.Where(current => current.MenuId == menuId)
//				.Where(current => current.ParentMenuItemId.HasValue == false)
//				.OrderBy(current => current.Ordering)
//				.ThenBy(current => current.UpdateDateTime)
//				.ToList()
//				;

//			return (View(varMenuItems));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Index)]
//		public virtual System.Web.Mvc.ActionResult Index(System.Guid parentMenuItemId)
//		{
//			// **************************************************
//			Models.Cms.MenuItem oParentMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.GetById(parentMenuItemId);

//			if (oParentMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.ParentMenuItem = oParentMenuItem;
//			// **************************************************

//			var varMenuItems =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.Get()
//				.Where(current => current.ParentMenuItemId.Value == parentMenuItemId)
//				.OrderBy(current => current.Ordering)
//				.ThenBy(current => current.UpdateDateTime)
//				.ToList()
//				;

//			return (View(varMenuItems));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Details)]
//		public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
//		{
//			Models.Cms.MenuItem oMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.GetById(id);

//			if (oMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oMenuItem));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.CreateRoot)]
//		public virtual System.Web.Mvc.ActionResult CreateRoot(System.Guid menuId)
//		{
//			// **************************************************
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.GetById(menuId);

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.Menu = oMenu;
//			// **************************************************

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
//			// **************************************************

//			// **************************************************
//			Models.Cms.MenuItem oMenuItem =
//				new Models.Cms.MenuItem();

//			oMenuItem.Ordering = 0;
//			oMenuItem.MenuId = menuId;
//			oMenuItem.ParentMenuItemId = null;
//			// **************************************************

//			return (View(oMenuItem));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.CreateRoot)]
//		public virtual System.Web.Mvc.ActionResult CreateRoot(Models.Cms.MenuItem menuItem)
//		{
//			// **************************************************
//			menuItem.InsertDateTime = Infrastructure.Utility.Now;
//			menuItem.UpdateDateTime = Infrastructure.Utility.Now;

//			menuItem.NavigateUrl =
//				menuItem.NavigateUrl.Replace(" ", string.Empty).ToLower();

//			if (menuItem.ImageUrl != null)
//			{
//				menuItem.ImageUrl =
//					menuItem.ImageUrl.Replace(" ", string.Empty).ToLower();
//			}

//			if (menuItem.PopoutImageUrl != null)
//			{
//				menuItem.PopoutImageUrl =
//					menuItem.PopoutImageUrl.Replace(" ", string.Empty).ToLower();
//			}
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.Insert(menuItem);

//				UnitOfWork.Save();

//				return (RedirectToAction(MVC.Administrator.MenuItem
//					.IndexOfRoot(menuItem.MenuId)));
//			}

//			// **************************************************
//			Models.Cms.Menu oMenu =
//				UnitOfWork.CmsUnitOfWork.MenuRepository.GetById(menuItem.MenuId);

//			if (oMenu == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.Menu = oMenu;
//			// **************************************************

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", menuItem.AccessTypeId);
//			// **************************************************

//			return (View(menuItem));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(System.Guid parentMenuItemId)
//		{
//			// **************************************************
//			Models.Cms.MenuItem oParentMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository
//				.GetById(parentMenuItemId);

//			if (oParentMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.ParentMenuItem = oParentMenuItem;
//			// **************************************************

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
//			// **************************************************

//			// **************************************************
//			Models.Cms.MenuItem oMenuItem =
//				new Models.Cms.MenuItem();

//			oMenuItem.Ordering = 0;
//			oMenuItem.MenuId = oParentMenuItem.MenuId;
//			oMenuItem.ParentMenuItemId = parentMenuItemId;
//			// **************************************************

//			return (View(oMenuItem));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.MenuItem menuItem)
//		{
//			// **************************************************
//			menuItem.InsertDateTime = Infrastructure.Utility.Now;
//			menuItem.UpdateDateTime = Infrastructure.Utility.Now;

//			menuItem.NavigateUrl =
//				menuItem.NavigateUrl.Replace(" ", string.Empty).ToLower();

//			if (menuItem.ImageUrl != null)
//			{
//				menuItem.ImageUrl =
//					menuItem.ImageUrl.Replace(" ", string.Empty).ToLower();
//			}

//			if (menuItem.PopoutImageUrl != null)
//			{
//				menuItem.PopoutImageUrl =
//					menuItem.PopoutImageUrl.Replace(" ", string.Empty).ToLower();
//			}
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.Insert(menuItem);

//				UnitOfWork.Save();

//				return (RedirectToAction(MVC.Administrator.MenuItem
//					.Index(menuItem.ParentMenuItemId.Value)));
//			}

//			// **************************************************
//			Models.Cms.MenuItem oParentMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository
//				.GetById(menuItem.ParentMenuItemId.Value);

//			if (oParentMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.ParentMenuItem = oParentMenuItem;
//			// **************************************************

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", menuItem.AccessTypeId);
//			// **************************************************

//			return (View(menuItem));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(System.Guid id)
//		{
//			Models.Cms.MenuItem oMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.GetById(id);

//			if (oMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", oMenuItem.AccessTypeId);
//			// **************************************************

//			return (View(oMenuItem));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.MenuItem menuItem)
//		{
//			// **************************************************
//			menuItem.UpdateDateTime = Infrastructure.Utility.Now;

//			menuItem.NavigateUrl =
//				menuItem.NavigateUrl.Replace(" ", string.Empty).ToLower();

//			if (menuItem.ImageUrl != null)
//			{
//				menuItem.ImageUrl =
//					menuItem.ImageUrl.Replace(" ", string.Empty).ToLower();
//			}

//			if (menuItem.PopoutImageUrl != null)
//			{
//				menuItem.PopoutImageUrl =
//					menuItem.PopoutImageUrl.Replace(" ", string.Empty).ToLower();
//			}
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.Update(menuItem);

//				UnitOfWork.Save();

//				if (menuItem.ParentMenuItemId.HasValue)
//				{
//					return (RedirectToAction
//						(MVC.Administrator.MenuItem.Index(menuItem.ParentMenuItemId.Value)));
//				}
//				else
//				{
//					return (RedirectToAction
//						(MVC.Administrator.MenuItem.IndexOfRoot(menuItem.MenuId)));
//				}
//			}

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", menuItem.AccessTypeId);
//			// **************************************************

//			return (View(menuItem));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult Delete(System.Guid id)
//		{
//			Models.Cms.MenuItem oMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.GetById(id);

//			if (oMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oMenuItem));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ActionName("Delete")]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
//		{
//			Models.Cms.MenuItem oMenuItem =
//				UnitOfWork.CmsUnitOfWork.MenuItemRepository.GetById(id);

//			if (oMenuItem == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			System.Guid sId;
//			bool blnGoToRoot;
//			if (oMenuItem.ParentMenuItemId.HasValue)
//			{
//				blnGoToRoot = false;
//				sId = oMenuItem.ParentMenuItemId.Value;
//			}
//			else
//			{
//				blnGoToRoot = true;
//				sId = oMenuItem.MenuId;
//			}

//			UnitOfWork.CmsUnitOfWork.MenuItemRepository.Delete(oMenuItem);

//			UnitOfWork.Save();

//			if (blnGoToRoot)
//			{
//				return (RedirectToAction
//					(MVC.Administrator.MenuItem.IndexOfRoot(sId)));
//			}
//			else
//			{
//				return (RedirectToAction
//					(MVC.Administrator.MenuItem.Index(sId)));
//			}
//		}
//	}
//}
