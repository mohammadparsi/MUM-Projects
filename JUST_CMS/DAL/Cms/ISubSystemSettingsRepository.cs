using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ISubSystemSettingsRepository : IRepository<Models.Cms.SubSystemSettings>
	{
		Models.Cms.SubSystemSettings GetByCultureLcid(int cultureLcid);
	}
}
