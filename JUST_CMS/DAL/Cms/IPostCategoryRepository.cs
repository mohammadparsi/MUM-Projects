namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/03/31
	/// 
	/// </summary>
	public interface IPostCategoryRepository : IRepository<Models.Cms.PostCategory>
	{
		System.Linq.IOrderedQueryable<Models.Cms.PostCategory> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Cms.PostCategory> GetActiveByCultureLcid(int cultureLcid);

		Models.Cms.PostCategory GetByCultureLcidAndName(int cultureLcid, string name);

		Models.Cms.PostCategory
			GetByCultureLcidAndNameExceptId(int cultureLcid, string name, System.Guid id);

		Models.Cms.PostCategory
			GetByCultureLcidAndMasterDataCode(int cultureLcid, long masterDataCode);

		Models.Cms.PostCategory
			GetByCultureLcidAndMasterDataCodeExceptId(int cultureLcid, long masterDataCode, System.Guid id);
	}
}
