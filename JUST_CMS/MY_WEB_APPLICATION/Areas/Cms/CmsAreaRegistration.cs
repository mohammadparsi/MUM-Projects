using System.Web.Mvc;

namespace MY_WEB_APPLICATION.Areas.Cms
{
	public class CmsAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return ("Cms");
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Cms_default",
				"Cms/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
