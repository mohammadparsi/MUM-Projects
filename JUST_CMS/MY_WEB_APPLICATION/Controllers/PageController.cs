using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/02/07
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Page)]
	public partial class PageController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PageController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Display)]
		public virtual System.Web.Mvc.ActionResult Display(string name)
		{
			if (name == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			name = name.Replace(" ", string.Empty);
			if (name == string.Empty)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Page oPage =
				UnitOfWork.CmsUnitOfWork.PageRepository
				.GetByCultureLcidAndName(CultureLcid, name);

			if (oPage == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
			{
				// در صورتی که کاربر، کاربر عادی نباشد، دستور ذيل نبايد اجرا شود
				//oPage.VisitCount++;

				return (View(oPage));
			}

			if (oPage.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPage.Layout.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPage.AccessTypeEnum != Models.Enums.AccessTypes.Public)
			{
				if (Infrastructure.AuthenticatedUser.IsAuthenticated == false)
				{
					string strReturnUrl =
						string.Format("/Page/{0}", oPage.Name);

					return (RedirectToAction(MVC.Account.Login(strReturnUrl)));
				}
				else
				{
					if (oPage.AccessTypeEnum == Models.Enums.AccessTypes.Special)
					{
						// دستور ذيل تعداد گروه‌هايی را برمی‌گرداند که
						// فعال بوده و کاربر جاری در آن عضو بوده
						// و در همان گروه‌ها، اين صفحه معرفی شده است
						int intCount =
							UnitOfWork.GroupRepository.Get()
							.Where(current => current.IsActive)
							.Where(current => current.Pages.Any
								(page => page.Id == oPage.Id))
							.Where(current => current.Users.Any
								(user => user.Id == Infrastructure.Sessions.AuthenticatedUser.Id))
							.Count()
							;

						if (intCount == 0)
						{
							// دستور ذيل شناسايی می کند که
							// آيا کاربر مستقيما به صفحه دسترسی دارد يا خير؟
							intCount =
								UnitOfWork.UserRepository.Get()
								.Where(current => current.Id ==
									Infrastructure.Sessions.AuthenticatedUser.Id)
								.Where(current => current.Pages.Any
									(page => page.Id == oPage.Id))
								.Count()
								;

							if (intCount == 0)
							{
								return (RedirectToAction
									(MVC.Error.Display(System.Net.HttpStatusCode.Unauthorized)));
							}
						}
					}
				}
			}

			// **************************************************
			ViewBag.Title = oPage.Title;
			ViewBag.Author = oPage.Author;
			ViewBag.Keywords = oPage.Keywords;
			ViewBag.Copyright = oPage.Copyright;
			ViewBag.Description = oPage.Description;
			ViewBag.Classification = oPage.Classification;

			if (oPage.DoesSearchEnginesIndexIt)
			{
				if (oPage.DoesSearchEnginesFollowIt)
				{
					ViewBag.Robots = "index,follow";
				}
				else
				{
					ViewBag.Robots = "index,nofollow";
				}
			}
			else
			{
				if (oPage.DoesSearchEnginesFollowIt)
				{
					ViewBag.Robots = "noindex,follow";
				}
				else
				{
					ViewBag.Robots = "noindex,nofollow";
				}
			}
			// **************************************************

			// **************************************************
			oPage.Hits++;

			UnitOfWork.Save();
			// **************************************************

			return (View(oPage));
		}
	}
}
