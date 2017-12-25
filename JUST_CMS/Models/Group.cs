namespace Models
{
	/// <summary>
	/// Version: 1.0.8
	/// Update Date: 1392/06/17
	/// 
	/// </summary>
	public class Group : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Group>
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

		public Group()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Group),
			Name = Resources.Strings.GroupKeys.Name)]

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
			(ResourceType = typeof(Resources.Group),
			Name = Resources.Strings.GroupKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<User> Users { get; set; }
		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; set; }
		//public virtual System.Collections.Generic.IList<Cms.MenuItem> MenuItems { get; set; }
		//public virtual System.Collections.Generic.IList<Cms.PhotoAlbum> PhotoAlbums { get; set; }
	}
}
