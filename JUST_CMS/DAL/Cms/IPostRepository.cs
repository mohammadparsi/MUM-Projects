namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public interface IPostRepository : IRepository<Models.Cms.Post>
	{
		//System.Linq.IOrderedQueryable<Models.Cms.Post>
		//	GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<ViewModels.Areas.Cms.Post.IndexViewModel>
			GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Cms.Post>
			GetActiveByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Cms.Post>
			GetActiveByCultureLcidAndCategoryId(int cultureLcid, System.Guid categoryId);
	}
}
