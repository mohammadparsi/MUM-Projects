using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/05
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.PostCategory)]
	public partial class PostCategoryController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public PostCategoryController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.Cms.PostCategory oPostCategory =
				UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oPostCategory == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oPostCategory.IsDeleted)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oPostCategory.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			return (View(oPostCategory));
		}
	}
}
