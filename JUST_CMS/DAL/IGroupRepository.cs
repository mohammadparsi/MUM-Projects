namespace DAL
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/11/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IGroupRepository : IRepository<Models.Group>
	{
		System.Linq.IOrderedQueryable<Models.Group> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Group> GetActiveByCultureLcid(int cultureLcid);

		int GetActiveGroupCountByUserIdAndActionId(System.Guid userId, System.Guid actionId);
	}
}
