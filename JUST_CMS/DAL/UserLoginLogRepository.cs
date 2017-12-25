﻿using System.Linq;
using System.Data.Entity;

namespace DAL
{
	/// <summary>
	/// Version: 1.0.1
	/// Update Date: 1393/01/23
	/// 
	/// </summary>
	public class UserLoginLogRepository : Repository<Models.UserLoginLog>, IUserLoginLogRepository
	{
		public UserLoginLogRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public System.Linq.IQueryable<Models.UserLoginLog> GetAuthenticatedUsers()
		{
			var varResult =
				Get()
				.Where(current => current.LogoutDateTime.HasValue == false)
				;

			return (varResult);
		}

		public System.Linq.IQueryable<Models.UserLoginLog> GetByUserId(System.Guid userId)
		{
			var varResult =
				Get()
				.Where(current => current.UserId == userId)
				.OrderByDescending(current => current.LoginDateTime)
				.Take(50)
				;

			return (varResult);
		}

		public Models.UserLoginLog GetBySessionIdAndUserId(string sessionId, System.Guid userId)
		{
			Models.UserLoginLog oUserLoginLog =
				Get()
				.Where(current => current.UserId == userId)
				.Where(current => current.SessionId == sessionId)
				.FirstOrDefault()
				;

			return (oUserLoginLog);
		}

		public System.Linq.IOrderedQueryable<Models.UserLoginLog> GetBySessionId(string sessionId)
		{
			var varResult =
				Get()
				.Where(current => current.SessionId == sessionId)
				.OrderBy(current => current.UserId)
				;

			return (varResult);
		}
	}
}
