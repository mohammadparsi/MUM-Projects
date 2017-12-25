namespace Models
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/03/07
	/// 
	/// </summary>
	[System.Serializable]
	public abstract class BaseEntity : System.Object
	{
		public BaseEntity()
		{
			Id = System.Guid.NewGuid();
		}

		// **********
		[System.ComponentModel.DataAnnotations.Key]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.General),
			Name = Resources.Strings.GeneralKeys.Id)]

		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 0)]
		public System.Guid Id { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.General),
			Name = Resources.Strings.GeneralKeys.MasterDataCode)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]

		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
		public long? MasterDataCode { get; set; }
		// **********
	}
}
