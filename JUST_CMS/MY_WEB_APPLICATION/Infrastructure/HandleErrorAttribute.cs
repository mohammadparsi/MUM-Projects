namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/23
	/// 
	/// </summary>
	public class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
	{
		public HandleErrorAttribute()
		{
		}

		public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
		{
			Dtx.LogHandler.Report(GetType(), null, filterContext.Exception);

			base.OnException(filterContext);
		}
	}
}