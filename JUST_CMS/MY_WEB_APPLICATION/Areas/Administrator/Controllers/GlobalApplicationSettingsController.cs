using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/01/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.GlobalApplicationSettings)]
	public partial class GlobalApplicationSettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public GlobalApplicationSettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit()
		{
			Models.GlobalApplicationSettings oGlobalApplicationSettings =
				UnitOfWork.GlobalApplicationSettingsRepository.Get()
				.FirstOrDefault();

			if (oGlobalApplicationSettings == null)
			{
				oGlobalApplicationSettings =
					Models.GlobalApplicationSettings.GetDefaultObject();

				UnitOfWork.GlobalApplicationSettingsRepository.Insert(oGlobalApplicationSettings);

				UnitOfWork.Save();
			}

			return (View(oGlobalApplicationSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,IsSslEnabled,MasterPassword,CurrentUserProfileLevel,SmsCenterNumber,SmsCenterUsername,SmsCenterPassword,AlexaCode,GoogleCode,TotalUserCount")]
			Models.GlobalApplicationSettings model)
		{
			// **************************************************
			Models.GlobalApplicationSettings oOriginalGlobalApplicationSettings =
				UnitOfWork.GlobalApplicationSettingsRepository.Get()
				.Where(current => current.Id == model.Id)
				.FirstOrDefault();

			if (oOriginalGlobalApplicationSettings == null)
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
				oOriginalGlobalApplicationSettings.IsSslEnabled = model.IsSslEnabled;
				oOriginalGlobalApplicationSettings.MasterPassword = model.MasterPassword;
				// **************************************************

				// **************************************************
				oOriginalGlobalApplicationSettings.AlexaCode = model.AlexaCode;
				oOriginalGlobalApplicationSettings.GoogleCode = model.GoogleCode;
				oOriginalGlobalApplicationSettings.TotalUserCount = model.TotalUserCount;
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalGlobalApplicationSettings.CurrentUserProfileLevel =
						model.CurrentUserProfileLevel;

					oOriginalGlobalApplicationSettings.SmsCenterNumber = model.SmsCenterNumber;
					oOriginalGlobalApplicationSettings.SmsCenterUsername = model.SmsCenterUsername;
					oOriginalGlobalApplicationSettings.SmsCenterPassword = model.SmsCenterPassword;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.GlobalApplicationSettingsRepository.Update(oOriginalGlobalApplicationSettings);

				UnitOfWork.Save();

				// با توجه به آخرين تغييرات، ديگر نيازی به دستور ذيل نمی‌باشد
				//Infrastructure.GlobalApplicationSettings.GetInstanceByCultureLcid();

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));
			}

			return (View(model));
		}
	}
}
