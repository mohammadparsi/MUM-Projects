namespace DAL.Cms
{
	/// <summary>
	/// Version: 1.1.2
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public class UnitOfWork : System.Object, IUnitOfWork
	{
		public UnitOfWork(Models.DatabaseContext databaseContext)
		{
			if (databaseContext == null)
			{
				throw (new System.ArgumentNullException("databaseContext"));
			}

			DatabaseContext = databaseContext;
		}

		protected Models.DatabaseContext DatabaseContext { get; set; }

		//private IRepository _Repository;
		//public IRepository Repository
		//{
		//	get
		//	{
		//		if (_Repository == null)
		//		{
		//			_Repository =
		//				new Repository(DatabaseContext);
		//		}
		//		return (_Repository);
		//	}
		//}

		private ITagRepository _tagRepository;
		public ITagRepository TagRepository
		{
			get
			{
				if (_tagRepository == null)
				{
					_tagRepository =
						new TagRepository(DatabaseContext);
				}
				return (_tagRepository);
			}
		}

		//private IMenuRepository _menuRepository;
		//public IMenuRepository MenuRepository
		//{
		//	get
		//	{
		//		if (_menuRepository == null)
		//		{
		//			_menuRepository =
		//				new MenuRepository(DatabaseContext);
		//		}
		//		return (_menuRepository);
		//	}
		//}

		private IPageRepository _pageRepository;
		public IPageRepository PageRepository
		{
			get
			{
				if (_pageRepository == null)
				{
					_pageRepository =
						new PageRepository(DatabaseContext);
				}
				return (_pageRepository);
			}
		}

		private IPostRepository _postRepository;
		public IPostRepository PostRepository
		{
			get
			{
				if (_postRepository == null)
				{
					_postRepository =
						new PostRepository(DatabaseContext);
				}
				return (_postRepository);
			}
		}

		//private IPhotoRepository _photoRepository;
		//public IPhotoRepository PhotoRepository
		//{
		//	get
		//	{
		//		if (_photoRepository == null)
		//		{
		//			_photoRepository =
		//				new PhotoRepository(DatabaseContext);
		//		}
		//		return (_photoRepository);
		//	}
		//}

		private ILayoutRepository _layoutRepository;
		public ILayoutRepository LayoutRepository
		{
			get
			{
				if (_layoutRepository == null)
				{
					_layoutRepository =
						new LayoutRepository(DatabaseContext);
				}
				return (_layoutRepository);
			}
		}

		//private IMenuItemRepository _menuItemRepository;
		//public IMenuItemRepository MenuItemRepository
		//{
		//	get
		//	{
		//		if (_menuItemRepository == null)
		//		{
		//			_menuItemRepository =
		//				new MenuItemRepository(DatabaseContext);
		//		}
		//		return (_menuItemRepository);
		//	}
		//}

		//private IPhotoAlbumRepository _photoAlbumRepository;
		//public IPhotoAlbumRepository PhotoAlbumRepository
		//{
		//	get
		//	{
		//		if (_photoAlbumRepository == null)
		//		{
		//			_photoAlbumRepository =
		//				new PhotoAlbumRepository(DatabaseContext);
		//		}
		//		return (_photoAlbumRepository);
		//	}
		//}

		private IPostCategoryRepository _postCategoryRepository;
		public IPostCategoryRepository PostCategoryRepository
		{
			get
			{
				if (_postCategoryRepository == null)
				{
					_postCategoryRepository =
						new PostCategoryRepository(DatabaseContext);
				}
				return (_postCategoryRepository);
			}
		}

		private ISubSystemSettingsRepository _subSystemSettingsRepository;
		public ISubSystemSettingsRepository SubSystemSettingsRepository
		{
			get
			{
				if (_subSystemSettingsRepository == null)
				{
					_subSystemSettingsRepository =
						new SubSystemSettingsRepository(DatabaseContext);
				}
				return (_subSystemSettingsRepository);
			}
		}
	}
}
