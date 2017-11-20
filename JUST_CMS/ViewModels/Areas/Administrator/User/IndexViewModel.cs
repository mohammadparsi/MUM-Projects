namespace ViewModels.Areas.Administrator.User
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1392/08/14
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class IndexViewModel : System.Object
	{
		public IndexViewModel()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.Id)]
		public System.Guid Id { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.IsActive)]
		public bool IsActive { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.IsEmailAddressVerified)]
		public bool IsEmailAddressVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.IsVerified)]
		public bool IsVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.IsDirectContactable)]
		public bool IsDirectContactable { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.IsAccountTerminated)]
		public bool IsAccountTerminated { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.IsDeleted)]
		public bool IsDeleted { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.Gender),
			Name = Models.Resources.Strings.GenderKeys.EntityName)]
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
			(ResourceType = typeof(Models.Resources.Role),
			Name = Models.Resources.Strings.RoleKeys.EntityName)]
		public string Role { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.User),
			Name = Models.Resources.Strings.UserKeys.HasAvatar)]
		public bool HasAvatar { get; set; }
		// **********
	}
}
