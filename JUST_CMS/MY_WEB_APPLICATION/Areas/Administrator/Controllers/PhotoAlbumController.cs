//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
//{
//	/// <summary>
//	/// Version: 1.0.1
//	/// Update Date: 1392/06/18
//	/// 
//	/// </summary>
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.Administrator.PhotoAlbum)]
//	public partial class PhotoAlbumController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public PhotoAlbumController()
//		{
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Index)]
//		public virtual System.Web.Mvc.ActionResult Index(System.Guid? parentPhotoAlbumId = null)
//		{
//			if (parentPhotoAlbumId.HasValue == false)
//			{
//				ViewBag.ParentPhotoAlbum = null;

//				var varPhotoAlbums =
//					UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//					.Where(current => current.CultureLcid == CultureLcid)
//					.Where(current => current.ParentPhotoAlbumId.HasValue == false)
//					.OrderBy(current => current.Ordering)
//					.ThenByDescending(current => current.UpdateDateTime)
//					.ToList()
//					;

//				return (View(varPhotoAlbums));
//			}
//			else
//			{
//				Models.Cms.PhotoAlbum oParentPhotoAlbum =
//					UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//					.Where(current => current.Id == parentPhotoAlbumId.Value)
//					.FirstOrDefault();

//				if (oParentPhotoAlbum == null)
//				{
//					return (RedirectToAction
//						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//				}

//				ViewBag.ParentPhotoAlbum = oParentPhotoAlbum;

//				var varPhotoAlbums =
//					UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//					.Where(current => current.CultureLcid == CultureLcid)
//					.Where(current => current.ParentPhotoAlbumId.Value == parentPhotoAlbumId.Value)
//					.OrderBy(current => current.Ordering)
//					.ThenByDescending(current => current.UpdateDateTime)
//					.ToList()
//					;

//				return (View(varPhotoAlbums));
//			}
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Details)]
//		public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
//		{
//			Models.Cms.PhotoAlbum oPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPhotoAlbum));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(System.Guid? parentPhotoAlbumId = null)
//		{
//			if (parentPhotoAlbumId.HasValue == false)
//			{
//				ViewBag.ParentPhotoAlbum = null;
//			}
//			else
//			{
//				Models.Cms.PhotoAlbum oParentPhotoAlbum =
//					UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//					.Where(current => current.Id == parentPhotoAlbumId.Value)
//					.FirstOrDefault();

//				if (oParentPhotoAlbum == null)
//				{
//					return (RedirectToAction
//						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//				}

//				ViewBag.ParentPhotoAlbum = oParentPhotoAlbum;
//			}

//			// **************************************************
//			Models.Cms.PhotoAlbum oPhotoAlbum =
//				new Models.Cms.PhotoAlbum();

//			oPhotoAlbum.Ordering = 0;
//			oPhotoAlbum.ParentPhotoAlbumId = parentPhotoAlbumId;
//			// **************************************************

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList(varAccessTypes, "Id", "Name");
//			// **************************************************

//			return (View(oPhotoAlbum));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.PhotoAlbum photoAlbum)
//		{
//			// **************************************************
//			photoAlbum.InsertDateTime = Infrastructure.Utility.Now;
//			photoAlbum.UpdateDateTime = Infrastructure.Utility.Now;
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Insert(photoAlbum);

//				UnitOfWork.Save();

//				return (RedirectToAction
//					(MVC.Administrator.PhotoAlbum.Index(photoAlbum.ParentPhotoAlbumId)));
//			}

//			if (photoAlbum.ParentPhotoAlbumId.HasValue == false)
//			{
//				ViewBag.ParentPhotoAlbum = null;
//			}
//			else
//			{
//				Models.Cms.PhotoAlbum oParentPhotoAlbum =
//					UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//					.Where(current => current.Id == photoAlbum.ParentPhotoAlbumId.Value)
//					.FirstOrDefault();

//				if (oParentPhotoAlbum == null)
//				{
//					return (RedirectToAction
//						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//				}

//				ViewBag.ParentPhotoAlbum = oParentPhotoAlbum;
//			}

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", photoAlbum.AccessTypeId);
//			// **************************************************

//			return (View(photoAlbum));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(System.Guid id)
//		{
//			Models.Cms.PhotoAlbum oPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", oPhotoAlbum.AccessTypeId);
//			// **************************************************

//			return (View(oPhotoAlbum));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.PhotoAlbum photoAlbum)
//		{
//			photoAlbum.UpdateDateTime = Infrastructure.Utility.Now;

//			if (ModelState.IsValid)
//			{
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Update(photoAlbum);

//				UnitOfWork.Save();

//				return (RedirectToAction
//					(MVC.Administrator.PhotoAlbum.Index(photoAlbum.ParentPhotoAlbumId)));
//			}

//			// **************************************************
//			var varAccessTypes =
//				UnitOfWork.AccessTypeRepository.GetActiveByCultureLcid(CultureLcid);

//			ViewBag.AccessTypeId =
//				new System.Web.Mvc.SelectList
//					(varAccessTypes, "Id", "Name", photoAlbum.AccessTypeId);
//			// **************************************************

//			return (View(photoAlbum));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult Delete(System.Guid id)
//		{
//			Models.Cms.PhotoAlbum oPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPhotoAlbum));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ActionName("Delete")]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
//		{
//			Models.Cms.PhotoAlbum oPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			System.Guid? sParentPhotoAlbumId =
//				oPhotoAlbum.ParentPhotoAlbumId;

//			if (oPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Delete(oPhotoAlbum);

//			UnitOfWork.Save();

//			return (RedirectToAction
//				(MVC.Administrator.PhotoAlbum.Index(sParentPhotoAlbumId)));
//		}
//	}
//}
