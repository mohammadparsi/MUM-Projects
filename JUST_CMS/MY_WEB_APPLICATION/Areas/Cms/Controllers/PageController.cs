using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/12/26
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Cms.Page)]
	public partial class PageController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PageController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varPages =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varPages));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id.Value);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPage));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			var varLayouts =
				UnitOfWork.CmsUnitOfWork
				.LayoutRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.LayoutId =
				new System.Web.Mvc.SelectList(varLayouts, "Id", "FullName");
			// **************************************************

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
			// **************************************************

			// **************************************************
			Models.Cms.Page oPage =
				new Models.Cms.Page();

			oPage.AuthorUserId =
				Infrastructure.Sessions.AuthenticatedUser.Id;

			oPage.Author =
				Infrastructure.Sessions.AuthenticatedUser.FullName;
			// **************************************************

			return (View(oPage));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "AuthorUserId,AccessTypeId,LayoutId,Name,DoesSearchEnginesIndexIt,DoesSearchEnginesFollowIt,Hits,Title,Author,Keywords,Classification,Description,Copyright,StartPublishingDateTime,FinishPublishingDateTime,IsSystem,IsActive,Ordering,IsVerified,IsDeleted,PageContent,Tags")]
			Models.Cms.Page page)
		{
			// **************************************************
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork
				.PageRepository.GetByCultureLcidAndName(CultureLcid, page.Name)
				;

			if (oPage != null)
			{
				// نام صفحه تکراری است! لطفا از نام ديگری استفاده نماييد
				string strErrorMessage = Models.Resources.Cms.Page.Error001;

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				page.AuthorUserId = Infrastructure.Sessions.AuthenticatedUser.Id;

				page.CultureLcid = CultureLcid;
				page.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				// **************************************************
				page.SetIsActive
					(page.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				page.SetIsDeleted
					(page.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				page.SetIsVerified
					(page.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer)
				{
					page.IsSystem = false;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PageRepository.Insert(page);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Page.Index()));
			}

			// **************************************************
			var varLayouts =
				UnitOfWork.CmsUnitOfWork
				.LayoutRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.LayoutId =
				new System.Web.Mvc.SelectList
					(varLayouts, "Id", "FullName", page.LayoutId);
			// **************************************************

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", page.AccessTypeId);
			// **************************************************

			return (View(page));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id.Value);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varLayouts =
				UnitOfWork.CmsUnitOfWork
				.LayoutRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.LayoutId =
				new System.Web.Mvc.SelectList
					(varLayouts, "Id", "FullName", oPage.LayoutId);
			// **************************************************

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", oPage.AccessTypeId);
			// **************************************************

			return (View(oPage));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,AuthorUserId,AccessTypeId,LayoutId,Name,DoesSearchEnginesIndexIt,DoesSearchEnginesFollowIt,Hits,Title,Author,Keywords,Classification,Description,Copyright,StartPublishingDateTime,FinishPublishingDateTime,IsSystem,IsActive,Ordering,IsVerified,IsDeleted,PageContent,Tags")]
			Models.Cms.Page page)
		{
			// **************************************************
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetByCultureLcidAndNameExceptId(CultureLcid, page.Name, page.Id)
				;

			if (oPage != null)
			{
				// نام صفحه تکراری است! لطفا از نام ديگری استفاده نماييد
				string strErrorMessage = Models.Resources.Cms.Page.Error001;

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			// **************************************************
			Models.Cms.Page oOriginalPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(page.Id);

			if (oOriginalPage.PageContent == null)
			{
				oOriginalPage.PageContent =
					new Models.Cms.PageContent();
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalPage.CultureLcid = CultureLcid;
				oOriginalPage.PageContent.Content = page.PageContent.Content;
				// **************************************************

				// **************************************************
				oOriginalPage.Hits = page.Hits;
				oOriginalPage.Tags = page.Tags;
				oOriginalPage.Title = page.Title;
				oOriginalPage.Author = page.Author;
				oOriginalPage.Keywords = page.Keywords;
				oOriginalPage.LayoutId = page.LayoutId;
				oOriginalPage.Ordering = page.Ordering;
				oOriginalPage.Copyright = page.Copyright;
				oOriginalPage.Description = page.Description;
				oOriginalPage.AccessTypeId = page.AccessTypeId;
				oOriginalPage.Classification = page.Classification;
				oOriginalPage.StartPublishingDateTime = page.StartPublishingDateTime;
				oOriginalPage.DoesSearchEnginesIndexIt = page.DoesSearchEnginesIndexIt;
				oOriginalPage.FinishPublishingDateTime = page.FinishPublishingDateTime;
				oOriginalPage.DoesSearchEnginesFollowIt = page.DoesSearchEnginesFollowIt;
				// **************************************************

				// **************************************************
				oOriginalPage.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPage.SetIsActive
					(page.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				oOriginalPage.SetIsDeleted
					(page.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				oOriginalPage.SetIsVerified
					(page.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalPage.Name = page.Name;
					oOriginalPage.IsSystem = page.IsSystem;
				}
				else
				{
					if (page.IsSystem == false)
					{
						oOriginalPage.Name = page.Name;
						oOriginalPage.IsSystem = page.IsSystem;
					}
				}
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				if (oOriginalPage.AccessTypeEnum != Models.Enums.AccessTypes.Special)
				{
					if ((oOriginalPage.Groups != null) && (oOriginalPage.Groups.Count > 0))
					{
						oOriginalPage.Groups.Clear();
					}

					if ((oOriginalPage.Users != null) && (oOriginalPage.Users.Count > 0))
					{
						oOriginalPage.Users.Clear();
					}
				}
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PageRepository.Update(oOriginalPage);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Page.Index()));
			}

			// **************************************************
			var varLayouts =
				UnitOfWork.CmsUnitOfWork
				.LayoutRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.LayoutId =
				new System.Web.Mvc.SelectList
					(varLayouts, "Id", "FullName", page.LayoutId);
			// **************************************************

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", page.AccessTypeId);
			// **************************************************

			return (View(page));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id.Value);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPage));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
		{
			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.CmsUnitOfWork.PageRepository.Delete(oPage);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.Page.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id.Value);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Groups =
				UnitOfWork.GroupRepository.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(oPage));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups
			(System.Guid id, System.Guid[] checkBoxes)
		{
			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oGroups =
				UnitOfWork.GroupRepository.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			oPage.Groups.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					Models.Group oGroup =
						oGroups
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (oGroup != null)
					{
						oPage.Groups.Add(oGroup);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Groups = oGroups;

			return (View(oPage));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectUsers)]
		public virtual System.Web.Mvc.ActionResult SelectUsers(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id.Value);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Users =
				UnitOfWork.UserRepository.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(oPage));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectUsers)]
		public virtual System.Web.Mvc.ActionResult SelectUsers
			(System.Guid id, System.Guid[] checkBoxes)
		{
			// **************************************************
			//Models.Cms.Page oPage =
			//	UnitOfWork.CmsUnitOfWork.PageRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository.GetById(id);
			// **************************************************

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oUsers =
				UnitOfWork.UserRepository.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			oPage.Users.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					Models.User oUser =
						oUsers
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (oUser != null)
					{
						oPage.Users.Add(oUser);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Users = oUsers;

			return (View(oPage));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.EditLocalizedPageContent)]
		public virtual System.Web.Mvc.ActionResult EditLocalizedPageContent
			(string areaName, string controllerName, string actionName)
		{
			// **************************************************
			string strFileName = string.Empty;
			if (string.IsNullOrEmpty(areaName))
			{
				strFileName =
					string.Format("{0}_{1}.{2}.htm",
					controllerName, actionName, CultureName);
			}
			else
			{
				strFileName =
					string.Format("{0}_{1}_{2}.{3}.htm",
					areaName, controllerName, actionName, CultureName);
			}

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedPageContents/{0}", strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			string strLocalizedPageContent = Dtx.IO.File.Read(strPathName);
			// **************************************************

			// **************************************************
			ViewModels.Areas.Cms.Page.EditLocalizedPageContentViewModel oEditLocalizedPageContentViewModel =
				new ViewModels.Areas.Cms.Page.EditLocalizedPageContentViewModel();

			oEditLocalizedPageContentViewModel.AreaName = areaName;
			oEditLocalizedPageContentViewModel.ActionName = actionName;
			oEditLocalizedPageContentViewModel.ControllerName = controllerName;

			oEditLocalizedPageContentViewModel.LocalizedPageContent = strLocalizedPageContent;
			// **************************************************

			return (View(oEditLocalizedPageContentViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.EditLocalizedPageContent)]
		public virtual System.Web.Mvc.ActionResult EditLocalizedPageContent
			(ViewModels.Areas.Cms.Page.EditLocalizedPageContentViewModel viewModel)
		{
			// **************************************************
			string strFileName = string.Empty;
			if (string.IsNullOrEmpty(viewModel.AreaName))
			{
				strFileName =
					string.Format("{0}_{1}.{2}.htm",
					viewModel.ControllerName, viewModel.ActionName, CultureName);
			}
			else
			{
				strFileName =
					string.Format("{0}_{1}_{2}.{3}.htm",
					viewModel.AreaName, viewModel.ControllerName, viewModel.ActionName, CultureName);
			}

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedPageContents/{0}", strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			Dtx.IO.File.Overwrite(strPathName, viewModel.LocalizedPageContent);
			// **************************************************

			// **************************************************
			// محتوای صفحه با موفقيت ذخيره گرديد
			string strInformationMessage =
				Models.Resources.Cms.Page.Information001;

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, strInformationMessage));
			// **************************************************

			return (View(viewModel));
		}
	}
}
