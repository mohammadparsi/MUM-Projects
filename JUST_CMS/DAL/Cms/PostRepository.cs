using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.6
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public class PostRepository : Repository<Models.Cms.Post>, IPostRepository
	{
		public PostRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		//public IOrderedQueryable<Models.Cms.Post> GetByCultureLcid(int cultureLcid)
		//{
		//	var varEntities = Get()
		//		.Where(current => current.CultureLcid == cultureLcid)
		//		.OrderByDescending(current => current.UpdateDateTime)
		//		;

		//	return (varEntities);
		//}

		public System.Linq.IOrderedQueryable<ViewModels.Areas.Cms.Post.IndexViewModel>
			GetByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.CultureLcid == cultureLcid)
				.Select(current => new ViewModels.Areas.Cms.Post.IndexViewModel
				{
					Id = current.Id,

					Title = current.Title,

					IsActive = current.IsActive,
					IsDeleted = current.IsDeleted,
					IsFeatured = current.IsFeatured,
					IsVerified = current.IsVerified,

					CategoryId = current.CategoryId,
					CategoryName = current.Category.Name,

					CommentCount = current.Comments.Count(),

					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime,

					AuthorUserId = current.AuthorUserId,
					AuthorUserGender = current.AuthorUser.Gender.Name2,
					AuthorUserLastName = current.AuthorUser.LastName,
					AuthorUserFirstName = current.AuthorUser.FirstName,
				})
				.OrderByDescending(current => current.IsFeatured)
				.ThenByDescending(current => current.InsertDateTime)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.Post> GetActiveByCultureLcid(int cultureLcid)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CultureLcid == cultureLcid)

				.Where(current => current.Category.IsActive)
				.Where(current => current.Category.IsDeleted == false)

				.Where(current => current.CreatorUser.IsActive)
				.Where(current => current.CreatorUser.IsDeleted == false)
				.Where(current => current.CreatorUser.IsAccountTerminated == false)

				.OrderBy(current => current.Ordering)
				.ThenByDescending(current => current.IsFeatured)
				.ThenByDescending(current => current.UpdateDateTime)
				;

			return (varEntities);
		}

		public System.Linq.IOrderedQueryable<Models.Cms.Post> GetActiveByCultureLcidAndCategoryId(int cultureLcid, System.Guid categoryId)
		{
			var varEntities = Get()
				.Where(current => current.IsActive)
				.Where(current => current.IsDeleted == false)
				.Where(current => current.CategoryId == categoryId)
				.Where(current => current.CultureLcid == cultureLcid)

				.Where(current => current.Category.IsActive)
				.Where(current => current.Category.IsDeleted == false)

				.Where(current => current.CreatorUser.IsActive)
				.Where(current => current.CreatorUser.IsDeleted == false)
				.Where(current => current.CreatorUser.IsAccountTerminated == false)

				.OrderBy(current => current.Ordering)
				.ThenByDescending(current => current.IsFeatured)
				.ThenByDescending(current => current.UpdateDateTime)
				;

			return (varEntities);
		}
	}
}
