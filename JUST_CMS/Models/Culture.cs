namespace Models
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/02/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class Culture : BaseExtendedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Culture>
		{
			public Configuration()
			{
			}
		}
		#endregion /Configuration

		public Culture()
		{
		}

		public Culture(System.Globalization.CultureInfo cultureInfo)
		{
			Lcid = cultureInfo.LCID;
			Name = cultureInfo.Name;
			NativeName = cultureInfo.NativeName;
			DisplayName = cultureInfo.DisplayName;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MyCulture),
			Name = Resources.Strings.MyCultureKeys.Lcid)]
		public int Lcid { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MyCulture),
			Name = Resources.Strings.MyCultureKeys.Name)]
		public string Name { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MyCulture),
			Name = Resources.Strings.MyCultureKeys.NativeName)]
		public string NativeName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MyCulture),
			Name = Resources.Strings.MyCultureKeys.DisplayName)]
		public string DisplayName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MyCulture),
			Name = Resources.Strings.MyCultureKeys.Description)]
		public string Description { get; set; }
		// **********
	}
}
