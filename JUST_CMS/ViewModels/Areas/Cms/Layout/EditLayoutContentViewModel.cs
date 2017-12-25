﻿namespace ViewModels.Areas.Cms.Layout
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/11/03
	/// 
	/// </summary>
	public class EditLayoutContentViewModel : System.Object
	{
		public EditLayoutContentViewModel()
		{
		}

		public Models.Cms.Layout Layout { get; set; }

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Areas.Cms.Layout.EditLayoutContentViewModel),
			Name = Resources.Areas.Cms.Layout.Strings.EditLayoutContentViewModelKeys.Content)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Content { get; set; }
		// **********
	}
}
