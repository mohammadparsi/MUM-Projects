//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
//{
//	/// <summary>
//	/// Version: 1.0.2
//	/// Update Date: 1392/07/06
//	/// Developer: Mr. Dariush Tasdighi
//	/// </summary>
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.Administrator.Photo)]
//	public partial class PhotoController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public PhotoController()
//		{
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Details)]
//		public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
//		{
//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.GetById(id);

//			if (oPhoto == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPhoto));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(System.Guid parentPhotoAlbumId)
//		{
//			// **************************************************
//			Models.Cms.PhotoAlbum oParentPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == parentPhotoAlbumId)
//				.FirstOrDefault();

//			if (oParentPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.ParentPhotoAlbum = oParentPhotoAlbum;
//			// **************************************************

//			// **************************************************
//			Models.Cms.Photo oPhoto =
//				new Models.Cms.Photo();

//			oPhoto.Ordering = 0;
//			oPhoto.PhotoAlbumId = parentPhotoAlbumId;
//			// **************************************************

//			return (View(oPhoto));
//		}

//		[System.Web.Mvc.HttpPost]
//		public virtual System.Web.Mvc.ActionResult Display
//			(System.Guid id, int? width = null, int? height = null)
//		{
//			// **************************************************
//			string strFileName = string.Empty;
//			string strPathName = string.Empty;
//			string strRootRelativePathName = string.Empty;

//			System.IO.MemoryStream oMemoryStream = null;
//			// **************************************************

//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.GetById(id);

//			if (oPhoto == null)
//			{
//				return (Infrastructure.Image.NoPhoto(width, height));
//			}

//			strFileName =
//				string.Format("{0}{1}", oPhoto.Id, oPhoto.Extension);

//			strRootRelativePathName =
//				string.Format("~/App_Data/Photos/{0}", strFileName);

//			strPathName =
//				Server.MapPath(strRootRelativePathName);

//			if (System.IO.File.Exists(strPathName) == false)
//			{
//				return (Infrastructure.Image.NoPhoto(width, height));
//			}

//			oMemoryStream =
//				Infrastructure.Image.GetPhoto(strPathName, width, height);

//			return (File(oMemoryStream.ToArray(),
//				contentType: "image/png", fileDownloadName: strFileName));
//		}

//		private string CheckPictrueValidation(System.Web.HttpPostedFileBase postedFile)
//		{
//			string strErrorMessage = string.Empty;

//			if (postedFile.ContentLength == 0)
//			{
//				strErrorMessage = "فايل به درستی آپلود نشده است!";
//				return (strErrorMessage);
//			}

//			string strExtension =
//				System.IO.Path.GetExtension(postedFile.FileName).ToLower();

//			switch (strExtension)
//			{
//				case ".gif":
//				case ".png":
//				case ".jpg":
//				case ".jpeg":
//				{
//					break;
//				}

//				default:
//				{
//					strErrorMessage =
//						"برای فايل‌های عکس تنها فرمت های JPG, GIF, PNG قابل قبول می باشد!";
//					return (strErrorMessage);
//				}
//			}

//			string strContentType = postedFile.ContentType;

//			switch (strContentType.ToLower())
//			{
//				case "image/gif":
//				case "image/png":
//				case "image/jpeg":
//				case "image/pjpeg":
//				{
//					break;
//				}

//				default:
//				{
//					strErrorMessage =
//						"برای فايل‌های عکس تنها فرمت های JPG, GIF, PNG قابل قبول می باشد!";
//					return (strErrorMessage);
//				}
//			}

//			return (strErrorMessage);
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Create)]
//		public virtual System.Web.Mvc.ActionResult Create(Models.Cms.Photo photo)
//		{
//			// **************************************************
//			System.Web.HttpPostedFileBase oPostedFile = null;

//			if (Request.Files.Count == 0)
//			{
//				ModelState.AddModelError
//					(string.Empty, "فايلی برای آپلود مشخص نشده است!");
//			}
//			else
//			{
//				oPostedFile = Request.Files[0];

//				string strErrorMessage =
//					CheckPictrueValidation(oPostedFile);

