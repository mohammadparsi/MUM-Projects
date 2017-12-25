namespace Models
{
	/// <summary>
	/// Version: 1.3.5
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public class DatabaseContext : System.Data.Entity.DbContext
	{
		static DatabaseContext()
		{
			// For The First Time
			System.Data.Entity.Database.SetInitializer(new DatabaseContextInitializer());
			// /For The First Time

			// For Migrations
			//System.Data.Entity.Database.SetInitializer(new DatabaseToLatestVersionMigrations());
			// /For Migrations
		}

		public DatabaseContext()
		{
		}

		public System.Data.Entity.DbSet<Models.Cms.PageContent> PageContents { get; set; }

		public System.Data.Entity.DbSet<Role> Roles { get; set; }
		public System.Data.Entity.DbSet<User> Users { get; set; }
		public System.Data.Entity.DbSet<City> Cities { get; set; }
		public System.Data.Entity.DbSet<Group> Groups { get; set; }
		public System.Data.Entity.DbSet<State> States { get; set; }
		public System.Data.Entity.DbSet<Gender> Genders { get; set; }
		public System.Data.Entity.DbSet<Culture> Cultures { get; set; }
		public System.Data.Entity.DbSet<Country> Countries { get; set; }
		public System.Data.Entity.DbSet<AccessType> AccessTypes { get; set; }
		public System.Data.Entity.DbSet<HttpReferer> HttpReferers { get; set; }
		public System.Data.Entity.DbSet<ProjectArea> ProjectAreas { get; set; }
		public System.Data.Entity.DbSet<UserLoginLog> UserLoginLogs { get; set; }
		public System.Data.Entity.DbSet<ProjectAction> ProjectActions { get; set; }
		public System.Data.Entity.DbSet<ProjectController> ProjectControllers { get; set; }

		public System.Data.Entity.DbSet<MailSettings> MyMailSettings { get; set; }
		public System.Data.Entity.DbSet<ApplicationSettings> MyApplicationSettings { get; set; }
		public System.Data.Entity.DbSet<GlobalApplicationSettings> MyGlobalApplicationSettings { get; set; }

		public System.Data.Entity.DbSet<Cms.Tag> Tags { get; set; }
		public System.Data.Entity.DbSet<Cms.Page> Pages { get; set; }
		public System.Data.Entity.DbSet<Cms.Post> Posts { get; set; }
		public System.Data.Entity.DbSet<Cms.Layout> Layouts { get; set; }
		public System.Data.Entity.DbSet<Cms.PostCategory> PostCategories { get; set; }
		public System.Data.Entity.DbSet<Cms.SubSystemSettings> SubSystemSettings { get; set; }

		//public System.Data.Entity.DbSet<Cms.Menu> Menus { get; set; }
		//public System.Data.Entity.DbSet<Cms.Photo> Photos { get; set; }
		//public System.Data.Entity.DbSet<Cms.MenuItem> MenuItems { get; set; }
		//public System.Data.Entity.DbSet<Cms.PhotoAlbum> PhotoAlbums { get; set; }

		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new Cms.PageContent.Configuration());

			modelBuilder.Configurations.Add(new City.Configuration());
			modelBuilder.Configurations.Add(new Role.Configuration());
			modelBuilder.Configurations.Add(new User.Configuration());
			modelBuilder.Configurations.Add(new Group.Configuration());
			modelBuilder.Configurations.Add(new State.Configuration());
			modelBuilder.Configurations.Add(new Gender.Configuration());
			modelBuilder.Configurations.Add(new Country.Configuration());
			modelBuilder.Configurations.Add(new Culture.Configuration());
			modelBuilder.Configurations.Add(new AccessType.Configuration());
			modelBuilder.Configurations.Add(new HttpReferer.Configuration());
			modelBuilder.Configurations.Add(new ProjectArea.Configuration());
			modelBuilder.Configurations.Add(new UserLoginLog.Configuration());
			modelBuilder.Configurations.Add(new ProjectAction.Configuration());
			modelBuilder.Configurations.Add(new ProjectController.Configuration());

			modelBuilder.Configurations.Add(new MailSettings.Configuration());
			modelBuilder.Configurations.Add(new ApplicationSettings.Configuration());
			modelBuilder.Configurations.Add(new GlobalApplicationSettings.Configuration());

			modelBuilder.Configurations.Add(new Cms.Tag.Configuration());
			modelBuilder.Configurations.Add(new Cms.Page.Configuration());
			modelBuilder.Configurations.Add(new Cms.Post.Configuration());
			modelBuilder.Configurations.Add(new Cms.Layout.Configuration());
			modelBuilder.Configurations.Add(new Cms.PostCategory.Configuration());
			modelBuilder.Configurations.Add(new Cms.SubSystemSettings.Configuration());

			//modelBuilder.Configurations.Add(new Cms.Menu.Configuration());
			//modelBuilder.Configurations.Add(new Cms.Photo.Configuration());
			//modelBuilder.Configurations.Add(new Cms.MenuItem.Configuration());
			//modelBuilder.Configurations.Add(new Cms.PhotoAlbum.Configuration());
		}
	}
}
