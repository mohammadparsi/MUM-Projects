using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.UserLoginLog)]
	public partial class UserLoginLogController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserLoginLogController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.GetData)]
		public virtual System.Web.Mvc.JsonResult GetData()
		{
			var varData =
				UnitOfWork.UserLoginLogRepository.Get()
				.OrderByDescending(current => current.LoginDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.UserLoginLog.IndexViewModel()
					{
						Id = current.Id,
						IP = current.IP,
						IsHidden = current.IsHidden,
						LoginDateTime = current.LoginDateTime,
						LogoutDateTime = current.LogoutDateTime,

						UserId = current.UserId,
						LastName = current.User.LastName,
						Gender = current.User.Gender.Name2,
						FirstName = current.User.FirstName,
						EmailAddress = current.User.EmailAddress,
						CellPhoneNumber = current.User.CellPhoneNumber
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.UserLoginLog.IndexViewModel>(varData);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.DisplayOnlineUsers)]
		public virtual System.Web.Mvc.ViewResult DisplayOnlineUsers()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.GetOnlineUsers)]
		public virtual System.Web.Mvc.JsonResult GetOnlineUsers()
		{
			var varData =
				UnitOfWork.UserLoginLogRepository.Get()
				.Where(current => current.LogoutDateTime.HasValue == false)
				.OrderByDescending(current => current.LoginDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.UserLoginLog.GetOnlineUsersViewModel()
					{
						Id = current.Id,
						IP = current.IP,
						IsHidden = current.IsHidden,
						LoginDateTime = current.LoginDateTime,

						UserId = current.UserId,
						LastName = current.User.LastName,
						Gender = current.User.Gender.Name2,
						FirstName = current.User.FirstName,
						EmailAddress = current.User.EmailAddress,
						CellPhoneNumber = current.User.CellPhoneNumber
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.UserLoginLog.GetOnlineUsersViewModel>(varData);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.DisplayUserLoginLogs)]
		public virtual System.Web.Mvc.ActionResult DisplayUserLoginLogs(System.Guid? userId)
		{
			if (userId.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.GetById(userId.Value)
				;

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User)
			{
				return (View(userId.Value));
			}
			else
			{
				if (Infrastructure.Sessions.AuthenticatedUser.Id == userId.Value)
				{
					return (View(userId.Value));
				}
				else
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
				}
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.GetUserLoginLogs)]
		public virtual System.Web.Mvc.JsonResult GetUserLoginLogs(System.Guid? userId)
		{
			if (userId.HasValue == false)
			{
				return (null);
			}

			Models.User oUser =
				UnitOfWork.UserRepository.GetById(userId.Value)
				;

			if (oUser == null)
			{
				return (null);
			}

			if ((Infrastructure.Sessions.AuthenticatedUser.Id != oUser.Id) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User))
			{
				return (null);
			}

			var varData =
				UnitOfWork.UserLoginLogRepository.Get()
				.Where(current => current.UserId == userId.Value)
				.OrderByDescending(current => current.LoginDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.UserLoginLog.GetUserLoginLogsViewModel()
					{
						Id = current.Id,
						IP = current.IP,
						IsHidden = current.IsHidden,
						LoginDateTime = current.LoginDateTime,
						LogoutDateTime = current.LogoutDateTime,
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.UserLoginLog.GetUserLoginLogsViewModel>(varData);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
