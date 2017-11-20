namespace ViewModels.Areas.Administrator.WebDownloader
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/05/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DownloadViewModel : System.Object
	{
		public DownloadViewModel()
		{
			PocketSize = 64;
			SourceTimeout = 100;
			TargetTimeout = 300;
			FlushLength = 10;
		}

		// **********
		public string TargetPathName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.TargetFileName)]
		public System.TimeSpan? DownLoadAndSaveToFileDuration { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.Flush)]
		public bool Flush { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.FlushLength)]
		public int FlushLength { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.SourceFileUrl)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]
		public string SourceFileUrl { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.TargetFileName)]
		public string TargetFileName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.PocketSize)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int PocketSize { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.TargetTimeout)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int TargetTimeout { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(ViewModels.Resources.Areas.Administrator.WebDownloader.DownloadViewModel),
			Name = ViewModels.Resources.Areas.Administrator.WebDownloader.Strings.DownloadViewModelKeys.SourceTimeout)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName = Models.Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(Models.RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Models.Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int SourceTimeout { get; set; }
		// **********
	}
}
