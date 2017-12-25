using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1392/11/29
	/// 
	/// </summary>
	public class GroupRepository : Repository<Models.Group>, IGroupRepository
	{
		public GroupRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public IOrderedQueryable<Models.Group> GetByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.Group> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				;

			return (varEntities);
		}

		public int GetActiveGroupCountByUserIdAndActionId(System.Guid userId, System.Guid actionId)
		{
			int intGroupCount =
				Get()
				.Where(current => current.IsActive)
				.Where(current => current.Users.Any(user => user.Id == userId))
				.Where(current => current.ProjectActions.Any(action => action.Id == actionId))
				.Count()
				;

			return (intGroupCount);
		}
	}
}
