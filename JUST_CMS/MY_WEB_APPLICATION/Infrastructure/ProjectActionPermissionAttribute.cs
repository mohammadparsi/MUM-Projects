namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/04/24
	/// 
	/// </summary>
	public class ProjectActionPermissionAttribute : System.Attribute
	{
		public ProjectActionPermissionAttribute
			(string keyName,
			Models.Enums.AccessTypes accessType = Models.Enums.AccessTypes.Special,
			bool isVisibleJustForProgrammer = true)
		{
			KeyName = keyName;
			AccessType = accessType;
			IsVisibleJustForProgrammer = isVisibleJustForProgrammer;
		}

		public ProjectActionPermissionAttribute
			(string keyName,
			Models.Enums.AccessTypes accessType = Models.Enums.AccessTypes.Special,
			System.Type resourceType = null,
			bool isVisibleJustForProgrammer = true)
		{
			KeyName = keyName;
			AccessType = accessType;
			ResourceType = resourceType;
			IsVisibleJustForProgrammer = isVisibleJustForProgrammer;
		}

		public string KeyName { get; set; }
		public System.Type ResourceType { get; set; }
		public bool IsVisibleJustForProgrammer { get; set; }
		public Models.Enums.AccessTypes AccessType { get; set; }
	}
}
