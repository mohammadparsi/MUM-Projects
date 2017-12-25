namespace DAL
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1392/11/28
	/// 
	/// </summary>
	public interface IApplicationSettingsRepository : IRepository<Models.ApplicationSettings>
	{
		Models.ApplicationSettings GetByCultureLcid(int cultureLcid);
	}
}
