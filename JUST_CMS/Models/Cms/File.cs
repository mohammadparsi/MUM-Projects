//namespace Models.Cms
//{
//	/// <summary>
//	/// Version: 1.1.1
//	/// Update Date: 1392/06/18
//	/// Developer: Mr. Dariush Tasdighi
//	/// </summary>
//	public class File : BaseLocalizedEntity
//	{
//		public File()
//		{
//		}

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.Length)]
//		public int Length { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.DownloadCount)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

//		[System.ComponentModel.DataAnnotations.RegularExpression
//			(RegularExpressions.RegularExpressionForInteger,
//			ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName =
//			Resources.Strings.MessagesKeys.RegularExpressionForInteger)]
//		public int DownloadCount { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.IsDirectAccessible)]
//		public bool IsDirectAccessible { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.IsAccessibleFromTheOtherSites)]
//		public bool IsAccessibleFromTheOtherSites { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.Title)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
//		public string Title { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Schema.NotMapped]

//		//[System.ComponentModel.DataAnnotations.Display
//		//	(ResourceType = typeof(Resources.Cms.BaseLocalizedEntityForPageAndPost),
//		//	Name = Resources.Strings.BaseLocalizedEntityForPageAndPostKeys.Tags)]
//		public string Tags { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.Keywords)]
//		public string Keywords { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.Extension)]
//		public string Extension { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.OriginalName)]
//		public string OriginalName { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.StringLength
//			(maximumLength: 32,
//			ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
//		public string MD5 { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.StringLength
//			(maximumLength: 40,
//			ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
//		public string Sha1 { get; set; }
//		// **********

//		// **********
//		[System.Web.Mvc.AllowHtml]

//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.File),
//			Name = Resources.Strings.FileKeys.Description)]

//		[System.ComponentModel.DataAnnotations.DataType
//			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
//		public string Description { get; set; }
//		// **********
//	}
//}
