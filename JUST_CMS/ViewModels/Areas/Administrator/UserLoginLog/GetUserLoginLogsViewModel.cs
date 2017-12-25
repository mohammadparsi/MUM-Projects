namespace ViewModels.Areas.Administrator.UserLoginLog
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/04/25
	/// 
	/// </summary>
	public class GetUserLoginLogsViewModel : System.Object
	{
		public GetUserLoginLogsViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.UserLoginLog),
			Name = Models.Resources.Strings.UserLoginLogKeys.IsHidden)]
		public bool IsHidden { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.UserLoginLog),
			Name = Models.Resources.Strings.UserLoginLogKeys.IP)]
		public string IP { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.UserLoginLog),
			Name = Models.Resources.Strings.UserLoginLogKeys.LoginDateTime)]
		public System.DateTime LoginDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.UserLoginLog),
			Name = Models.Resources.Strings.UserLoginLogKeys.LogoutDateTime)]
		public System.DateTime? LogoutDateTime { get; set; }
		// **********

		// **********
		public string Login
		{
			get
			{
				return (Dtx.Calendar.Utility.DisplayDateTime(LoginDateTime, true));
			}
		}
		// **********

		// **********
		public string Logout
		{
			get
			{
				return (Dtx.Calendar.Utility.DisplayDateTime(LogoutDateTime, true));
			}
		}
		// **********
	}
}
