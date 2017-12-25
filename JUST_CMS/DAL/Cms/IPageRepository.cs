namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.8
	/// Update Date: 1392/12/04
	/// 
	/// </summary>
	public interface IPageRepository : IRepository<Models.Cms.Page>
	{
		int GetPageCountByLayoutId(System.Guid layoutId);

		Models.Cms.Page GetByCultureLcidAndName
			(int cultureLcid, string name);

		Models.Cms.Page GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id);

		System.Linq.IOrderedQueryable<Models.Cms.Page> GetSpecial();

		System.Linq.IOrderedQueryable<Models.Cms.Page> GetByCultureLcid(int cultureLcid);
	}
}
