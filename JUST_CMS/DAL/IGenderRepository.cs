namespace DAL
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/03/11
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IGenderRepository : IRepository<Models.Gender>
	{
		System.Linq.IOrderedQueryable<Models.Gender> GetByCultureLcid(int cultureLcid);

		System.Linq.IOrderedQueryable<Models.Gender> GetActiveByCultureLcid(int cultureLcid);

		Models.Gender
			GetByCultureLcidAndMasterDataCodeExceptId(int cultureLcid, long masterDataCode, System.Guid id);
	}
}
