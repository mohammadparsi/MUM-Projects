namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/05/20
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table("PageContents", Schema = "Cms")]
	public class PageContent : System.Object
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PageContent>
		{
			public Configuration()
			{
				HasKey(current => current.PageId);

				HasRequired(current => current.Page)
				.WithOptional(person => person.PageContent)
				.WillCascadeOnDelete(true)
				;

				if (Utility.IsDatabaseSqlServerCompactEdition)
				{
					Property(current => current.Content)
						.HasColumnType("ntext")
						.IsMaxLength()
						;
				}
			}
		}
		#endregion /Configuration

		public PageContent()
		{
		}

		public virtual Page Page { get; set; }
		public System.Guid PageId { get; set; }

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.PageContent),
			Name = Resources.Cms.Strings.PageContentKeys.Content)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Content { get; set; }
		// **********
	}
}
