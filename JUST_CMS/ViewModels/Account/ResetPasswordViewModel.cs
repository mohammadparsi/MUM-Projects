namespace ViewModels.Account
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/08/15
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ResetPasswordViewModel : System.Object
	{
		public ResetPasswordViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.EmailAddress)]

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
	}
}
