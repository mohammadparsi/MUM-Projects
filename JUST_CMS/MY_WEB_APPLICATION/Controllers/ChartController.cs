using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/04/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Chart)]
	public partial class ChartController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ChartController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.GetGendersCount)]
		public virtual System.Web.Mvc.JsonResult GetGendersCount()
		{
			var varResult =
				UnitOfWork.GenderRepository.Get()
				.Where(current => current.CultureLcid == CultureLcid)
				.Select(current => new
				{
					Name = current.Name3,
					UserCount = current.Users.Count
				})
				.ToList()
				;

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
