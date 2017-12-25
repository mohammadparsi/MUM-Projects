using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/02/08
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.Culture)]
	public partial class CultureController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public CultureController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varCultures =
				UnitOfWork.CultureRepository.Get()
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				.ToList()
				;

			return (View(varCultures));
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

			Models.Culture oCulture =
				UnitOfWork.CultureRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oCulture == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oCulture));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Lcid,Name,Description,NativeName,DisplayName,IsSystem,Ordering,InsertDateTime,CreatorUserId,UpdateDateTime,EditorUserId,IsActive,ActiveDateTime,ActivatorUserId,IsVerified,VerifyDateTime,VerifierUserId,IsDeleted,DeleteDateTime,RemoverUserId,MasterDataCode")]
			Models.Culture culture)
		{
			// **************************************************
			Models.Culture oOriginalCulture =
				UnitOfWork.CultureRepository.GetById(culture.Id);

			if (oOriginalCulture == null)
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
				oOriginalCulture.Ordering = culture.Ordering;
				oOriginalCulture.DisplayName = culture.DisplayName;
				oOriginalCulture.Description = culture.Description;
				// **************************************************

				// **************************************************
				oOriginalCulture.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalCulture.SetIsActive
					(culture.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CultureRepository.Update(oOriginalCulture);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.Culture.Index()));
			}

			return (View(culture));
		}
	}
}
