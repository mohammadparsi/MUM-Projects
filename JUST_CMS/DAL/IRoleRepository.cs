namespace DAL
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1392/12/19
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public interface IRoleRepository : IRepository<Models.Role>
	{
		Models.Role GetByCultureLcidAndCode(int cultureLcid, int code);

		System.Linq.IOrderedQueryable<Models.Role>
			GetByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code);

		System.Linq.IOrderedQueryable<Models.Role>
			GetActiveByCultureLcidAndLessThanOrEqualToCode(int cultureLcid, int code);
	}
}
