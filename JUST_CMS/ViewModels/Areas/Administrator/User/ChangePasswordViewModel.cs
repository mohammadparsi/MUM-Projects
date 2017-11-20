namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/12/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ChangePasswordViewModel : System.Object
	{
		public ChangePasswordViewModel()
		{
		}

		// **********
		public System.Guid UserId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Areas.Administrator.User.ChangePasswordViewModel),
			Name = Resources.Areas.Administrator.User.Strings.ChangePasswordViewModelKeys.NewPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForPassword,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string NewPassword { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Areas.Administrator.User.ChangePasswordViewModel),
			Name = Resources.Areas.Administrator.User.Strings.ChangePasswordViewModelKeys.ConfirmNewPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Compare
			(otherProperty: "NewPassword",
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Confirm)]
		public string ConfirmNewPassword { get; set; }
		// **********
	}
}
