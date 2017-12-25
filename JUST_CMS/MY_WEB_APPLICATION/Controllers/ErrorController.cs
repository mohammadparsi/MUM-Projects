namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/17
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Error)]
	public partial class ErrorController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ErrorController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Error.Unexpected)]
		public virtual System.Web.Mvc.ViewResult Display(System.Net.HttpStatusCode? code)
		{
			if (code == null)
			{
				code = System.Net.HttpStatusCode.BadRequest;
			}

			switch(code.Value)
			{
				case System.Net.HttpStatusCode.BadRequest:
				{
					ViewBag.Message = Resources.Errors.BadRequest;
					break;
				}

				case System.Net.HttpStatusCode.Forbidden:
				{
					ViewBag.Message = Resources.Errors.Forbidden;
					break;
				}

				case System.Net.HttpStatusCode.NotFound:
				{
					ViewBag.Message = Resources.Errors.NotFound;
					break;
				}

				case System.Net.HttpStatusCode.TemporaryRedirect:
				{
					ViewBag.Message = Resources.Errors.TemporaryRedirect;
					break;
				}

				case System.Net.HttpStatusCode.Unauthorized:
				{
					ViewBag.Message = Resources.Errors.Unauthorized;
					break;
				}

				default:
				{
					ViewBag.Message = code.ToString();

					break;
				}
			}

			return (View());
		}
	}
}
