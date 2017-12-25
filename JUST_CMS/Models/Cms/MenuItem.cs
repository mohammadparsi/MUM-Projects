//namespace Models.Cms
//{
//	/// <summary>
//	/// Version: 1.0.6
//	/// Update Date: 1392/06/17
//	/// 
//	/// </summary>
//	[System.ComponentModel.DataAnnotations.Schema.Table("MenuItems", Schema = "Cms")]
//	public class MenuItem : BaseEntity
//	{
//		#region Configuration
//		internal class Configuration :
//			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<MenuItem>
//		{
//			public Configuration()
//			{
//				HasRequired(current => current.Menu)
//					.WithMany(menu => menu.MenuItems)
//					.HasForeignKey(current => current.MenuId)
//					.WillCascadeOnDelete(false)
//					;

//				HasOptional(current => current.ParentMenuItem)
//					.WithMany(menuItem => menuItem.MenuItems)
//					.HasForeignKey(current => current.ParentMenuItemId)
//					.WillCascadeOnDelete(false)
//					;
//			}
//		}
//		#endregion /Configuration

//		public MenuItem()
//		{
//		}

//		// **********
//		// **********
//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Menu),
//			Name = Resources.Strings.MenuKeys.EntityName)]
//		public virtual Menu Menu { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.Menu),
//			Name = Resources.Strings.MenuKeys.EntityName)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
//		public System.Guid MenuId { get; set; }
//		// **********
//		// **********
//		// **********

//		// **********
//		// **********
//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.ParentMenuItem)]
//		public virtual MenuItem ParentMenuItem { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.ParentMenuItem)]
//		public System.Guid? ParentMenuItemId { get; set; }
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

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.Selectable)]
//		public bool Selectable { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.OpenInNewTabOrWindow)]
//		public bool OpenInNewTabOrWindow { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.Text)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

//		[System.ComponentModel.DataAnnotations.StringLength
//			(maximumLength: 100,
//			ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
//		public string Text { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.ToolTip)]
//		public string ToolTip { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.NavigateUrl)]

//		[System.ComponentModel.DataAnnotations.Required
//			(ErrorMessageResourceType = typeof(Resources.Messages),
//			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
//		public string NavigateUrl { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.ImageUrl)]

//		[System.ComponentModel.DataAnnotations.DataType
//			(System.ComponentModel.DataAnnotations.DataType.ImageUrl)]
//		public string ImageUrl { get; set; }
//		// **********

//		// **********
//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.PopoutImageUrl)]

//		[System.ComponentModel.DataAnnotations.DataType
//			(System.ComponentModel.DataAnnotations.DataType.ImageUrl)]
//		public string PopoutImageUrl { get; set; }
//		// **********

//		// **********
//		[System.Web.Mvc.AllowHtml]

//		[System.ComponentModel.DataAnnotations.Display
//			(ResourceType = typeof(Resources.MenuItem),
//			Name = Resources.Strings.MenuItemKeys.Description)]

//		[System.ComponentModel.DataAnnotations.DataType
//			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
//		public string Description { get; set; }
//		// **********

//		public virtual System.Collections.Generic.IList<User> Users { get; set; }
//		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }
//		public virtual System.Collections.Generic.IList<MenuItem> MenuItems { get; set; }
//	}
//}
