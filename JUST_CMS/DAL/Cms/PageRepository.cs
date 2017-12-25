using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.9
	/// Update Date: 1392/12/06
	/// 
	/// </summary>
	public class PageRepository : Repository<Models.Cms.Page>, IPageRepository
	{
		public PageRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Cms.Page GetByCultureLcidAndName(int cultureLcid, string name)
		{
			Models.Cms.Page oPage =
				Get()
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (oPage);
		}

		public Models.Cms.Page GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id)
		{
			Models.Cms.Page oPage =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (oPage);
		}

		public int GetPageCountByLayoutId(System.Guid layoutId)
		{
			int intPageCount =
				Get()
				.Where(current => current.LayoutId == layoutId)
				.Count()
				;

			return (intPageCount);
		}

		public IOrderedQueryable<Models.Cms.Page> GetSpecial()
		{
			var varEntities =
				Get()
				.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
					Models.Enums.AccessTypes.Special)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Cms.Page> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()
				.Include(current => current.Layout)
				.Include(current => current.AccessType)
				.Include(current => current.AuthorUser)

				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.IsActive)
				.ThenBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}
	}
}
