﻿namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/04/08
	/// 
	/// </summary>
	public static class Sessions
	{
		private static string CultureKeyName
		{
			get
			{
				return ("Culture");
			}
		}

		public static string Culture
		{
			get
			{
				if ((System.Web.HttpContext.Current.Session[CultureKeyName] == null) ||
					(System.Web.HttpContext.Current.Session[CultureKeyName].ToString() == string.Empty))
				{
					DAL.UnitOfWork oUnitOfWork = null;

					try
					{
						oUnitOfWork = new DAL.UnitOfWork();

						Models.Culture oCulture =
							oUnitOfWork.CultureRepository.GetFirst();

						if(oCulture == null)
						{
							System.Web.HttpContext.Current.Session[CultureKeyName] = "fa-IR";
						}
						else
						{
							System.Web.HttpContext.Current.Session[CultureKeyName] = oCulture.Name;
						}
					}
					catch
					{
						System.Web.HttpContext.Current.Session[CultureKeyName] = "fa-IR";
					}
					finally
					{
						if(oUnitOfWork != null)
						{
							oUnitOfWork.Dispose();
							oUnitOfWork = null;
						}
					}
				}

				return (System.Web.HttpContext.Current.Session[CultureKeyName].ToString());
			}
			set
			{
				System.Web.HttpContext.Current.Session[CultureKeyName] = value;
			}
		}

		public static string AuthenticatedUserKeyName
		{
			get
			{
				return ("AuthenticatedUser");
			}
		}

		public static AuthenticatedUser AuthenticatedUser
		{
			get
			{
				AuthenticatedUser oAuthenticatedUser =
					System.Web.HttpContext.Current.Session[AuthenticatedUserKeyName] as AuthenticatedUser;

				return (oAuthenticatedUser);
			}
			set
			{
				System.Web.HttpContext.Current.Session[AuthenticatedUserKeyName] = value;
			}
		}
	}
}
