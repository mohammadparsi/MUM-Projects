namespace Models
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/04/03
	/// 
	/// </summary>
	public class Gender : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Gender>
		{
			public Configuration()
			{
			}
		}
		#endregion /Configuration

		public Gender()
		{
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Gender),
			Name = Resources.Strings.GenderKeys.Code)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int Code { get; set; }
		// **********

		// **********
		public Enums.Genders CodeEnum
		{
			get
			{
				return ((Enums.Genders)Code);
			}
		}
		// **********
		// **********
		// **********

		// **********
		/// <summary>
		/// خانم، آقا
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Gender),
			Name = Resources.Strings.GenderKeys.Name1)]
		public string Name1 { get; set; }
		// **********

		// **********
		/// <summary>
		/// خانم، آقای
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Gender),
			Name = Resources.Strings.GenderKeys.Name2)]
		public string Name2 { get; set; }
		// **********

		// **********
		/// <summary>
		/// زن، مرد
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Gender),
			Name = Resources.Strings.GenderKeys.Name3)]
		public string Name3 { get; set; }
		// **********

		// **********
		/// <summary>
		/// مونث، مذکر
		/// </summary>
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Gender),
			Name = Resources.Strings.GenderKeys.Name4)]
		public string Name4 { get; set; }
		// **********

		public virtual System.Collections.Generic.IList<User> Users { get; set; }
	}
}
