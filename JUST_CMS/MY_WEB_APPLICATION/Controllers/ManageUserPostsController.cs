using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/06/04
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.ManageUserPosts)]
	public partial class ManageUserPostsController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ManageUserPostsController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.DisplayMyPosts)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			var varPosts =
				UnitOfWork.CmsUnitOfWork.PostRepository.Get()
				.Include(current => current.Category)
				.Include(current => current.AccessType)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == CultureLcid)
				.Where(current => current.AuthorUserId == Infrastructure.Sessions.AuthenticatedUser.Id)
				.OrderByDescending(current => current.IsFeatured)
				.ThenBy(current => current.Ordering)
				.ThenByDescending(current => current.UpdateDateTime)
				.ToList()
				;

			return (View(varPosts));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
		{
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id);

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oPost.AuthorUserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Unauthorized)));
			}

			if (oPost.IsDeleted)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPost));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

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
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.Post post)
		{
			// **************************************************
			post.CultureLcid = CultureLcid;

			post.SetInsertDateTime(Infrastructure.Sessions.AuthenticatedUser.Id);
			// **************************************************

			if (ModelState.IsValid)
			{
				UnitOfWork.CmsUnitOfWork.PostRepository.Insert(post);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ManageUserPosts.Index()));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.AccessTypeId =
				new System.Web.Mvc.SelectList
					(varAccessTypes, "Id", "Name", post.AccessTypeId);
			// **************************************************

			// **************************************************
			var varPostCategories =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.CategoryId =
				new System.Web.Mvc.SelectList
					(varPostCategories, "Id", "Name", post.CategoryId);
			// **************************************************

			return (View(post));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid id)
		{
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id);

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oPost.AuthorUserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Unauthorized)));
			}

			if (oPost.IsDeleted)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

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
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.Post post)
		{
			// **************************************************
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
				// **************************************************

				// **************************************************
				oOriginalPost.Body = post.Body;
				oOriginalPost.Tags = post.Tags;
				oOriginalPost.Title = post.Title;
				oOriginalPost.Author = post.Author;
				oOriginalPost.Keywords = post.Keywords;
				oOriginalPost.Copyright = post.Copyright;
				oOriginalPost.CategoryId = post.CategoryId;
				oOriginalPost.IsFeatured = post.IsFeatured;
				oOriginalPost.Description = post.Description;
				oOriginalPost.AccessTypeId = post.AccessTypeId;
				oOriginalPost.Introduction = post.Introduction;
				oOriginalPost.Classification = post.Classification;
				oOriginalPost.ThumbnailImageUrl = post.ThumbnailImageUrl;
				oOriginalPost.IsCommentsEnabled = post.IsCommentsEnabled;
				oOriginalPost.DoesSearchEnginesIndexIt = post.DoesSearchEnginesIndexIt;
				oOriginalPost.DoesSearchEnginesFollowIt = post.DoesSearchEnginesFollowIt;
				// **************************************************

				// **************************************************
				oOriginalPost.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id);

				oOriginalPost.SetIsActive
					(false, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.CmsUnitOfWork.PostRepository.Update(oOriginalPost);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.ManageUserPosts.Index()));
			}

			// **************************************************
			var varAccessTypes =
				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

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
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid id)
		{
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id);

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oPost.AuthorUserId != Infrastructure.Sessions.AuthenticatedUser.Id)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Unauthorized)));
			}

			if (oPost.IsDeleted)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oPost));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
		{
			Models.Cms.Post oPost =
				UnitOfWork.CmsUnitOfWork.PostRepository.GetById(id);

			if (oPost == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			oPost.SetIsDeleted
				(true, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.ManageUserPosts.Index()));
		}
	}
}
