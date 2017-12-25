namespace Infrastructure
{
	/// <summary>
	/// Version: 1.1.0
	/// Update Date: 1393/04/08
	/// 
	/// </summary>
	public class AuthenticatedUser : System.Object, Models.IAuthenticatedUser
	{
		public static bool IsAuthenticated
		{
			get
			{
				if ((System.Web.HttpContext.Current != null) &&
					(System.Web.HttpContext.Current.Request != null) &&
					(System.Web.HttpContext.Current.Request.IsAuthenticated))
				{
					if (Sessions.AuthenticatedUser == null)
					{
						// باشد false بايد ،endResponse دقت کنيد که مقدار
						System.Web.HttpContext.Current.Response.Redirect
							("~/Account/Logout", endResponse: false);

						return (false);
					}
					else
					{
						return (true);
					}
				}
				else
				{
					if (Sessions.AuthenticatedUser != null)
					{
						Sessions.AuthenticatedUser = null;

						// باشد false بايد ،endResponse دقت کنيد که مقدار
						System.Web.HttpContext.Current.Response.Redirect
							("~/Account/Logout", endResponse: false);
					}

					return (false);
				}
			}
		}

		public static void SignOut()
		{
			// **************************************************
			DAL.UnitOfWork oUnitOfWork = null;
			try
			{
				if (Infrastructure.Sessions.AuthenticatedUser != null)
				{
					System.Guid sUserId =
						Infrastructure.Sessions.AuthenticatedUser.Id;

					string strSessionId =
						System.Web.HttpContext.Current.Session.SessionID;

					oUnitOfWork =
						new DAL.UnitOfWork();

					Models.UserLoginLog oUserLoginLog =
						oUnitOfWork.UserLoginLogRepository
						.GetBySessionIdAndUserId(strSessionId, sUserId);

					if (oUserLoginLog != null)
					{
						oUserLoginLog.LogoutDateTime = Infrastructure.Utility.Now;

						oUnitOfWork.Save();
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (oUnitOfWork != null)
				{
					oUnitOfWork.Dispose();
					oUnitOfWork = null;
				}
			}
			// **************************************************

			System.Web.Security.FormsAuthentication.SignOut();

			//Session.Clear();
			Sessions.AuthenticatedUser = null;
			//Session.Remove(Infrastructure.Sessions.AuthenticatedUserKeyName);
		}

		public AuthenticatedUser(Models.User user)
		{
			User = user;
		}

		protected Models.User User { get; set; }

		public System.Guid Id
		{
			get
			{
				return (User.Id);
			}
		}

		public string Password
		{
			get
			{
				return (User.Password.ToLower());
			}
		}

		public string EmailAddress
		{
			get
			{
				return (User.EmailAddress.ToLower());
			}
		}

		private Models.Enums.Roles? _role;
		public Models.Enums.Roles Role
		{
			get
			{
				if (_role.HasValue == false)
				{
					DAL.UnitOfWork oUnitOfWork =
						new DAL.UnitOfWork();

					Models.User oUser =
						oUnitOfWork.UserRepository.GetById(Id);

					if (oUser == null)
					{
						_role = Models.Enums.Roles.User;
					}
					else
					{
						if (oUser.Role == null)
						{
							_role = Models.Enums.Roles.User;
						}
						else
						{
							_role = oUser.Role.CodeEnum;
						}
					}
				}
				return (_role.Value);
			}
		}

		public int RoleCode
		{
			get
			{
				return ((int)Role);
			}
		}

		public long MasterDataCode
		{
			get
			{
				if (User.MasterDataCode.HasValue == false)
				{
					return (0);
				}
				else
				{
					return (User.MasterDataCode.Value);
				}
			}
		}

		public string FullName
		{
			get
			{
				return (User.FullName);
			}
		}

		public bool IsCellPhoneNumberVerified
		{
			get
			{
				return (User.IsCellPhoneNumberVerified);
			}
			set
			{
				User.IsCellPhoneNumberVerified = value;
			}
		}
	}
}
