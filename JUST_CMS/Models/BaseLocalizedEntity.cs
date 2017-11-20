namespace Models
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/02/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.Serializable]
	public abstract class BaseLocalizedEntity : BaseExtendedEntity
	{
		public BaseLocalizedEntity()
		{
			CultureLcid =
				System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Base),
			Name = Resources.Strings.BaseKeys.CultureId)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public System.Guid CultureId { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.Base),
			Name = Resources.Strings.BaseKeys.CultureLcid)]

		[System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
		[System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
		public int CultureLcid { get; set; }
		// **********
	}
}
