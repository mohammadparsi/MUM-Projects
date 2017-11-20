//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Controllers
//{
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.PhotoAlbum)]
//	public partial class PhotoAlbumController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public PhotoAlbumController()
//		{
//		}

//		[System.Web.Mvc.HttpGet]
//		[Infrastructure.ProjectActionPermission
//			(isVisibleJustForProgrammer: false,
//			accessType: Models.Enums.AccessTypes.Public,
//			keyName: Resources.Strings.ActionsKeys.Display)]
//		public virtual System.Web.Mvc.ActionResult Display(System.Guid? id)
//		{
//			if (id.HasValue == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
//			}

//			var varPhotoAlbum =
//				UnitOfWork.CmsUnitOfWork.PhotoAlbumRepository.Get()
//				.Where(current => current.Id == id.Value)
//				.FirstOrDefault();

//			if (varPhotoAlbum == null)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
//				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
//			{
//				// در صورتی که کاربر، کاربر عادی نباشد، دستور ذيل نبايد اجرا شود
//				//oPage.VisitCount++;

//				return (View(varPhotoAlbum));
//			}

//			if (varPhotoAlbum.IsDeleted)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
//			}

//			if (varPhotoAlbum.IsActive == false)
//			{
//				return (RedirectToAction
//					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
//			}

//			return (View(varPhotoAlbum));
//		}
//	}
//}
