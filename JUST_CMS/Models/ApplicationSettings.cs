namespace Models
{
	/// <summary>
	/// Version: 1.1.5
	/// Update Date: 1393/04/29
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ApplicationSettings : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationSettings>
		{
			public Configuration()
			{
				HasOptional(current => current.MailSettings)
					.WithMany(mailSettings => mailSettings.AllOfApplicationSettings)
					.HasForeignKey(current => current.MailSettingsId)
					.WillCascadeOnDelete(false)
					;
			}
		}
		#endregion /Configuration

		public ApplicationSettings()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Base),
			Name = Resources.Strings.BaseKeys.CultureId)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public System.Guid CultureId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Base),
			Name = Resources.Strings.BaseKeys.CultureLcid)]

		[System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
		public int CultureLcid { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MailSettings),
			Name = Resources.Strings.MailSettingsKeys.EntityName)]
		public virtual MailSettings MailSettings { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.MailSettings),
			Name = Resources.Strings.MailSettingsKeys.EntityName)]
		public System.Guid? MailSettingsId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.ApplicationName)]
		public string ApplicationName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.ApplicationVersion)]
		public string ApplicationVersion { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.ApplicationCopyrightText)]
		public string ApplicationCopyrightText { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.CheckNationalCode)]
		public bool CheckNationalCode { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.ServerTimeZoneDifference)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForInteger)]
		public int ServerTimeZoneDifference { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.ForceUserToUpdateProfileAfterLogin)]
		public bool ForceUserToUpdateProfileAfterLogin { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.IsCaptchaImageEnabled)]
		public bool IsCaptchaImageEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.IsSmsServiceEnabled)]
		public bool IsSmsServiceEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.IsRegistrationEnabled)]
		public bool IsRegistrationEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.IsCmsEnabled)]
		public bool IsCmsEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.ShouldUserBeActivatedAfterRegistration)]
		public bool ShouldUserBeActivatedAfterRegistration { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.CanUserAttachFileInContactUsPage)]
		public bool CanUserAttachFileInContactUsPage { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.CanUserSearchTheOthers)]
		public bool CanUserSearchTheOthers { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ApplicationSettings),
			Name = Resources.Strings.ApplicationSettingsKeys.IsUserEmailVerificationRequiredForLogin)]
		public bool IsUserEmailVerificationRequiredForLogin { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Title)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string HomePageTitle { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Keywords)]
		public string HomePageKeywords { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Classification)]
		public string HomePageClassification { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Description)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string HomePageDescription { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Copyright)]
		public string HomePageCopyright { get; set; }
		// **********

		public static ApplicationSettings GetDefaultObject()
		{
			ApplicationSettings oDefaultObject =
				new Models.ApplicationSettings();

			oDefaultObject.MailSettingsId = null;

			oDefaultObject.IsSmsServiceEnabled = false;
			oDefaultObject.IsRegistrationEnabled = false;
			oDefaultObject.ShouldUserBeActivatedAfterRegistration = false;
			oDefaultObject.IsUserEmailVerificationRequiredForLogin = true;

			oDefaultObject.CultureLcid =
				System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

			return (oDefaultObject);
		}
	}
}
