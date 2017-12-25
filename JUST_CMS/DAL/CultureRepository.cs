using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/02/09
	/// 
	/// </summary>
	public class CultureRepository : Repository<Models.Culture>, ICultureRepository
	{
		public CultureRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Culture GetFirst()
		{
			return (GetActive().FirstOrDefault());
		}

		public Models.Culture GetByLcid(int lcid)
		{
			Models.Culture oCulture = Get()
				.Where(current => current.Lcid == lcid)
				.FirstOrDefault()
				;

			return (oCulture);
		}

		public IOrderedQueryable<Models.Culture> GetActive()
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}
	}
}
