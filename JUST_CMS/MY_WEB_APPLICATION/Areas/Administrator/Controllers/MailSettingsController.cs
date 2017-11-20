using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/11/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.MailSettings)]
	public partial class MailSettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public MailSettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varAllOfMailSettings =
				UnitOfWork.MailSettingsRepository.GetByCultureLcid(CultureLcid)
				.ToList()
				;

			return (View(varAllOfMailSettings));
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

			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			Models.MailSettings oMailSettings =
				new Models.MailSettings();
			// **************************************************

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "Name,BccAddresses,SenderDisplayName,SenderEmailAddress,SenderEmailPassword,SupportDisplayName,SupportEmailAddress,EmailSubjectTemplate,SmtpClientTimeout,SmtpClientPortNumber,SmtpClientEnableSsl,SmtpClientHostAddress,IsActive,Ordering,Description")]
			Models.MailSettings mailsettings)
		{
			// **************************************************
			mailsettings.CultureLcid = CultureLcid;

			mailsettings.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
			// **************************************************

			if (ModelState.IsValid)
			{
				UnitOfWork.MailSettingsRepository.Insert(mailsettings);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.MailSettings.Index()));
			}

			return (View(mailsettings));
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

			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oMailSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,Name,BccAddresses,SenderDisplayName,SenderEmailAddress,SenderEmailPassword,SupportDisplayName,SupportEmailAddress,EmailSubjectTemplate,SmtpClientTimeout,SmtpClientPortNumber,SmtpClientEnableSsl,SmtpClientHostAddress,IsActive,Ordering,Description")]
			Models.MailSettings mailsettings)
		{
			// **************************************************
			Models.MailSettings oOriginalMailSettings =
				UnitOfWork.MailSettingsRepository.GetById(mailsettings.Id);

			if (oOriginalMailSettings == null)
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
				oOriginalMailSettings.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalMailSettings.Name = mailsettings.Name;
				oOriginalMailSettings.Ordering = mailsettings.Ordering;
				oOriginalMailSettings.Description = mailsettings.Description;
				oOriginalMailSettings.BccAddresses = mailsettings.BccAddresses;
				oOriginalMailSettings.SenderDisplayName = mailsettings.SenderDisplayName;
				oOriginalMailSettings.SmtpClientTimeout = mailsettings.SmtpClientTimeout;
				oOriginalMailSettings.SenderEmailAddress = mailsettings.SenderEmailAddress;
				oOriginalMailSettings.SupportDisplayName = mailsettings.SupportDisplayName;
				oOriginalMailSettings.SenderEmailPassword = mailsettings.SenderEmailPassword;
				oOriginalMailSettings.SmtpClientEnableSsl = mailsettings.SmtpClientEnableSsl;
				oOriginalMailSettings.SupportEmailAddress = mailsettings.SupportEmailAddress;
				oOriginalMailSettings.EmailSubjectTemplate = mailsettings.EmailSubjectTemplate;
				oOriginalMailSettings.SmtpClientPortNumber = mailsettings.SmtpClientPortNumber;
				oOriginalMailSettings.SmtpClientHostAddress = mailsettings.SmtpClientHostAddress;
				// **************************************************

				// **************************************************
				oOriginalMailSettings.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalMailSettings.SetIsActive
					(mailsettings.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.MailSettingsRepository.Update(oOriginalMailSettings);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.MailSettings.Index()));
			}

			return (View(mailsettings));
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

			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oMailSettings));
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
			Models.MailSettings oMailSettings =
				UnitOfWork.MailSettingsRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oMailSettings == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.MailSettingsRepository.Delete(oMailSettings);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.MailSettings.Index()));
		}
	}
}
