using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/01/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Tag)]
	public partial class TagController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public TagController()
		{
		}

		//[System.Web.Mvc.HttpGet]
		//[Infrastructure.ProjectActionPermission
		//	(isVisibleJustForProgrammer: false,
		//	accessType: Models.Enums.AccessTypes.Public,
		//	keyName: Resources.Strings.ActionsKeys.List)]
		//public virtual System.Web.Mvc.ActionResult Index()
		//{
		//	//var varTags =
		//	//	UnitOfWork.CmsUnitOfWork.TagRepository.Get()
		//	//	.Select(current => new ViewModels.TagIndexViewModel
		//	//	{
		//	//		Id = current.Id,
		//	//		Name = current.Name,
		//	//		IsActive = current.IsActive,
		//	//		CultureLcid = current.CultureLcid,
		//	//		PageCount = current.Pages.Count(),
		//	//		PostCount = current.Posts.Count(),
		//	//		PhotoCount = current.Photos.Count(),
		//	//		UpdateDateTime = current.UpdateDateTime
		//	//	})
		//	//	.Where(current => current.IsActive)
		//	//	.Where(current => current.CultureLcid == CultureLcid)
		//	//	.OrderBy(current => current.Name)
		//	//	.ToList()
		//	//	;

		//	//return (View(varTags));

		//	return(null);
		//}

		//[System.Web.Mvc.HttpGet]
		//[Infrastructure.ProjectActionPermission
		//	(isVisibleJustForProgrammer: false,
		//	accessType: Models.Enums.AccessTypes.Public,
		//	keyName: Resources.Strings.ActionsKeys.Details)]
		//public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		//{
		//	Models.Cms.Tag oTag =
		//		UnitOfWork.CmsUnitOfWork.TagRepository.Get()
		//		.Where(current => current.Id == id.Value)
		//		.FirstOrDefault();

		//	if (oTag == null)
		//	{
		//		return (RedirectToAction
		//			(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
		//	}

		//	if (oTag.IsActive == false)
		//	{
		//		return (RedirectToAction
		//			(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
		//	}

		//	return (View(oTag));
		//}

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

			Models.Cms.Tag oTag =
				UnitOfWork.CmsUnitOfWork.TagRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oTag == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oTag.IsDeleted)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oTag.IsActive == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.TemporaryRedirect)));
			}

			return (View(oTag));
		}
	}
}
