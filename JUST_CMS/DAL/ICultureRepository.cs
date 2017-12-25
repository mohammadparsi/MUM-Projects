namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1392/02/09
	/// 
	/// </summary>
	public interface ICultureRepository : IRepository<Models.Culture>
	{
		Models.Culture GetFirst();

		Models.Culture GetByLcid(int lcid);

		System.Linq.IOrderedQueryable<Models.Culture> GetActive();
	}
}
