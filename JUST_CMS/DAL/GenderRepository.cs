using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class GenderRepository : Repository<Models.Gender>, IGenderRepository
	{
		public GenderRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IOrderedQueryable<Models.Gender> GetByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name1)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Gender> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name1)
				;

			return (varEntities);
		}

		public Models.Gender
			GetByCultureLcidAndMasterDataCodeExceptId(int cultureLcid, long masterDataCode, System.Guid id)
		{
			var varEntity =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.MasterDataCode == masterDataCode)
				.FirstOrDefault()
				;

			return (varEntity);
		}
	}
}
