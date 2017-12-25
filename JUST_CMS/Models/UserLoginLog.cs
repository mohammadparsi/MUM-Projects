namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/01/24
	/// 
	/// </summary>
	public class UserLoginLog : BaseEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserLoginLog>
		{
			public Configuration()
			{
				HasRequired(current => current.User)
					.WithMany(user => user.LoginLogs)
					.HasForeignKey(current => current.UserId)
					.WillCascadeOnDelete(false)
					;
			}
		}
		#endregion /Configuration

		public UserLoginLog()
		{
		}

		// **********
		// **********
		// **********
		public System.Guid UserId { get; protected internal set; }
		// **********

		// **********
		public virtual User User { get; protected internal set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.UserLoginLog),
			Name = Resources.Strings.UserLoginLogKeys.IsHidden)]
		public bool IsHidden { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.UserLoginLog),
			Name = Resources.Strings.UserLoginLogKeys.SessionId)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public string SessionId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.UserLoginLog),
			Name = Resources.Strings.UserLoginLogKeys.IP)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public string IP { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.UserLoginLog),
			Name = Resources.Strings.UserLoginLogKeys.LoginDateTime)]
		public System.DateTime LoginDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.UserLoginLog),
			Name = Resources.Strings.UserLoginLogKeys.LogoutDateTime)]
		public System.DateTime? LogoutDateTime { get; set; }
		// **********
	}
}
