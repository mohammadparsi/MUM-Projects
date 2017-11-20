namespace Infrastructure.Cms
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class SubSystemSettings
	{
		static SubSystemSettings()
		{
		}

		private static Models.Cms.SubSystemSettings _instance;
		public static Models.Cms.SubSystemSettings Instance
		{
			get
			{
				// دستورات ذيل برای چندزبانگی کار نمی‌کند
				//if (_instance == null)
				//{
				//	GetInstanceByCultureLcid();
				//}
				//return (_instance);

				GetInstanceByCultureLcid();

				return (_instance);
			}
		}

		private static void GetInstanceByCultureLcid()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				int intCultureLcid =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				Models.Cms.SubSystemSettings oSubSystemSettings =
					oUnitOfWork.CmsUnitOfWork.SubSystemSettingsRepository
					.GetByCultureLcid(intCultureLcid);

				if (oSubSystemSettings == null)
				{
					oSubSystemSettings =
						Models.Cms.SubSystemSettings.GetDefaultObject();
				}

				_instance = oSubSystemSettings;
			}
			catch //(System.Exception ex)
			{
				_instance =
					Models.Cms.SubSystemSettings.GetDefaultObject();
			}
			finally
			{
				if (oUnitOfWork != null)
				{
					oUnitOfWork.Dispose();
					oUnitOfWork = null;
				}
			}
		}
	}
}
