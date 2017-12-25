using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/01/27
	/// 
	/// </summary>
	public static class GlobalApplicationSettings
	{
		static GlobalApplicationSettings()
		{
		}

		private static Models.GlobalApplicationSettings _instance;
		public static Models.GlobalApplicationSettings Instance
		{
			get
			{
				GetInstance();

				return (_instance);
			}
		}

		private static void GetInstance()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				Models.GlobalApplicationSettings oGlobalApplicationSettings =
					oUnitOfWork.GlobalApplicationSettingsRepository
					.Get()
					.FirstOrDefault();

				if (oGlobalApplicationSettings == null)
				{
					oGlobalApplicationSettings =
						Models.GlobalApplicationSettings.GetDefaultObject();
				}

				_instance = oGlobalApplicationSettings;
			}
			catch //(System.Exception ex)
			{
				_instance =
					Models.GlobalApplicationSettings.GetDefaultObject();
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
