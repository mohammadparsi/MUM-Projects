namespace Models
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/03/11
	/// 
	/// </summary>
	public class Country : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Country>
		{
			public Configuration()
			{
			}
		}
		#endregion /Configuration

		public Country()
		{
		}

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

		public virtual System.Collections.Generic.IList<State> States { get; set; }
	}
}
