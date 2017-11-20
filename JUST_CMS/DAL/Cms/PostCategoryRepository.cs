using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/03/31
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PostCategoryRepository :
		Repository<Models.Cms.PostCategory>, IPostCategoryRepository
	{
		public PostCategoryRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IOrderedQueryable<Models.Cms.PostCategory> GetByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.PostCategory> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities =
				Get()

				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)

				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public Models.Cms.PostCategory GetByCultureLcidAndName(int cultureLcid, string name)
		{
			var varEntity =
				Get()
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault()
				;

			return (varEntity);
		}

		public Models.Cms.PostCategory
			GetByCultureLcidAndNameExceptId(int cultureLcid, string name, System.Guid id)
		{
			var varEntity =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault()
				;

			return (varEntity);
		}

		public Models.Cms.PostCategory
			GetByCultureLcidAndMasterDataCode(int cultureLcid, long masterDataCode)
		{
			var varEntity =
				Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => current.MasterDataCode == masterDataCode)
				.FirstOrDefault()
				;

			return (varEntity);
		}

		public Models.Cms.PostCategory
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
