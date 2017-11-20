namespace ViewModels.Account
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/02/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class RegisterViewModel : System.Object
	{
		public RegisterViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Account.RegisterViewModel),
			Name = Resources.Account.Strings.RegisterViewModelKeys.EmailAddress)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 250,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForEmail,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForEmail)]
		public string EmailAddress { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.CellPhoneNumber)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 14,
			MinimumLength = 14,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.StringLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForCellPhoneNumber,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForCellPhoneNumber)]
		public string CellPhoneNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Account.RegisterViewModel),
			Name = Resources.Account.Strings.RegisterViewModelKeys.Password)]

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
		public string Password { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Account.RegisterViewModel),
			Name = Resources.Account.Strings.RegisterViewModelKeys.ConfirmPassword)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Compare
			(otherProperty: "Password",
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.Confirm)]
		public string ConfirmPassword { get; set; }
		// **********
	}
}
