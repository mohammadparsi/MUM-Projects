//namespace Models.Cms
//{
//	/// <summary>
//	/// Version: 1.0.3
//	/// Update Date: 1392/06/18
//	/// 
//	/// </summary>
//	[System.ComponentModel.DataAnnotations.Schema.Table("PhotoAlbum", Schema = "Cms")]
//	public class PhotoAlbum : Folder
//	{
//		#region Configuration
//		internal class Configuration :
//			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PhotoAlbum>
//		{
//			public Configuration()
//			{
//				HasOptional(current => current.ParentPhotoAlbum)
//					.WithMany(photoAlbum => photoAlbum.PhotoAlbums)
//					.HasForeignKey(current => current.ParentPhotoAlbumId)
//					.WillCascadeOnDelete(false)
//					;

//				HasRequired(current => current.AccessType)
//					.WithMany(accessType => accessType.PhotoAlbums)
//					.HasForeignKey(current => current.AccessTypeId)
//					.WillCascadeOnDelete(false)
//					;
//			}
//		}
//		#endregion /Configuration

//		public PhotoAlbum()
//		{
//		}

//		// **********
//		// **********
//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.PhotoAlbum),
//			Name = Resources.Strings.PhotoAlbumKeys.ParentPhotoAlbum)]
//		public virtual PhotoAlbum ParentPhotoAlbum { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.PhotoAlbum),
//			Name = Resources.Strings.PhotoAlbumKeys.ParentPhotoAlbum)]
//		public System.Guid? ParentPhotoAlbumId { get; set; }
//		// **********
//		// **********
//		// **********

//		// **********
//		// **********
//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.AccessType),
//			Name = Resources.Strings.AccessTypeKeys.EntityName)]
//		public virtual AccessType AccessType { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.AccessType),
//			Name = Resources.Strings.AccessTypeKeys.EntityName)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
//		public System.Guid AccessTypeId { get; set; }
//		// **********

//		// **********
//		public Enums.AccessTypes AccessTypeEnum
//		{
//			get
//			{
//				if (AccessType == null)
//				{
//					return (Enums.AccessTypes.Registered);
//				}
//				else
//				{
//					return ((Enums.AccessTypes)AccessType.Code);
//				}
//			}
//		}
//		// **********
//		// **********
//		// **********

//		public virtual System.Collections.Generic.IList<User> Users { get; set; }
//		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }
//		public virtual System.Collections.Generic.IList<Photo> Photos { get; set; }
//		public virtual System.Collections.Generic.IList<PhotoAlbum> PhotoAlbums { get; set; }
//	}
//}
