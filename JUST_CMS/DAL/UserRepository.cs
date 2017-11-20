using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.1.4
	/// Update Date: 1393/03/18
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class UserRepository : Repository<Models.User>, IUserRepository
	{
		public UserRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.User GetByEmailAddress(string emailAddress)
		{
			Models.User oUser =
				Get()
				.Where(current => string.Compare(current.EmailAddress, emailAddress,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByCellPhoneNumber(string cellPhoneNumber)
		{
			Models.User oUser =
				Get()
				.Where(current =>
					string.Compare(current.CellPhoneNumber, cellPhoneNumber,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByNationalCode(string nationalCode)
		{
			Models.User oUser =
				Get()
				.Where(current =>
					string.Compare(current.NationalCode, nationalCode,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByEmailAddressVerificationKey(string verificationKey)
		{
			System.Guid sVerificationKey =
				new System.Guid(verificationKey);

			Models.User oUser =
				Get()
				.Where(current => current.EmailAddressVerificationKey == sVerificationKey)
				.FirstOrDefault()
				;

			return (oUser);
		}

		public Models.User GetByCellPhoneNumberVerificationKey(string verificationKey)
		{
			verificationKey =
				verificationKey.Replace("o", "0").Replace("O", "0");

			Models.User oUser =
				Get()
				.Where(current =>
					string.Compare(current.CellPhoneNumberVerificationKey, verificationKey,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault()
				;

			return (oUser);
		}

		public bool CheckDirectAccessForAction(System.Guid userId, System.Guid actionId)
		{
			int intCount =
				Get()
				.Where(current => current.Id == userId)
				.Where(current => current.ProjectActions.Any(action => action.Id == actionId))
				.Count()
				;

			if (intCount == 0)
			{
				return (false);
			}
			else
			{
				return (true);
			}
		}

		public Models.User GetByEmailAddressExceptId(string emailAddress, System.Guid id)
		{
			Models.User oUser =
				Get()
				.Where(current => current.Id != id)
				.Where(current =>
					string.Compare(current.EmailAddress, emailAddress,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByNationalCodeExceptId(string nationalCode, System.Guid id)
		{
			Models.User oUser =
				Get()
				.Where(current => current.Id != id)
				.Where(current =>
					string.Compare(current.NationalCode, nationalCode,
					System.StringComparison.InvariantCultureIgnoreCase) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public Models.User GetByCellPhoneNumberExceptId(string cellPhoneNumber, System.Guid id)
		{
			Models.User oUser =
				Get()
				.Where(current => current.Id != id)
				.Where(current =>
					string.Compare(current.CellPhoneNumber, cellPhoneNumber, true) == 0)
				.FirstOrDefault();

			return (oUser);
		}

		public IOrderedQueryable<Models.User> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.GenderId)
				.ThenBy(current => current.FirstName)
				.ThenBy(current => current.LastName)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.User> GetActiveCompaniesByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.MasterDataCode != 0)
				.Where(current => current.CultureLcid == cultureLcid)
				.Where(current => string.IsNullOrEmpty(current.CompanyName) == false)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.CompanyName)
				;

			return (varEntities);
		}

		public IOrderedQueryable<Models.User> GetActiveRecipientsByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.IsDirectContactable)
				.Where(current => current.CultureLcid == cultureLcid)
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.GenderId)
				.ThenBy(current => current.FirstName)
				.ThenBy(current => current.LastName)
				;

			return (varEntities);
		}
	}
}
