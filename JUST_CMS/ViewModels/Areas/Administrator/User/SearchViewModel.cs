namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/11/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class SearchViewModel : System.Object
	{
		public SearchViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.LastName)]
		public string LastName { get; set; }
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
			Name = Models.Resources.Strings.UserKeys.Gender)]
		public System.Guid? GenderId { get; set; }
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
	}
}