//				if (string.IsNullOrEmpty(strErrorMessage) == false)
//				{
//					ModelState.AddModelError(string.Empty, strErrorMessage);
//				}
//			}
//			// **************************************************

//			if (ModelState.IsValid)
//			{
//				// **************************************************
//				photo.InsertDateTime = Infrastructure.Utility.Now;
//				photo.UpdateDateTime = Infrastructure.Utility.Now;

//				photo.OriginalName =
//					System.IO.Path.GetFileName(oPostedFile.FileName);
//				// **************************************************

//				// **************************************************
//				photo.Extension =
//					System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

//				System.Drawing.Image oImage =
//					System.Drawing.Image.FromStream(oPostedFile.InputStream);

//				photo.Width = oImage.Width;
//				photo.Height = oImage.Height;
//				photo.Length = oPostedFile.ContentLength;

//				photo.MD5 =
//					Dtx.Security.Hashing.GetMD5(oPostedFile.FileName);

//				photo.Sha1 =
//					Dtx.Security.Hashing.GetSha1(oPostedFile.FileName);

//				string strRootRelativePathName =
//					string.Format("~/App_Data/Photos/{0}{1}", photo.Id, photo.Extension);

//				string strPathName =
//					Server.MapPath(strRootRelativePathName);

//				oPostedFile.SaveAs(strPathName);
//				// **************************************************

//				UnitOfWork.CmsUnitOfWork.PhotoRepository.Insert(photo);

//				UnitOfWork.Save();

//				return (RedirectToAction
//					(MVC.Administrator.PhotoAlbum.Index(photo.PhotoAlbumId)));
//			}

//			// **************************************************
//			Models.Cms.PhotoAlbum oParentPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == photo.PhotoAlbumId)
//				.FirstOrDefault();

//			if (oParentPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			ViewBag.ParentPhotoAlbum = oParentPhotoAlbum;
//			// **************************************************

//			return (View(photo));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(System.Guid id)
//		{
//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.GetById(id);

//			if (oPhoto == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPhoto));
//		}

//		[System.Web.Mvc.HttpPost]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Edit)]
//		public virtual System.Web.Mvc.ActionResult Edit(Models.Cms.Photo photo)
//		{
//			if (ModelState.IsValid)
//			{
//				// **************************************************
//				Models.Cms.Photo oPhoto =
//					UnitOfWork.CmsUnitOfWork.PhotoRepository.GetById(photo.Id);

//				oPhoto.UpdateDateTime = Infrastructure.Utility.Now;

//				oPhoto.Tags = photo.Tags;
//				oPhoto.Title = photo.Title;
//				oPhoto.IsActive = photo.IsActive;
//				oPhoto.Keywords = photo.Keywords;
//				oPhoto.Description = photo.Description;
//				oPhoto.Ordering = photo.Ordering;
//				oPhoto.DownloadCount = photo.DownloadCount;
//				oPhoto.IsDirectAccessible = photo.IsDirectAccessible;
//				oPhoto.IsAccessibleFromTheOtherSites = photo.IsAccessibleFromTheOtherSites;
//				// **************************************************

//				UnitOfWork.CmsUnitOfWork.PhotoRepository.Update(oPhoto);

//				UnitOfWork.Save();

//				return (RedirectToAction
//					(MVC.Administrator.PhotoAlbum.Index(oPhoto.PhotoAlbumId)));
//			}

//			return (View(photo));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult Delete(System.Guid id)
//		{
//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.GetById(id);

//			if (oPhoto == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			return (View(oPhoto));
//		}

//		[System.Web.Mvc.HttpPost]
//		[System.Web.Mvc.ActionName("Delete")]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Special,
//			keyName: Resources.Strings.ActionsKeys.Delete)]
//		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
//		{
//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.GetById(id);

//			if (oPhoto == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			UnitOfWork.CmsUnitOfWork.PhotoRepository.Delete(oPhoto);

//			UnitOfWork.Save();

//			return (RedirectToAction
//				(MVC.Administrator.PhotoAlbum.Index(oPhoto.PhotoAlbumId)));
//		}
//	}
//}
