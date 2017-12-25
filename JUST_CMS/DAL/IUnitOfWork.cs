namespace DAL
{
	/// <summary>
	/// Version: 1.2.9
	/// Update Date: 1393/03/11
	/// 
	/// </summary>
	public interface IUnitOfWork : System.IDisposable
	{
		Cms.IUnitOfWork CmsUnitOfWork { get; }

		ICityRepository CityRepository { get; }
		IRoleRepository RoleRepository { get; }
		IUserRepository UserRepository { get; }
		IGroupRepository GroupRepository { get; }
		IStateRepository StateRepository { get; }
		IGenderRepository GenderRepository { get; }
		ICountryRepository CountryRepository { get; }
		ICultureRepository CultureRepository { get; }
		IAccessTypeRepository AccessTypeRepository { get; }
		IHttpRefererRepository HttpRefererRepository { get; }
		IProjectAreaRepository ProjectAreaRepository { get; }
		IUserLoginLogRepository UserLoginLogRepository { get; }
		IProjectActionRepository ProjectActionRepository { get; }
		IProjectControllerRepository ProjectControllerRepository { get; }

		IMailSettingsRepository MailSettingsRepository { get; }
		IApplicationSettingsRepository ApplicationSettingsRepository { get; }
		IGlobalApplicationSettingsRepository GlobalApplicationSettingsRepository { get; }

		void Save();
	}
}
