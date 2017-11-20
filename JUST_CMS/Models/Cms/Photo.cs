//namespace Models.Cms
//{
//	/// <summary>
//	/// Version: 1.0.4
//	/// Update Date: 1392/12/06
//	/// Developer: Mr. Dariush Tasdighi
//	/// </summary>
//	[System.ComponentModel.DataAnnotations.Schema.Table("Photos", Schema = "Cms")]
//	public class Photo : File, ITag
//	{
//		#region Configuration
//		internal class Configuration :
//			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Photo>
//		{
//			public Configuration()
//			{
//				HasRequired(current => current.PhotoAlbum)
//					.WithMany(photoAlbum => photoAlbum.Photos)
//					.HasForeignKey(current => current.PhotoAlbumId)
//					.WillCascadeOnDelete(true)
//					;
//			}
//		}
//		#endregion /Configuration

//		public Photo()
//		{
//		}

//		// **********
//		// **********
//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.PhotoAlbum),
//			Name = Resources.Strings.PhotoAlbumKeys.EntityName)]
//		public virtual PhotoAlbum PhotoAlbum { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.PhotoAlbum),
//			Name = Resources.Strings.PhotoAlbumKeys.EntityName)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
//		public System.Guid PhotoAlbumId { get; set; }
//		// **********
//		// **********
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Photo),
//			Name = Resources.Strings.PhotoKeys.Width)]
//		public int Width { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Photo),
//			Name = Resources.Strings.PhotoKeys.Height)]
//		public int Height { get; set; }
//		// **********

//		public virtual System.Collections.Generic.IList<Tag> TagList { get; set; }
//	}
//}
