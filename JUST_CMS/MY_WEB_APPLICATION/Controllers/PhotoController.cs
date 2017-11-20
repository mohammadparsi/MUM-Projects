//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Controllers
//{
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.Photo)]
//	public partial class PhotoController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public PhotoController()
//		{
//		}

//		private bool CheckSecurity(Models.Cms.Photo photo)
//		{
//			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
//				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
//			{
//				return (true);
//			}

//			if (photo.IsActive == false)
//			{
//				return (false);
//			}

//			// TODO: اين قسمت تکميل شود
//			//if (oPhoto.IsAccessibleFromTheOtherSites == false)
//			//{
//			//}

//			Models.Cms.PhotoAlbum oPhotoAlbum = photo.PhotoAlbum;

//			while (oPhotoAlbum != null)
//			{
//				if (oPhotoAlbum.IsActive == false)
//				{
//					return (false);
//				}
//				else
//				{
//					switch (oPhotoAlbum.AccessTypeEnum)
//					{
//						case Models.Enums.AccessTypes.Public:
//						{
//							// TODO: اين قسمت تکميل شود
//							break;
//						}

//						case Models.Enums.AccessTypes.Registered:
//						{
//							// TODO: اين قسمت تکميل شود
//							break;
//						}

//						case Models.Enums.AccessTypes.Special:
//						{
//							// TODO: اين قسمت تکميل شود
//							break;
//						}
//					}
//				}

//				oPhotoAlbum = oPhotoAlbum.ParentPhotoAlbum;
//			}

//			return (true);
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Public,
//			keyName: Resources.Strings.ActionsKeys.Display)]
//		public virtual System.Web.Mvc.ActionResult Display
//			(System.Guid? id = null, int? width = null, int? height = null)
//		{
//			if (id.HasValue == false)
//			{
//				return (Infrastructure.Image.NoPhoto(width, height));
//			}

//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oPhoto == null)
//			{
//				return (Infrastructure.Image.NoPhoto(width, height));
//			}

//			if (CheckSecurity(oPhoto) == false)
//			{
//				return (Infrastructure.Image.AccessDenied(width, height));
//			}

//			string strFileName =
//				string.Format("{0}{1}", oPhoto.Id, oPhoto.Extension);

//			string strRootRelativePathName =
//				string.Format("~/App_Data/Photos/{0}", strFileName);

//			string strPathName =
//				Server.MapPath(strRootRelativePathName);

//			if (System.IO.File.Exists(strPathName) == false)
//			{
//				return (Infrastructure.Image.NoPhoto(width, height));
//			}

//			oPhoto.DownloadCount++;
//			UnitOfWork.Save();

//			System.IO.MemoryStream oMemoryStream =
//				Infrastructure.Image.GetPhoto(strPathName, width, height);

//			return (File(oMemoryStream.ToArray(),
//				contentType: "image/jpeg", fileDownloadName: strFileName));
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Public,
//			keyName: Resources.Strings.ActionsKeys.Details)]
//		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
//		{
//			if (id.HasValue == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
//			}

//			Models.Cms.Photo oPhoto =
//				UnitOfWork.CmsUnitOfWork.PhotoRepository.Get()
//				.Where(current => current.Id == id)
//				.FirstOrDefault();

//			if (oPhoto == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			if (CheckSecurity(oPhoto) == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.Unauthorized)));
//			}

//			return (View(oPhoto));
//		}
//	}
//}
