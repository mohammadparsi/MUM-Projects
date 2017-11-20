using System.Web.Mvc;

namespace MY_WEB_APPLICATION.Areas.Administrator
{
	public class AdministratorAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return ("Administrator");
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			//context.MapRoute(
			//	"Administrator_Default",
			//	"Administrator/{controller}/{action}/{id}",
			//	new { action = "Index", id = UrlParameter.Optional }
			//);

			context.MapRoute(
				"Administrator_Default",
				"Administrator/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			);

			//context.MapRoute(
			//	"Administrator_Default",
			//	"Administrator/Home/Index/{id}",
			//	new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
			//	namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			//);

			context.MapRoute(
				"Administrator_Page",
				"Administrator/Page/{action}/{id}",
				new { controller = "Page", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "MY_WEB_APPLICATION.Areas.Administrator.Controllers" }
			);
		}
	}
}
