namespace ViewModels.Home
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/11/16
	/// 
	/// </summary>
	public class ContactViewModel : System.Object
	{
		public ContactViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Home.ContactViewModel),
			Name = Resources.Home.Strings.ContactViewModelKeys.RecipientUserId)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]
		public System.Guid RecipientUserId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Home.ContactViewModel),
			Name = Resources.Home.Strings.ContactViewModelKeys.FullName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.MaxLength)]
		public string FullName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Home.ContactViewModel),
			Name = Resources.Home.Strings.ContactViewModelKeys.EmailAddress)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForEmail,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForEmail)]
		public string EmailAddress { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Home.ContactViewModel),
			Name = Resources.Home.Strings.ContactViewModelKeys.Subject)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.MaxLength)]
		public string Subject { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Home.ContactViewModel),
			Name = Resources.Home.Strings.ContactViewModelKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Home.ContactViewModel),
			Name = Resources.Home.Strings.ContactViewModelKeys.CarbonCopyToSender)]
		public bool CarbonCopyToSender { get; set; }
		// **********
	}
}
