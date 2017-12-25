namespace MY_WEB_APPLICATION
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/04/10
	/// 
	/// </summary>
	public class FilterConfig : System.Object
	{
		public FilterConfig()
		{
		}

		public static void RegisterGlobalFilters(System.Web.Mvc.GlobalFilterCollection filters)
		{
			// ترتيب نوشتن اهميت دارد
			filters.Add(new Infrastructure.HttpRefererAttribute());
			filters.Add(new Infrastructure.AuthorizeAttribute());
			filters.Add(new Infrastructure.HandleErrorAttribute());
		}
	}
}
