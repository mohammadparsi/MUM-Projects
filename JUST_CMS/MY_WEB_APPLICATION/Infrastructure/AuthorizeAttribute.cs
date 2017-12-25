using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/12/07
	/// 
	/// </summary>
	public class AuthorizeAttribute : System.Web.Mvc.ActionFilterAttribute
	{
		public AuthorizeAttribute()
		{
		}

		public override void OnActionExecuting
			(System.Web.Mvc.ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			// **************************************************
			object objArea =
				filterContext.RouteData.DataTokens["area"];

			string strArea = string.Empty;
			if (objArea != null)
			{
				strArea = objArea.ToString().Trim();
			}
			// **************************************************

			// **************************************************
			object objController =
				filterContext.RouteData.Values["controller"];

			if ((objController == null) ||
				(objController.ToString().Trim() == string.Empty))
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "InvalidRequest",
						filterContext: filterContext
					);

				return;
			}

			string strController =
				objController.ToString().Trim();
			// **************************************************

			// **************************************************
			object objAction =
				filterContext.RouteData.Values["action"];

			if ((objAction == null) ||
				(objAction.ToString().Trim() == string.Empty))
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "InvalidRequest",
						filterContext: filterContext
					);

				return;
			}

			string strAction =
				objAction.ToString().Trim();
			// **************************************************

			// **************************************************
			string strReturnUrl = string.Empty;
			if (string.IsNullOrEmpty(strArea))
			{
				strReturnUrl =
					string.Format("/{0}/{1}", strController, strAction);
			}
			else
			{
				strReturnUrl =
					string.Format("/{0}/{1}/{2}", strArea, strController, strAction);
			}
			// **************************************************

			if((strArea == string.Empty) &&
				(string.Compare(strAction, "Generate", ignoreCase: true) == 0) &&
				(string.Compare(strController, "DefaultCaptcha", ignoreCase: true) == 0))
			{
				return;
			}

			if ((strArea == string.Empty) &&
				(string.Compare(strAction, "Refresh", ignoreCase: true) == 0) &&
				(string.Compare(strController, "DefaultCaptcha", ignoreCase: true) == 0))
			{
				return;
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer))
			{
				return;
			}

			// **************************************************
			// اگر
			// Infrastructure.AuthenticatedUser.IsAuthenticated = false
			// يا
			// Infrastructure.AuthenticatedUser.IsAuthenticated = true &&
			// Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.Programmer
			// **************************************************

			DAL.UnitOfWork oUnitOfWork = new DAL.UnitOfWork();

			// **************************************************
			Models.ProjectAction oAction =
				oUnitOfWork.ProjectActionRepository
				.GetByRouteValues(strArea, strController, strAction);

			if (oAction == null)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "InvalidRequest",
						filterContext: filterContext
					);

				return;
			}
			// **************************************************

			// **************************************************
			// اگر
			// oAction != null
			// **************************************************

			if (oAction.ProjectController.ProjectArea.IsActive == false)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "AccessDenied",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// اگر
			// oAction.ProjectController.ProjectArea.IsActive = true
			// **************************************************

			if (oAction.ProjectController.ProjectArea.IsVisibleJustForProgrammer)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "AccessDenied",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// اگر
			// oAction.ProjectController.ProjectArea.IsVisibleJustForProgrammer = false
			// **************************************************

			if (oAction.ProjectController.IsActive == false)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "AccessDenied",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// اگر
			// oAction.ProjectController.IsActive = true
			// **************************************************

			if (oAction.ProjectController.IsVisibleJustForProgrammer)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "AccessDenied",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// اگر
			// oAction.ProjectController.IsVisibleJustForProgrammer = false
			// **************************************************

			if (oAction.IsActive == false)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "AccessDenied",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// اگر
			// oAction.IsActive = true
			// **************************************************

			if (oAction.IsVisibleJustForProgrammer)
			{
				RedirectToRouteResult
					(
						areaName: string.Empty,
						controllerName: "Error",
						actionName: "AccessDenied",
						filterContext: filterContext
					);

				return;
			}

			// **************************************************
			// اگر
			// oAction.IsVisibleJustForProgrammer = false
			// **************************************************

			if (oAction.AccessTypeEnum == Models.Enums.AccessTypes.Public)
			{
				return;
			}

			// **************************************************
			// اگر
			// oAction.IsPublic == false
			// **************************************************

			if (Infrastructure.AuthenticatedUser.IsAuthenticated == false)
			{
				string strUrl =
					string.Format("/Account/Login?ReturnUrl={0}", strReturnUrl);

				filterContext.Result =
					new System.Web.Mvc.RedirectResult(strUrl);

				return;
			}

			// **************************************************
			// اگر
			// Infrastructure.AuthenticatedUser.IsAuthenticated = true
			// **************************************************

			if (oAction.AccessTypeEnum == Models.Enums.AccessTypes.Registered)
			{
				return;
			}

			switch (Infrastructure.Sessions.AuthenticatedUser.Role)
			{
				//case Models.Enums.Roles.Programmer:

				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Administrator:

				//case Models.Enums.Roles.User
				//case Models.Enums.Roles.Supervisor:
				{
					return;
				}
			}

			// **************************************************
			// اگر
			// oAction.AccessTypeEnum = Models.Enums.AccessTypes.Special
			// و
			// Infrastructure.Sessions.AuthenticatedUser.Role = Models.Enums.Roles.User
			// **************************************************

			int intGroupCount =
				oUnitOfWork.GroupRepository.GetActiveGroupCountByUserIdAndActionId
				(Infrastructure.Sessions.AuthenticatedUser.Id, oAction.Id);

			if (intGroupCount >= 1)
			{
				return;
			}

			// **************************************************
			// اگر
			// intGroupCount = 0
			// **************************************************

			bool blnUserHasDirectAccessForAction =
				oUnitOfWork.UserRepository.CheckDirectAccessForAction
				(Infrastructure.Sessions.AuthenticatedUser.Id, oAction.Id);

			if (blnUserHasDirectAccessForAction)
			{
				return;
			}

			// **************************************************
			// اگر
			// blnUserHasDirectAccessForAction = false
			// **************************************************

			RedirectToRouteResult
				(
					areaName: string.Empty,
					controllerName: "Error",
					actionName: "AccessDenied",
					filterContext: filterContext
				);
		}

		private void RedirectToRouteResult
			(System.Web.Mvc.ActionExecutingContext filterContext,
			string areaName, string controllerName, string actionName,
			System.Collections.Generic.IDictionary<string, object> actionParameters = null)
		{
			if (actionParameters != null)
			{
				foreach (var varItem in actionParameters)
				{
					filterContext.ActionParameters.Add(varItem);
				}
			}

			filterContext.Result =
				new System.Web.Mvc.RedirectToRouteResult(
				new System.Web.Routing.RouteValueDictionary
					{
						{ "area", areaName },
						{ "controller", controllerName },
						{ "action", actionName }
					});
		}

		public override void OnActionExecuted
			(System.Web.Mvc.ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);
		}

		public override void OnResultExecuting
			(System.Web.Mvc.ResultExecutingContext filterContext)
		{
			base.OnResultExecuting(filterContext);
		}

		public override void OnResultExecuted
			(System.Web.Mvc.ResultExecutedContext filterContext)
		{
			base.OnResultExecuted(filterContext);
		}
	}
}
