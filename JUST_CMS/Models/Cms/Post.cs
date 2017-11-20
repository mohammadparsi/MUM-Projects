namespace Models.Cms
{
	/// <summary>
	/// Version: 1.1.2
	/// Update Date: 1392/12/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table("Posts", Schema = "Cms")]
	public class Post : BaseLocalizedPageOrPost, ITag
	{
		#region Configuration
		public class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Post>
		{
			public Configuration()
			{
				//HasOptional(current => current.Layout)
				//	.WithMany(layout => layout.Posts)
				//	.HasForeignKey(current => current.LayoutId)
				//	.WillCascadeOnDelete(false)
				//	;

				HasRequired(current => current.Category)
					.WithMany(postCategory => postCategory.Posts)
					.HasForeignKey(current => current.CategoryId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.AccessType)
					.WithMany(accessType => accessType.Posts)
					.HasForeignKey(current => current.AccessTypeId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.AuthorUser)
					.WithMany(user => user.WrittenPosts)
					.HasForeignKey(current => current.AuthorUserId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.VerifierUser)
					.WithMany(user => user.VerifiedPosts)
					.HasForeignKey(current => current.VerifierUserId)
					.WillCascadeOnDelete(false)
					;

				if (Utility.IsDatabaseSqlServerCompactEdition)
				{
					Property(current => current.Body)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Author)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Keywords)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Copyright)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Introduction)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Classification)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Description)
						.HasColumnType("ntext")
						.IsMaxLength()
						;
				}
			}
		}
		#endregion /Configuration

		public Post()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.AuthorUser)]
		public virtual User AuthorUser { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.AuthorUser)]
		public System.Guid AuthorUserId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.AccessType),
			Name = Resources.Strings.AccessTypeKeys.EntityName)]
		public virtual AccessType AccessType { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.AccessType),
			Name = Resources.Strings.AccessTypeKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid AccessTypeId { get; set; }
		// **********

		// **********
		public Enums.AccessTypes AccessTypeEnum
		{
			get
			{
				if (AccessType == null)
				{
					return (Enums.AccessTypes.Registered);
				}
				else
				{
					return ((Enums.AccessTypes)AccessType.Code);
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.Category)]
		public virtual PostCategory Category { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.Category)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid CategoryId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.IsFeatured)]
		public bool IsFeatured { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.HasItAttachments)]
		public bool HasItAttachments { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.IsCommentsEnabled)]
		public bool IsCommentsEnabled { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.Password)]
		public string Password { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.Introduction)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Introduction { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.ThumbnailImageUrl)]

		// TODO: بعدا تست شود
		//[System.ComponentModel.DataAnnotations.RegularExpression
		//	(RegularExpressions.RegularExpressionForUrl,
		//	ErrorMessageResourceType = typeof(Resources.Messages),
		//	ErrorMessageResourceName =
		//	Resources.Strings.MessagesKeys.RegularExpressionForUrl)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Url)]
		public string ThumbnailImageUrl { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Post),
			Name = Resources.Cms.Strings.PostKeys.Body)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Body { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<Tag> TagList { get; set; }
		public virtual System.Collections.Generic.IList<PostComment> Comments { get; set; }
	}
}
