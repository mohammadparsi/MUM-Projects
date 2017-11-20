namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/11/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectAction : BaseAreaControllerAction
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProjectAction>
		{
			public Configuration()
			{
				HasRequired(current => current.ProjectController)
					.WithMany(projectController => projectController.ProjectActions)
					.HasForeignKey(current => current.ProjectControllerId)
					.WillCascadeOnDelete(true)
					;

				HasOptional(current => current.Layout)
					.WithMany(layout => layout.ProjectActions)
					.HasForeignKey(current => current.LayoutId)
					.WillCascadeOnDelete(true)
					;

				if (Utility.IsDatabaseSqlServerCompactEdition)
				{
					Property(current => current.Description)
						.HasColumnType("ntext")
						.IsMaxLength()
						;
				}
			}
		}
		#endregion /Configuration

		#region Static Method(s)
		public static string GetDisplayName(Models.ProjectAction projectAction)
		{
			if (projectAction == null)
			{
				return (string.Empty);
			}

			string strResult = projectAction.DisplayName;

			if (strResult == null)
			{
				strResult = projectAction.Name;
			}
			else
			{
				strResult = strResult.Trim();

				if (strResult == string.Empty)
				{
					strResult = projectAction.Name;
				}
			}

			return (strResult);
		}
		#endregion /Static Method(s)

		public ProjectAction()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ProjectController),
			Name = Resources.Strings.ProjectControllerKeys.EntityName)]
		public virtual ProjectController ProjectController { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ProjectController),
			Name = Resources.Strings.ProjectControllerKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid ProjectControllerId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Cms.Layout),
			Name = Resources.Cms.Strings.LayoutKeys.EntityName)]
		public virtual Cms.Layout Layout { get; set; }
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
		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public new string Name { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<User> Users { get; set; }
		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }
	}
}
