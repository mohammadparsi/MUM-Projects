using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Cms.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/12/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Cms.Post)]
	public partial class PostController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PostController()
		{
		}

		//[System.Web.Mvc.HttpGet]
		//[Infrastructure.ProjectActionPermission
		//	(isVisibleJustForProgrammer: false,
		//	accessType: Models.Enums.AccessTypes.Special,
		//	keyName: Resources.Strings.ActionsKeys.Index)]
		//public virtual System.Web.Mvc.ActionResult Index()
		//{
		//	var varPosts =
		//		UnitOfWork.CmsUnitOfWork.PostRepository
		//		.GetByCultureLcid(CultureLcid)
		//		.ToList()
		//		;

		//	return (View(varPosts));
		//}

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
			keyName: Resources.Strings.ActionsKeys.GetPosts)]
		public virtual System.Web.Mvc.JsonResult GetPosts()
		{
			var varData =
				UnitOfWork.CmsUnitOfWork.PostRepository
				.GetByCultureLcid(CultureLcid)
				;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Cms.Post.IndexViewModel>(varData);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
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

			// **************************************************
			//Models.Cms.Post oPost =
			//	UnitOfWork.CmsUnitOfWork.PostRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id.Value);
			// **************************************************

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPost));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
			// **************************************************

			// **************************************************
			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.CategoryId =
				new System.Web.Mvc.SelectList(varPostCategories, "Id", "Name");
			// **************************************************

			// **************************************************
			Models.Cms.Post oPost =
				new Models.Cms.Post();

			oPost.AuthorUserId =
				Infrastructure.Sessions.AuthenticatedUser.Id;

			oPost.Author =
				Infrastructure.Sessions.AuthenticatedUser.FullName;
			// **************************************************

			return (View(oPost));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Include = "AuthorUserId,AccessTypeId,CategoryId,IsFeatured,IsCommentsEnabled,Password,Introduction,ThumbnailImageUrl,Body,DoesSearchEnginesIndexIt,DoesSearchEnginesFollowIt,Hits,Title,Author,Keywords,Classification,Description,Copyright,StartPublishingDateTime,FinishPublishingDateTime,IsActive,Ordering,IsVerified,IsDeleted,Tags")]
			Models.Cms.Post post)
		{
			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				post.CultureLcid = CultureLcid;

				post.AuthorUserId =
					Infrastructure.Sessions.AuthenticatedUser.Id;
				// **************************************************

				// **************************************************
				post.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);

				post.SetIsActive
					(post.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				post.SetIsDeleted
					(post.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				post.SetIsVerified
					(post.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Insert(post);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Post.Index()));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", post.AccessTypeId);
			// **************************************************

			// **************************************************
			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.CategoryId =
				new System.Web.Mvc.SelectList
					(varPostCategories, "Id", "Name", post.CategoryId);
			// **************************************************

			return (View(post));
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

			// **************************************************
			//Models.Cms.Post oPost =
			//	UnitOfWork.CmsUnitOfWork.PostRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id.Value);
			// **************************************************

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", oPost.AccessTypeId);
			// **************************************************

			// **************************************************
			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.CategoryId =
				new System.Web.Mvc.SelectList
					(varPostCategories, "Id", "Name", oPost.CategoryId);
			// **************************************************

			return (View(oPost));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Include = "Id,AuthorUserId,AccessTypeId,CategoryId,IsFeatured,IsCommentsEnabled,Password,Introduction,ThumbnailImageUrl,Body,DoesSearchEnginesIndexIt,DoesSearchEnginesFollowIt,Hits,Title,Author,Keywords,Classification,Description,Copyright,StartPublishingDateTime,FinishPublishingDateTime,IsActive,Ordering,IsVerified,IsDeleted,Tags")]
			Models.Cms.Post post)
		{
			// **************************************************
			//Models.Cms.Post oOriginalPost =
			//	UnitOfWork.CmsUnitOfWork.PostRepository.Get()
			//	.Where(current => current.Id == id)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Post oOriginalPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(post.Id);

			if (oOriginalPost == null)
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
				oOriginalPost.CultureLcid = CultureLcid;

				oOriginalPost.AuthorUserId =
					Infrastructure.Sessions.AuthenticatedUser.Id;
				// **************************************************

				// **************************************************
				oOriginalPost.Body = post.Body;
				oOriginalPost.Hits = post.Hits;
				oOriginalPost.Tags = post.Tags;
				oOriginalPost.Title = post.Title;
				oOriginalPost.Author = post.Author;
				oOriginalPost.Keywords = post.Keywords;
				oOriginalPost.Ordering = post.Ordering;
				oOriginalPost.Password = post.Password;
				oOriginalPost.Copyright = post.Copyright;
				oOriginalPost.CategoryId = post.CategoryId;
				oOriginalPost.IsFeatured = post.IsFeatured;
				oOriginalPost.Description = post.Description;
				oOriginalPost.AccessTypeId = post.AccessTypeId;
				oOriginalPost.Introduction = post.Introduction;
				oOriginalPost.Classification = post.Classification;
				oOriginalPost.IsCommentsEnabled = post.IsCommentsEnabled;
				oOriginalPost.ThumbnailImageUrl = post.ThumbnailImageUrl;
				oOriginalPost.StartPublishingDateTime = post.StartPublishingDateTime;
				oOriginalPost.DoesSearchEnginesIndexIt = post.DoesSearchEnginesIndexIt;
				oOriginalPost.FinishPublishingDateTime = post.FinishPublishingDateTime;
				oOriginalPost.DoesSearchEnginesFollowIt = post.DoesSearchEnginesFollowIt;
				// **************************************************

				// **************************************************
				oOriginalPost.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPost.SetIsActive
					(post.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				oOriginalPost.SetIsDeleted
					(post.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				oOriginalPost.SetIsVerified
					(post.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Update(oOriginalPost);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Cms.Post.Index()));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", post.AccessTypeId);
			// **************************************************

			// **************************************************
			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.CategoryId =
				new System.Web.Mvc.SelectList
					(varPostCategories, "Id", "Name", post.CategoryId);
			// **************************************************

			return (View(post));
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

			// **************************************************
			//Models.Cms.Post oPost =
			//	UnitOfWork.CmsUnitOfWork.PostRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id.Value);
			// **************************************************

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPost));
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
			// **************************************************
			//Models.Cms.Post oPost =
			//	UnitOfWork.CmsUnitOfWork.PostRepository.Get()
			//	.Where(current => current.Id == id.Value)
			//	.FirstOrDefault();

			// شده است، نبايد از دستور فوق استفاده کنيم Override ،GetById از آنجايی که تابع
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id);
			// **************************************************

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.CmsUnitOfWork.PostRepository.Delete(oPost);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Cms.Post.Index()));
		}
	}
}
