namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/11/30
	/// 
	/// </summary>
	public class ProjectArea : BaseAreaControllerAction
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ProjectArea>
		{
			public Configuration()
			{
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
		public static string GetDisplayName(Models.ProjectArea projectArea)
		{
			if (projectArea == null)
			{
				return (string.Empty);
			}

			string strResult = projectArea.DisplayName;

			if (strResult == null)
			{
				strResult = projectArea.Name;
			}
			else
			{
				strResult = strResult.Trim();

				if (strResult == string.Empty)
				{
					strResult = projectArea.Name;
				}
			}

			return (strResult);
		}
		#endregion /Static Method(s)

		public ProjectArea()
		{
		}

		public virtual System.Collections.Generic.IList<ProjectController> ProjectControllers { get; set; }
	}
}
