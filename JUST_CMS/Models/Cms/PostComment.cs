namespace Models.Cms
{
	[System.ComponentModel.DataAnnotations.Schema.Table("PostComments", Schema = "Cms")]
	public class PostComment : BaseExtendedEntity
	{
		#region Configuration
		public class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PostComment>
		{
			public Configuration()
			{
				HasRequired(current => current.Post)
					.WithMany(post => post.Comments)
					.HasForeignKey(current => current.PostId)
					.WillCascadeOnDelete(false)
					;

				HasOptional(current => current.User)
					.WithMany(post => post.PostComments)
					.HasForeignKey(current => current.UserId)
					.WillCascadeOnDelete(false)
					;
			}
		}
		#endregion /Configuration

		public PostComment()
		{
		}

		public Post Post { get; set; }

		public System.Guid PostId { get; set; }

		public User User { get; set; }

		public System.Guid? UserId { get; set; }

		public bool IsPrivate { get; set; }

		public bool IsRead { get; set; }

		public string UserIP { get; set; }

		public string UserUrl { get; set; }

		public string UserEmail { get; set; }

		public string UserFullName { get; set; }

		public string UserSubject { get; set; }

		public string UserDescription { get; set; }
	}
}
