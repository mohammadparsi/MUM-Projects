namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/04/03
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class City : BaseExtendedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<City>
		{
			public Configuration()
			{
				HasRequired(current => current.State)
					.WithMany(state => state.Cities)
					.HasForeignKey(current => current.StateId)
					.WillCascadeOnDelete(false)
					;
			}
		}
		#endregion /Configuration

		public City()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.State),
			Name = Resources.Strings.StateKeys.EntityName)]
		public virtual State State { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.State),
			Name = Resources.Strings.StateKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid StateId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.General),
			Name = Resources.Strings.GeneralKeys.Name)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 100,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string Name { get; set; }
		// **********

		public override string ToString()
		{
			string strResult =
				string.Format("{0} - {1} - {2}", State.Country.Name, State.Name, Name);

			return (strResult);
		}
	}
}
