namespace ViewModels.Areas.Administrator.UserLoginLog
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/04/22
	/// 
	/// </summary>
	public class IndexViewModel : System.Object
	{
		public IndexViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public System.Guid UserId { get; set; }
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
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.Gender)]
		public string Gender { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.FirstName)]
		public string FirstName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.LastName)]
		public string LastName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.EmailAddress)]
		public string EmailAddress { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.CellPhoneNumber)]
		public string CellPhoneNumber { get; set; }
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
