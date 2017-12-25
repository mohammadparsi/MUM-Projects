using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/12/06
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.ProjectArea)]
	public partial class ProjectAreaController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ProjectAreaController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository
				.GetViewModelByRole(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			return (View(varProjectAreas));
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

			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectArea));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.ProjectArea oProjectArea =
				new Models.ProjectArea();
			// **************************************************

			return (View(oProjectArea));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind
				(Include = "IsVisibleJustForProgrammer,Name,DisplayName,Description,IsActive,Ordering")]
			Models.ProjectArea projectArea)
		{
			// **************************************************
			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository
				.GetByName(projectArea.Name);

			if (oProjectArea != null)
			{
				// نام زيرسيستم تکراری است! لطفا از نام ديگری استفاده نماييد
				string strErrorMessage =
					Models.Resources.ProjectArea.Error001;

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				projectArea.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);
				// **************************************************

				UnitOfWork.ProjectAreaRepository.Insert(projectArea);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.ProjectArea.Index()));
			}

			return (View(projectArea));
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

			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectArea));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind
				(Include = "Id,IsVisibleJustForProgrammer,Name,DisplayName,Description,IsActive,Ordering")]
			Models.ProjectArea projectArea)
		{
			// **************************************************
			Models.ProjectArea oOriginalProjectArea =
				UnitOfWork.ProjectAreaRepository.GetById(projectArea.Id);

			if (oOriginalProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository
				.GetByNameExceptId(projectArea.Name, projectArea.Id);

			if (oProjectArea != null)
			{
				// نام زيرسيستم تکراری است! لطفا از نام ديگری استفاده نماييد
				string strErrorMessage =
					Models.Resources.ProjectArea.Error001;

				ModelState.AddModelError("Name", strErrorMessage);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalProjectArea.Ordering = projectArea.Ordering;
				oOriginalProjectArea.DisplayName = projectArea.DisplayName;
				oOriginalProjectArea.Description = projectArea.Description;
				// **************************************************

				// **************************************************
				oOriginalProjectArea.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalProjectArea.SetIsActive
					(projectArea.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalProjectArea.Name = projectArea.Name;
					oOriginalProjectArea.IsVisibleJustForProgrammer =
						projectArea.IsVisibleJustForProgrammer;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ProjectAreaRepository.Update(oOriginalProjectArea);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.ProjectArea.Index()));
			}

			return (View(projectArea));
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

			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oProjectArea));
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
			if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.ProjectAreaRepository.Delete(oProjectArea);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.ProjectArea.Index()));
		}
	}
}
