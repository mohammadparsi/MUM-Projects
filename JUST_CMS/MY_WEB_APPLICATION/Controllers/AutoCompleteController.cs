using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/04/17
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.AutoComplete)]
	public partial class AutoCompleteController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public AutoCompleteController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.GetCities)]
		public virtual System.Web.Mvc.JsonResult GetCities(string term)
		{
			if (string.IsNullOrEmpty(term))
			{
				return (null);
			}

			var varCities =
				UnitOfWork.CityRepository.Get()
				.Where(current => current.Name.Contains(term))
				.Where(current => current.IsActive)
				.Where(current => current.State.IsActive)
				.Where(current => current.State.Country.IsActive)
				.Select(current => new ViewModels.AutoComplete.GetCitiesViewModel()
				{
					Id = current.Id,
					CityName = current.Name,
					StateName = current.State.Name,
					CountryName = current.State.Country.Name
				})
				.OrderBy(current => current.CountryName)
				.ThenBy(current => current.StateName)
				.ThenBy(current => current.CityName)
				.Take(20)
				;

			return (Json(varCities, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}
	}
}
