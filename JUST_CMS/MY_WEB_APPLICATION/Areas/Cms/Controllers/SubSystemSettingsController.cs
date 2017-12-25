using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		resourceType: typeof(Models.Resources.Cms.SubSystemSettings),
		keyName: Models.Resources.Cms.Strings.SubSystemSettingsKeys.EntityName)]
	public partial class SubSystemSettingsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public SubSystemSettingsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit()
		{
			Models.Cms.SubSystemSettings oSubSystemSettings =
				UnitOfWork.CmsUnitOfWork.SubSystemSettingsRepository.Get()
				.Where(current => current.CultureLcid == CultureLcid)
				.FirstOrDefault();

			if (oSubSystemSettings == null)
			{
				oSubSystemSettings =
					Models.Cms.SubSystemSettings.GetDefaultObject();

				UnitOfWork.CmsUnitOfWork.SubSystemSettingsRepository.Insert(oSubSystemSettings);

				UnitOfWork.Save();
			}

			return (View(oSubSystemSettings));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.SubSystemSettings subSystemSettings)
		{
			// **************************************************
			Models.Cms.SubSystemSettings oOriginalSubSystemSettings =
				UnitOfWork.CmsUnitOfWork.SubSystemSettingsRepository.Get()
				.Where(current => current.Id == subSystemSettings.Id)
				.FirstOrDefault();

			if (oOriginalSubSystemSettings == null)
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
				oOriginalSubSystemSettings.CultureLcid = CultureLcid;
				// **************************************************

				// **************************************************
				oOriginalSubSystemSettings.HomePagePostCount = subSystemSettings.HomePagePostCount;

				oOriginalSubSystemSettings.RssImageUrl = subSystemSettings.RssImageUrl;
				oOriginalSubSystemSettings.RssImageLink = subSystemSettings.RssImageLink;
				oOriginalSubSystemSettings.RssImageTitle = subSystemSettings.RssImageTitle;

				oOriginalSubSystemSettings.RssItemCount = subSystemSettings.RssItemCount;
				oOriginalSubSystemSettings.RssRootCreatorUrl = subSystemSettings.RssRootCreatorUrl;

				oOriginalSubSystemSettings.RssChannelLink = subSystemSettings.RssChannelLink;
				oOriginalSubSystemSettings.RssChannelTitle = subSystemSettings.RssChannelTitle;
				oOriginalSubSystemSettings.RssChannelDescription = subSystemSettings.RssChannelDescription;
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.SubSystemSettingsRepository.Update(oOriginalSubSystemSettings);

				UnitOfWork.Save();

				// با توجه به آخرين تغييرات، ديگر نيازی به دستور ذيل نمی‌باشد
				//Infrastructure.ApplicationSettings.GetInstanceByCultureLcid();

				PageMessages.Add
					(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));
			}

			return (View(subSystemSettings));
		}
	}
}
