namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/06/19
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class AccessType : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AccessType>
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

		public AccessType()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.AccessType),
			Name = Resources.Strings.AccessTypeKeys.Code)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int Code { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.AccessType),
			Name = Resources.Strings.AccessTypeKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.AccessType),
			Name = Resources.Strings.AccessTypeKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; set; }
		public virtual System.Collections.Generic.IList<Cms.Post> Posts { get; set; }
		//public virtual System.Collections.Generic.IList<Cms.MenuItem> MenuItems { get; set; }
		//public virtual System.Collections.Generic.IList<Cms.PhotoAlbum> PhotoAlbums { get; set; }

		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }
	}
}
