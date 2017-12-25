namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/11/09
	/// 
	/// </summary>
	public class BaseLocalizedPageOrPost : BaseLocalizedEntity
	{
		public BaseLocalizedPageOrPost()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.DoesSearchEnginesIndexIt)]
		public bool DoesSearchEnginesIndexIt { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.DoesSearchEnginesFollowIt)]
		public bool DoesSearchEnginesFollowIt { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Hits)]
		public int Hits { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Title)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength(
			maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Title { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Tags)]
		public string Tags { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Author)]
		public string Author { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Keywords)]
		public string Keywords { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Classification)]
		public string Classification { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Description)]

		[System.ComponentModel.DataAnnotations.StringLength(
			maximumLength: 65,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Description { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.Copyright)]
		public string Copyright { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.StartPublishingDateTime)]
		public System.DateTime? StartPublishingDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.BaseLocalizedPageOrPost),
			Name = Resources.Cms.Strings.BaseLocalizedPageOrPostKeys.FinishPublishingDateTime)]
		public System.DateTime? FinishPublishingDateTime { get; set; }
		// **********
	}
}
