namespace Models
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/04/14
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class RegularExpressions
	{
		public const string RegularExpressionForSalary = @"\d{0,9}";

		public const string RegularExpressionForDouble = @"[0-9.]{0,9}";

		public const string RegularExpressionForNationalCode = @"\d{10}";

		public const string RegularExpressionForInteger = @"^(\+|-)?\d+$";

		public const string RegularExpressionForUnsignedInteger = @"^\d*$";

		public const string RegularExpressionForCellPhoneNumber = @"\d{14}";

		public const string RegularExpressionForPassword = @"[a-zA-Z0-9_]{8,40}";

		public const string RegularExpressionForFileName = @"[a-zA-Z0-9_]{1,100}";

		public const string RegularExpressionForEmail = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

		public const string RegularExpressionForUsername = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"; //@"[a-zA-Z0-9_]{6,20}";

		public const string RegularExpressionForUrl =
			@"^(https?)://(((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))|((([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])))(:[1-9][0-9]+)?(/)?([/?].+)?$";
			//@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
	}
}
