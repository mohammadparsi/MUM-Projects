using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/04/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Post)]
	public partial class PostController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PostController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Display)]
		public virtual System.Web.Mvc.ActionResult Display(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// در صورتی که کاربر جزء مسوولين پايگاه باشد
			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
			{
				// در صورتی که کاربر، کاربر عادی نباشد، دستور ذيل نبايد اجرا شود
				//oPost.Hits++;

				return (View(oPost));
			}

			// در صورتی که کاربر همان شخصی باشد که مطلب را درج کرده
			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User) &&
				(oPost.CreatorUserId == Infrastructure.Sessions.AuthenticatedUser.Id))
			{
				// در صورتی که کاربر، کاربر عادی نباشد، دستور ذيل نبايد اجرا شود
				//oPost.Hits++;

				return (View(oPost));
			}

			if (oPost.CreatorUser.IsAccountTerminated)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPost.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPost.Category.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			//if (oPost.IsPublic == false)
			//{
			//	if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			//	{
			//		if (oPost.IsJustAuthenticationEnough == false)
			//		{
			//			// دستور ذيل، تعداد گروه‌هايی را برمی‌گرداند که
			//			// فعال بوده و کاربرجاری در آنها عضو بوده و
			//			// و در همان گروه‌ها اين صفحه را نيز دربردارند
			//			int intCount =
			//				UnitOfWork.GroupRepository.Get()
			//				.Where(current => current.IsActive)
			//				.Where(current => current.Pages.Any
			//					(page => page.Id == oPost.Id))
			//				.Where(current => current.Users.Any
			//					(user => user.Id == Infrastructure.Sessions.AuthenticatedUser.Id))
			//				.Count()
			//				;

			//			if (intCount == 0)
			//			{
			//				intCount =
			//					UnitOfWork.UserRepository.Get()
			//					.Where(current => current.Id ==
			//						Infrastructure.Sessions.AuthenticatedUser.Id)
			//					.Where(current => current.Pages.Any(page => page.Id == oPost.Id))
			//					.Count()
			//					;

			//				if (intCount == 0)
			//				{
			//					return (RedirectToAction(MVC.Error.AccessDenied()));
			//				}
			//			}
			//		}
			//	}
			//	else
			//	{
			//		string strReturnUrl =
			//			string.Format("/Page/{0}", oPost.Name);

			//		return (RedirectToAction(MVC.Account.Login(strReturnUrl)));
			//	}
			//}

			// **************************************************
			ViewBag.Title = oPost.Title;
			ViewBag.Author = oPost.Author;
			ViewBag.Keywords = oPost.Keywords;
			ViewBag.Copyright = oPost.Copyright;
			ViewBag.Description = oPost.Description;
			ViewBag.Classification = oPost.Classification;

			if (oPost.DoesSearchEnginesIndexIt)
			{
				if (oPost.DoesSearchEnginesFollowIt)
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
				if (oPost.DoesSearchEnginesFollowIt)
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
			oPost.Hits++;

			UnitOfWork.Save();
			// **************************************************

			return (View(oPost));
		}


		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.DisplayForRss)]
		public virtual System.Web.Mvc.ActionResult DisplayForRss(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// در صورتی که کاربر جزء مسوولين پايگاه باشد
			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
			{
				// در صورتی که کاربر، کاربر عادی نباشد، دستور ذيل نبايد اجرا شود
				//oPost.Hits++;

				return (View(oPost));
			}

			// در صورتی که کاربر همان شخصی باشد که مطلب را درج کرده
			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.User) &&
				(oPost.CreatorUserId == Infrastructure.Sessions.AuthenticatedUser.Id))
			{
				// در صورتی که کاربر، کاربر عادی نباشد، دستور ذيل نبايد اجرا شود
				//oPost.Hits++;

				return (View(oPost));
			}

			if (oPost.CreatorUser.IsAccountTerminated)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPost.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			if (oPost.Category.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			//if (oPost.IsPublic == false)
			//{
			//	if (Infrastructure.AuthenticatedUser.IsAuthenticated)
			//	{
			//		if (oPost.IsJustAuthenticationEnough == false)
			//		{
			//			// دستور ذيل، تعداد گروه‌هايی را برمی‌گرداند که
			//			// فعال بوده و کاربرجاری در آنها عضو بوده و
			//			// و در همان گروه‌ها اين صفحه را نيز دربردارند
			//			int intCount =
			//				UnitOfWork.GroupRepository.Get()
			//				.Where(current => current.IsActive)
			//				.Where(current => current.Pages.Any
			//					(page => page.Id == oPost.Id))
			//				.Where(current => current.Users.Any
			//					(user => user.Id == Infrastructure.Sessions.AuthenticatedUser.Id))
			//				.Count()
			//				;

			//			if (intCount == 0)
			//			{
			//				intCount =
			//					UnitOfWork.UserRepository.Get()
			//					.Where(current => current.Id ==
			//						Infrastructure.Sessions.AuthenticatedUser.Id)
			//					.Where(current => current.Pages.Any(page => page.Id == oPost.Id))
			//					.Count()
			//					;

			//				if (intCount == 0)
			//				{
			//					return (RedirectToAction(MVC.Error.AccessDenied()));
			//				}
			//			}
			//		}
			//	}
			//	else
			//	{
			//		string strReturnUrl =
			//			string.Format("/Page/{0}", oPost.Name);

			//		return (RedirectToAction(MVC.Account.Login(strReturnUrl)));
			//	}
			//}

			// **************************************************
			ViewBag.Title = oPost.Title;
			ViewBag.Author = oPost.Author;
			ViewBag.Keywords = oPost.Keywords;
			ViewBag.Copyright = oPost.Copyright;
			ViewBag.Description = oPost.Description;
			ViewBag.Classification = oPost.Classification;

			if (oPost.DoesSearchEnginesIndexIt)
			{
				if (oPost.DoesSearchEnginesFollowIt)
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
				if (oPost.DoesSearchEnginesFollowIt)
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
			oPost.Hits++;

			UnitOfWork.Save();
			// **************************************************

			return (View(oPost));
		}
	}
}
