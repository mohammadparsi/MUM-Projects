namespace Models.Cms
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/12/04
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table("Tags", Schema = "Cms")]
	public class Tag : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Tag>
		{
			public Configuration()
			{
				HasOptional(current => current.VerifierUser)
					.WithMany(user => user.VerifiedTags)
					.HasForeignKey(current => current.VerifierUserId)
					.WillCascadeOnDelete(false)
					;

				HasMany(current => current.Pages)
				.WithMany(page => page.TagList)
				.Map(current =>
				{
					current.ToTable("TagsOfPages", "Cms");
					// MapRightKey را می نويسيم و بعد MapLeftKey اول
					// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
					current.MapLeftKey("TagId");
					current.MapRightKey("PageId");
				});

				HasMany(current => current.Posts)
				.WithMany(post => post.TagList)
				.Map(current =>
				{
					current.ToTable("TagsOfPosts", "Cms");
					// MapRightKey را می نويسيم و بعد MapLeftKey اول
					// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
					current.MapLeftKey("TagId");
					current.MapRightKey("PostId");
				});

				//HasMany(current => current.Photos)
				//.WithMany(post => post.TagList)
				//.Map(current =>
				//{
				//	current.ToTable("TagsOfPhotos", "Cms");
				//	// MapRightKey را می نويسيم و بعد MapLeftKey اول
				//	// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
				//	current.MapLeftKey("TagId");
				//	current.MapRightKey("PhotoId");
				//});
			}
		}
		#endregion /Configuration

		public Tag()
		{
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Tag),
			Name = Resources.Cms.Strings.TagKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Page> Pages { get; set; }
		public virtual System.Collections.Generic.IList<Post> Posts { get; set; }

		//public virtual System.Collections.Generic.IList<Photo> Photos { get; set; }
	}
}
