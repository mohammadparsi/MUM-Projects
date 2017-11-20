using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/11/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class MailSettingsRepository : Repository<Models.MailSettings>, IMailSettingsRepository
	{
		public MailSettingsRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IOrderedQueryable<Models.MailSettings> GetByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenByDescending(current => current.UpdateDateTime)
				;

			return (varEntities);
		}


		public IOrderedQueryable<Models.MailSettings> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}
	}
}
