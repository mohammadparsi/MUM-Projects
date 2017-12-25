namespace ViewModels.Areas.Cms.Tag
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public class IndexViewModel : System.Object, Models.IBaseExtendedEntity
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
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.IsSystem)]
		public bool IsSystem { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.IsDeleted)]
		public bool IsDeleted { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.IsVerified)]
		public bool IsVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.Ordering)]
		public int Ordering { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.Cms.Tag),
			Name = Models.Resources.Cms.Strings.TagKeys.Name)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.Cms.Tag),
			Name = Models.Resources.Cms.Strings.TagKeys.PageCount)]
		public int PageCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.Cms.Tag),
			Name = Models.Resources.Cms.Strings.TagKeys.PostCount)]
		public int PostCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.Cms.Tag),
			Name = Models.Resources.Cms.Strings.TagKeys.PhotoCount)]
		public int PhotoCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.UpdateDateTime)]
		public System.DateTime? UpdateDateTime { get; set; }
		// **********
	}
}
