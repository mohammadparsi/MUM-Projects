namespace Models.Cms
{
	/// <summary>
	/// Version: 1.1.2
	/// Update Date: 1392/11/12
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table("Layouts", Schema = "Cms")]
	public class Layout : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Layout>
		{
			public Configuration()
			{
				if (Utility.IsDatabaseSqlServerCompactEdition)
				{
					Property(current => current.Description)
						.HasColumnType("ntext")
						.IsMaxLength()
						;
				}
			}
		}
		#endregion /Configuration

		public Layout()
		{
		}

		// **********
		private string _name;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Layout),
			Name = Resources.Cms.Strings.LayoutKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength(
			maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForFileName,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForFileName)]
		public string Name
		{
			get
			{
				return (_name);
			}
			set
			{
				if (value != null)
				{
					value =
						value.Replace(" ", string.Empty).ToLower();
				}
				_name = value;
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Layout),
			Name = Resources.Cms.Strings.LayoutKeys.Title)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength(
			maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Title { get; set; }
		// **********

		// **********
		public string FullName
		{
			get
			{
				string strFullName =
					string.Format("{0} - {1}", Name, Title);

				return (strFullName);
			}
		}
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Layout),
			Name = Resources.Cms.Strings.LayoutKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Page> Pages { get; set; }
		//public virtual System.Collections.Generic.IList<Post> Posts { get; set; }
		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }
	}
}
