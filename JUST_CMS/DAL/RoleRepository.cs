using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1392/12/19
	/// 
	/// </summary>
	public class RoleRepository : Repository<Models.Role>, IRoleRepository
	{
		public RoleRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Role GetByCultureLcidAndCode(int cultureLcid, int code)
		{
			Models.Role oRole = Get()
				.Where(current => current.Code == code)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault()
				;

			return (oRole);
		}

		public IOrderedQueryable<Models.Role>
			GetByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code)
		{
			var varEntities = Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.Code <= code)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Code)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Role>
			GetActiveByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.Code <= code)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Code)
				;

			return (varEntities);
		}
	}
}
