using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/14
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.User)]
	public partial class UserController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			if (Infrastructure.ApplicationSettings.Instance.CanUserSearchTheOthers == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.GetUsers)]
		public virtual System.Web.Mvc.JsonResult GetUsers()
		{
			if (Infrastructure.ApplicationSettings.Instance.CanUserSearchTheOthers == false)
			{
				return (null);
			}

			var varUsers =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsProfilePublic)
				.Where(current => current.ProfileLevel != 0)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsEmailAddressVerified)
				.Where(current => current.IsAccountTerminated == false)
				.OrderByDescending(current => current.LastLoginDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.User.IndexViewModel()
					{
						Id = current.Id,
						IsVerified = current.IsVerified,
						Gender = current.Gender.Name1,
						FirstName = current.FirstName,
						LastName = current.LastName,
						Role = current.Role.Name,
						HasAvatar = current.HasAvatar
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.User.IndexViewModel>(varUsers);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		private string FixText(string text)
		{
			if (text == null)
			{
				return (string.Empty);
			}

			text = text.Trim();
			if (text == string.Empty)
			{
				return (string.Empty);
			}

			while (text.Contains("  "))
			{
				text = text.Replace("  ", " ");
			}

			return (text);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Search)]
		public virtual System.Web.Mvc.JsonResult Search
			(ViewModels.Areas.Administrator.User.SearchViewModel viewModel)
		{
			if (Infrastructure.ApplicationSettings.Instance.CanUserSearchTheOthers == false)
			{
				return (null);
			}

			var varPeople =
				UnitOfWork.UserRepository.Get()
				;

			// **************************************************
			viewModel.FirstName =
				FixText(viewModel.FirstName);

			if (viewModel.FirstName != string.Empty)
			{
				varPeople =
					varPeople
					.Where(current => current.FirstName.Contains(viewModel.FirstName))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.LastName =
				FixText(viewModel.LastName);

			if (viewModel.LastName != string.Empty)
			{
				varPeople =
					varPeople
					.Where(current => current.LastName.Contains(viewModel.LastName))
					;
			}
			// **************************************************

			var varNewPeople =
				varPeople
				.Where(current => current.IsActive)
				.Where(current => current.IsProfilePublic)
				.Where(current => current.ProfileLevel != 0)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsEmailAddressVerified)
				.Where(current => current.IsAccountTerminated == false)
				.OrderByDescending(current => current.LastLoginDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.User.IndexViewModel()
					{
						Id = current.Id,
						IsVerified = current.IsVerified,
						Gender = current.Gender.Name1,
						FirstName = current.FirstName,
						LastName = current.LastName,
						Role = current.Role.Name,
						HasAvatar = current.HasAvatar
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.User.IndexViewModel>(varNewPeople);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
