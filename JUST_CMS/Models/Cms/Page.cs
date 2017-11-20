namespace Models.Cms
{
	/// <summary>
	/// Version: 1.1.7
	/// Update Date: 1392/12/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.Table("Pages", Schema = "Cms")]
	public class Page : BaseLocalizedPageOrPost, ITag
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Page>
		{
			public Configuration()
			{
				HasOptional(current => current.Layout)
					.WithMany(layout => layout.Pages)
					.HasForeignKey(current => current.LayoutId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.AccessType)
						.WithMany(accessType => accessType.Pages)
						.HasForeignKey(current => current.AccessTypeId)
						.WillCascadeOnDelete(false)
						;

				HasRequired(current => current.AuthorUser)
					.WithMany(user => user.WrittenPages)
					.HasForeignKey(current => current.AuthorUserId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.VerifierUser)
					.WithMany(user => user.VerifiedPages)
					.HasForeignKey(current => current.VerifierUserId)
					.WillCascadeOnDelete(false)
					;

				if (Utility.IsDatabaseSqlServerCompactEdition)
				{
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

		public Page()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Page),
			Name = Resources.Cms.Strings.PageKeys.AuthorUser)]
		public virtual User AuthorUser { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Page),
			Name = Resources.Cms.Strings.PageKeys.AuthorUser)]
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
			(ResourceType = typeof(Resources.Cms.Layout),
			Name = Resources.Cms.Strings.LayoutKeys.EntityName)]
		public virtual Layout Layout { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Layout),
			Name = Resources.Cms.Strings.LayoutKeys.EntityName)]
		public System.Guid? LayoutId { get; set; }
		// **********
		// **********
		// **********

		// **********
		private string _name;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Page),
			Name = Resources.Cms.Strings.PageKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
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

		public virtual PageContent PageContent { get; set; }

		public virtual System.Collections.Generic.IList<Tag> TagList { get; set; }

		// دو دستور ذيل مربوط به سطح دسترسی می‌شود
		public virtual System.Collections.Generic.IList<User> Users { get; set; }
		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }
	}
}
