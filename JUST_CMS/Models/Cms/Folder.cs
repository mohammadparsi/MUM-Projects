//namespace Models.Cms
//{
//	/// <summary>
//	/// Version: 1.0.5
//	/// Update Date: 1392/06/18
//	/// 
//	/// </summary>
//	public class Folder : BaseLocalizedEntity
//	{
//		public Folder()
//		{
//		}

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Folder),
//			Name = Resources.Strings.FolderKeys.IsVisible)]
//		public bool IsVisible { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Folder),
//			Name = Resources.Strings.FolderKeys.Name)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

//		[System.ComponentModel.DataAnnotations.StringLength
//			(maximumLength: 100,
//			ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName=Resources.Strings.MessagesKeys.MaxLength)]
//		public string Name { get; set; }
//		// **********

//		// **********
//		[System.Web.Mvc.AllowHtml]

//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Folder),
//			Name = Resources.Strings.FolderKeys.Description)]

//		[System.ComponentModel.DataAnnotations.DataType
//			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
//		public string Description { get; set; }
//		// **********
//	}
//}
