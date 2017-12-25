namespace Models
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/03/11
	/// 
	/// </summary>
	public class State : BaseExtendedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<State>
		{
			public Configuration()
			{
				HasRequired(current => current.Country)
					.WithMany(country => country.States)
					.HasForeignKey(current => current.CountryId)
					.WillCascadeOnDelete(false)
					;
			}
		}
		#endregion /Configuration

		public State()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Country),
			Name = Resources.Strings.CountryKeys.EntityName)]
		public virtual Country Country { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Country),
			Name = Resources.Strings.CountryKeys.EntityName)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid CountryId { get; set; }
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

		public virtual System.Collections.Generic.IList<City> Cities { get; set; }
	}
}
