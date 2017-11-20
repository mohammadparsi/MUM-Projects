namespace Models
{
	public static class Utility
	{
		static Utility()
		{
		}

		public static bool IsDatabaseSqlServerCompactEdition
		{
			get
			{
				bool blnResult = false;

				if (System.Configuration.ConfigurationManager.ConnectionStrings.Count != 0)
				{
					System.Configuration.ConnectionStringSettings oConnectionStringSettings =
						System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"];

					if (oConnectionStringSettings != null)
					{
						if (oConnectionStringSettings.ConnectionString.Contains("MyDatabase.sdf"))
						{
							blnResult = true;
						}
					}
				}

				return (blnResult);
			}
		}
	}
}
