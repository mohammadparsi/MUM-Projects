namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/12/08
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface ITagRepository : IRepository<Models.Cms.Tag>
	{
		Models.Cms.Tag GetByCultureLcidAndName
			(int cultureLcid, string name);

		Models.Cms.Tag GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id);

		System.Linq.IOrderedQueryable<ViewModels.Areas.Cms.Tag.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid);
	}
}
