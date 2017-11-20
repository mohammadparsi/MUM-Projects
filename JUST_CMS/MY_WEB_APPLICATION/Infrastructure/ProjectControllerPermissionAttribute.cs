namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/04/24
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectControllerPermissionAttribute : System.Attribute
	{
		public ProjectControllerPermissionAttribute
			(string keyName,
			bool isVisibleJustForProgrammer = true)
		{
			KeyName = keyName;
			IsVisibleJustForProgrammer = isVisibleJustForProgrammer;
		}

		public ProjectControllerPermissionAttribute
			(string keyName,
			System.Type resourceType = null,
			bool isVisibleJustForProgrammer = true)
			: this(keyName, isVisibleJustForProgrammer)
		{
			ResourceType = resourceType;
		}

		public string KeyName { get; set; }
		public System.Type ResourceType { get; set; }
		public bool IsVisibleJustForProgrammer { get; set; }
	}
}
