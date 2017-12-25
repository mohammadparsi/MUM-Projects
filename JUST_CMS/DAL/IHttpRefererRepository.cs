namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/25
	/// 
	/// </summary>
	public interface IHttpRefererRepository : IRepository<Models.HttpReferer>
	{
		System.Linq.IOrderedQueryable<Models.HttpReferer> GetPublic();
	}
}
