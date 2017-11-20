namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/11/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class ProjectController : BaseAreaControllerAction
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProjectController>
		{
			public Configuration()
			{
				HasRequired(current => current.ProjectArea)
					.WithMany(projectArea => projectArea.ProjectControllers)
					.HasForeignKey(current => current.ProjectAreaId)
					.WillCascadeOnDelete(false)
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
		public static string GetDisplayName(Models.ProjectController projectController)
		{
			if (projectController == null)
			{
				return (string.Empty);
			}

			string strResult = projectController.DisplayName;

			if (strResult == null)
			{
				strResult = projectController.Name;
			}
			else
			{
				strResult = strResult.Trim();

				if (strResult == string.Empty)
				{
					strResult = projectController.Name;
				}
			}

			return (strResult);
		}
		#endregion /Static Method(s)

		public ProjectController()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ProjectArea),
			Name = Resources.Strings.ProjectAreaKeys.EntityName)]
		public virtual ProjectArea ProjectArea { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.ProjectArea),
			Name = Resources.Strings.ProjectAreaKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid ProjectAreaId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public new string Name { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }
	}
}
