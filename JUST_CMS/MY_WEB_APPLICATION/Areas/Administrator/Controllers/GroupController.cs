using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/12/19
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.Group)]
	public partial class GroupController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public GroupController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varGroups =
				UnitOfWork.GroupRepository
				.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varGroups));
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

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.Group oGroup = new Models.Group();
			// **************************************************

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "Name,IsActive,Ordering,Description")]
			Models.Group group)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				group.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				group.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				group.SetIsActive
					(group.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GroupRepository.Insert(group);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Group.Index()));
			}

			return (View(group));
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

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Name,IsActive,Ordering,Description")]
			Models.Group group)
		{
			// **************************************************
			Models.Group oOriginalGroup =
				UnitOfWork.GroupRepository.GetById(group.Id)
				;

			if (oOriginalGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalGroup.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalGroup.Name = group.Name;
				oOriginalGroup.Ordering = group.Ordering;
				oOriginalGroup.Description = group.Description;
				// **************************************************

				// **************************************************
				oOriginalGroup.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalGroup.SetIsActive
					(group.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GroupRepository.Update(oOriginalGroup);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Group.Index()));
			}

			return (View(group));
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

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oGroup));
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
			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.GroupRepository.Delete(oGroup);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.Group.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectProjectActionsForGroup)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/Group'>{0}</a>",
				Resources.Controllers.Administrator_Group);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oGroup.Id,
					name: oGroup.Name,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Models.Resources.Group.EntityName,
					selectedProjectActions: oGroup.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectProjectActionsForGroup)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oProjectActions =
				UnitOfWork.ProjectActionRepository.Get()
				.ToList()
				;

			oGroup.ProjectActions.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varProjectAction =
						oProjectActions
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varProjectAction != null)
					{
						oGroup.ProjectActions.Add(varProjectAction);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/Group'>{0}</a>",
				Resources.Controllers.Administrator_Group);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oGroup.Id,
					name: oGroup.Name,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Models.Resources.Group.EntityName,
					selectedProjectActions: oGroup.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetSpecial()
				.ToList()
				;

			return (View(oGroup));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.Group oGroup =
				UnitOfWork.GroupRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oGroup == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oPages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.ToList()
				;

			oGroup.Pages.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varPage =
						oPages
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varPage != null)
					{
						oGroup.Pages.Add(varPage);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetSpecial()
				.ToList()
				;

			return (View(oGroup));
		}
	}
}
