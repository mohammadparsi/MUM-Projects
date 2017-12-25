namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.1.2
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public interface IUnitOfWork
	{
		ITagRepository TagRepository { get; }
		//IMenuRepository MenuRepository { get; }
		IPageRepository PageRepository { get; }
		IPostRepository PostRepository { get; }
		//IPhotoRepository PhotoRepository { get; }
		ILayoutRepository LayoutRepository { get; }
		//IMenuItemRepository MenuItemRepository { get; }
		//IPhotoAlbumRepository PhotoAlbumRepository { get; }
		IPostCategoryRepository PostCategoryRepository { get; }
		ISubSystemSettingsRepository SubSystemSettingsRepository { get; }
	}
}
