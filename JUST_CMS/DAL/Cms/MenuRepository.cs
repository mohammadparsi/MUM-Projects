//using System.Linq;
//using System.Data.Entity;

//namespace DAL.Cms
//{
//	/// <summary>
//	/// Version: 1.0.2
//	/// Update Date: 1392/11/28
//	/// 
//	/// </summary>
//	public class MenuRepository : Repository<Models.Cms.Menu>, IMenuRepository
//	{
//		public MenuRepository(Models.DatabaseContext databaseContext)
//			: base(databaseContext)
//		{
//		}

//		public IOrderedQueryable<Models.Cms.Menu> GetActiveByCultureLcid(int cultureLcid)
//		{
//			var varEntities = Get()
//				.Where(current => current.IsActive)
//				.Where(current => current.IsDeleted == false)
//				.Where(current => current.CultureLcid == cultureLcid)
//				.OrderBy(current => current.Ordering)
//				.ThenBy(current => current.Name)
//				;

//			return (varEntities);
//		}
//	}
//}
