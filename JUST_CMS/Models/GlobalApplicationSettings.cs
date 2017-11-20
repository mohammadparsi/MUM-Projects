namespace Models
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/01/27
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GlobalApplicationSettings : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<GlobalApplicationSettings>
		{
			public Configuration()
			{
			}
		}
		#endregion /Configuration

		public GlobalApplicationSettings()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.MasterPassword)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForPassword,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string MasterPassword { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.CurrentUserProfileLevel)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int CurrentUserProfileLevel { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.IsSslEnabled)]
		public bool IsSslEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.SmsCenterNumber)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string SmsCenterNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.SmsCenterUsername)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string SmsCenterUsername { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.SmsCenterPassword)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string SmsCenterPassword { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display(Name = "Alexa Code")]
		public string AlexaCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display(Name = "Google Code")]
		public string GoogleCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.TotalUserCount)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int TotalUserCount { get; set; }
		// **********

		public static GlobalApplicationSettings GetDefaultObject()
		{
			GlobalApplicationSettings oDefaultObject =
				new Models.GlobalApplicationSettings();

			oDefaultObject.CurrentUserProfileLevel = 1;

			return (oDefaultObject);
		}
	}
}
