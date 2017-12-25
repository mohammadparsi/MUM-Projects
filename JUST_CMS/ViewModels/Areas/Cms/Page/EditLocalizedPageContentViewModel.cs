namespace ViewModels.Areas.Cms.Page
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/12/25
	/// 
	/// </summary>
	public class EditLocalizedPageContentViewModel : System.Object
	{
		public EditLocalizedPageContentViewModel()
		{
		}

		// **********
		public string AreaName { get; set; }
		// **********

		// **********
		public string ActionName { get; set; }
		// **********

		// **********
		public string ControllerName { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string LocalizedPageContent { get; set; }
		// **********

		public override string ToString()
		{
			string strResult = "/";

			if (string.IsNullOrEmpty(AreaName) == false)
			{
				strResult += AreaName + "/";
			}

			if (string.IsNullOrEmpty(ControllerName) == false)
			{
				strResult += ControllerName + "/";
			}

			if (string.IsNullOrEmpty(ActionName) == false)
			{
				strResult += ActionName + "/";
			}

			return (strResult);
		}
	}
}
