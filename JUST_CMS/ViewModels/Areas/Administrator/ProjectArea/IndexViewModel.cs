namespace ViewModels.Areas.Administrator.ProjectArea
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1392/12/21
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
			Name = Models.Resources.Strings.BaseEntityKeys.Ordering)]
		public int Ordering { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseAreaControllerAction),
			Name = Models.Resources.Strings.BaseAreaControllerActionKeys.IsVisibleJustForProgrammer)]
		public bool IsVisibleJustForProgrammer { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseAreaControllerAction),
			Name = Models.Resources.Strings.BaseAreaControllerActionKeys.Name)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseAreaControllerAction),
			Name = Models.Resources.Strings.BaseAreaControllerActionKeys.DisplayName)]
		public string DisplayName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.ProjectArea),
			Name = Models.Resources.Strings.ProjectAreaKeys.ProjectControllerCount)]
		public int ProjectControllerCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.InsertDateTime)]
		public System.DateTime InsertDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Models.Resources.BaseEntity),
			Name = Models.Resources.Strings.BaseEntityKeys.UpdateDateTime)]
		public System.DateTime? UpdateDateTime { get; set; }
		// **********

		public bool IsSystem
		{
			get
			{
				return (false);
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public bool IsDeleted
		{
			get
			{
				return (false);
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}

		public bool IsVerified
		{
			get
			{
				return (true);
			}
			set
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
