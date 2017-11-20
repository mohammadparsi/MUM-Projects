//namespace Models.Cms
//{
//	/// <summary>
//	/// Version: 1.0.1
//	/// Update Date: 1392/06/17
//	/// Developer: Mr. Dariush Tasdighi
//	/// </summary>
//	[System.ComponentModel.DataAnnotations.Schema.Table("Menus", Schema = "Cms")]
//	public class Menu : BaseLocalizedEntity
//	{
//		#region Configuration
//		internal class Configuration :
//			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Menu>
//		{
//			public Configuration()
//			{
//				HasOptional(current => current.Role)
//					.WithMany(role => role.Menus)
//					.HasForeignKey(current => current.RoleId)
//					.WillCascadeOnDelete(false)
//					;
//			}
//		}
//		#endregion /Configuration

//		public Menu()
//		{
//		}

//		// **********
//		// **********
//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Role),
//			Name = Resources.Strings.RoleKeys.EntityName)]
//		public virtual Role Role { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Role),
//			Name = Resources.Strings.RoleKeys.EntityName)]
//		public System.Guid? RoleId { get; set; }
//		// **********
//		// **********
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Menu),
//			Name = Resources.Strings.MenuKeys.Name)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

//		[System.ComponentModel.DataAnnotations.StringLength
//			(maximumLength: 100,
//			ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
//		public string Name { get; set; }
//		// **********

//		// **********
//		[System.Web.Mvc.AllowHtml]

//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Menu),
//			Name = Resources.Strings.MenuKeys.Description)]

//		[System.ComponentModel.DataAnnotations.DataType
//			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
//		public string Description { get; set; }
//		// **********

//		public virtual System.Collections.Generic.IList<MenuItem> MenuItems { get; set; }
//	}
//}
