using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/04/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class SubSystemSettingsRepository :
		Repository<Models.Cms.SubSystemSettings>, ISubSystemSettingsRepository
	{
		public SubSystemSettingsRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Cms.SubSystemSettings GetByCultureLcid(int cultureLcid)
		{
			Models.Cms.SubSystemSettings oSubSystemSettings =
				Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault()
				;

			return (oSubSystemSettings);
		}
	}
}
