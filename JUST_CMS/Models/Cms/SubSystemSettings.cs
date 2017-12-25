namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table("CmsSubSystemSettings", Schema = "Cms")]
	public class SubSystemSettings : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<SubSystemSettings>
		{
		}
		#endregion /Configuration

		public SubSystemSettings()
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
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.SubSystemSettings),
			Name = Resources.Cms.Strings.SubSystemSettingsKeys.HomePagePostCount)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int HomePagePostCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssItemCount)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int RssItemCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssChannelTitle)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public string RssChannelTitle { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssChannelLink)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUrl,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]
		public string RssChannelLink { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssChannelDescription)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public string RssChannelDescription { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssImageUrl)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUrl,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]
		public string RssImageUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssImageLink)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUrl,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]
		public string RssImageLink { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssImageTitle)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public string RssImageTitle { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.General),
			Name = Resources.Cms.Strings.GeneralKeys.RssRootCreatorUrl)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUrl,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUrl)]
		public string RssRootCreatorUrl { get; set; }
		// **********

		public static SubSystemSettings GetDefaultObject()
		{
			SubSystemSettings oDefaultObject =
				new SubSystemSettings();

			oDefaultObject.HomePagePostCount = 10;

			oDefaultObject.RssItemCount = 10;
			oDefaultObject.RssChannelTitle = "عنوان";
			oDefaultObject.RssChannelLink = "http://www.IranianExperts.com";
			oDefaultObject.RssRootCreatorUrl = "http://www.IranianExperts.com/Rss/1";
			oDefaultObject.RssChannelDescription = "توضيحات";

			oDefaultObject.RssImageTitle = "عنوان عکس";
			oDefaultObject.RssImageLink = "http://www.IranianExperts.com";
			oDefaultObject.RssImageUrl = "http://www.IranianExperts.com/Images/Rss.png";

			oDefaultObject.CultureLcid =
				System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

			return (oDefaultObject);
		}
	}
}
