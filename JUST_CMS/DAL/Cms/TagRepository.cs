using System.Linq;
using System.Data.Entity;

namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1392/12/04
	/// 
	/// </summary>
	public class TagRepository : Repository<Models.Cms.Tag>, ITagRepository
	{
		public TagRepository(Models.DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}

		public Models.Cms.Tag GetByCultureLcidAndName(int cultureLcid, string name)
		{
			Models.Cms.Tag oTag =
				Get()
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (oTag);
		}

		public Models.Cms.Tag GetByCultureLcidAndNameExceptId
			(int cultureLcid, string name, System.Guid id)
		{
			Models.Cms.Tag oTag =
				Get()
				.Where(current => current.Id != id)
				.Where(current => current.Name == name)
				.Where(current => current.CultureLcid == cultureLcid)
				.FirstOrDefault();

			return (oTag);
		}

		public IOrderedQueryable<ViewModels.Areas.Cms.Tag.IndexViewModel>
			GetViewModelByCultureLcid(int cultureLcid)
		{
			var varEntities =
			Get()
			.Where(current => current.CultureLcid == cultureLcid)
			.Select(current => new ViewModels.Areas.Cms.Tag.IndexViewModel
			{
				Id = current.Id,
				Name = current.Name,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				IsVerified = current.IsVerified,
				PageCount = current.Pages.Count(),
				PostCount = current.Posts.Count(),
				//PhotoCount = current.Photos.Count(),
				UpdateDateTime = current.UpdateDateTime
			})
			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)
			;

			return (varEntities);
		}
	}
}
