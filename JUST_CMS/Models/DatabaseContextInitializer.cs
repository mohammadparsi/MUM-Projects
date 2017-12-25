﻿namespace Models
{
	/// <summary>
	/// Version: 1.1.1
	/// Update Date: 1393/04/27
	/// 
	/// </summary>
	internal class DatabaseContextInitializer :
		System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
	{
		public DatabaseContextInitializer()
		{
		}

		protected override void Seed(DatabaseContext databaseContext)
		{
			try
			{
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_ProjectAreas_Name ON ProjectAreas (Name)");
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_ProjectControllers_ProjectAreaId_Name ON ProjectControllers (ProjectAreaId, Name)");
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_ProjectActions_ProjectControllerId_Name ON ProjectActions (ProjectControllerId, Name)");
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_Genders_CultureLcid_Code ON Genders (CultureLcid, Code)");
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_Roles_CultureLcid_Code ON Roles (CultureLcid, Code)");

				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_Roles_CultureLcid_Name ON Roles (CultureLcid, Name)");
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_AccessTypes_CultureLcid_Code ON AccessTypes (CultureLcid, Code)");

				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_AccessTypes_CultureLcid_Name ON AccessTypes (CultureLcid, Name)");
				// **************************************************

				// **************************************************
				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_Users_EmailAddress ON Users (EmailAddress)");

				databaseContext.Database
					.ExecuteSqlCommand
						("CREATE UNIQUE INDEX IX_Users_CellPhoneNumber ON Users (CellPhoneNumber)");
				// **************************************************
				// **************************************************
				// **************************************************

				// مشکل دارد SQL Server Compact Edition روی Schema سه دستور ذيل به خاطر

				// **************************************************
				// **************************************************
				// **************************************************
				//databaseContext.Database
				//	.ExecuteSqlCommand
				//		("CREATE UNIQUE INDEX IX_Cms_Tags_CultureLcid_Name ON Cms.Tags (CultureLcid, Name)");
				// **************************************************

				// **************************************************
				//databaseContext.Database
				//	.ExecuteSqlCommand
				//		("CREATE UNIQUE INDEX IX_Cms_Pages_CultureLcid_Name ON Cms.Pages (CultureLcid, Name)");
				// **************************************************

				// **************************************************
				//databaseContext.Database
				//	.ExecuteSqlCommand
				//		("CREATE UNIQUE INDEX IX_Cms_Layouts_CultureLcid_Name ON Cms.Layouts (CultureLcid, Name)");
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				// 1065
				Culture oPersianCulture =
					new Culture(new System.Globalization.CultureInfo("fa-IR"));

				oPersianCulture.IsActive = true;
				oPersianCulture.Ordering = 10000;
				oPersianCulture.DisplayName = "فارسی";

				databaseContext.Cultures.Add(oPersianCulture);

				// 1033
				Culture oEnglishCulture =
					new Culture(new System.Globalization.CultureInfo("en-US"));

				oEnglishCulture.IsActive = true;
				oEnglishCulture.Ordering = 10001;
				oEnglishCulture.DisplayName = "English";

				databaseContext.Cultures.Add(oEnglishCulture);
				// **************************************************
				// **************************************************
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				// **************************************************
				// **************************************************
				Gender oPersianFemale = new Gender();

				oPersianFemale.Name1 = "خانم";
				oPersianFemale.Name2 = "خانم";
				oPersianFemale.Name3 = "زن";
				oPersianFemale.Name4 = "مونث";
				oPersianFemale.IsActive = true;
				oPersianFemale.Ordering = 10000;
				oPersianFemale.MasterDataCode = 11001;
				oPersianFemale.Code = (int)Enums.Genders.Female;
				oPersianFemale.CultureLcid = oPersianCulture.Lcid;

				oPersianFemale.Id =
					new System.Guid("8ebd13a8-4cc7-4b17-a3c4-a2fead497425");

				databaseContext.Genders.Add(oPersianFemale);
				// **************************************************

				// **************************************************
				Gender oEnglishFemale = new Gender();

				oEnglishFemale.Name1 = "Female";
				oEnglishFemale.Name2 = "Ms.";
				oEnglishFemale.Name3 = "";
				oEnglishFemale.Name4 = "";
				oEnglishFemale.IsActive = true;
				oEnglishFemale.Ordering = 10000;
				oEnglishFemale.MasterDataCode = 11001;
				oEnglishFemale.Code = (int)Enums.Genders.Female;
				oEnglishFemale.CultureLcid = oEnglishCulture.Lcid;

				oEnglishFemale.Id =
					new System.Guid("4deb112f-8d5a-45d8-9267-83298fa794cc");

				databaseContext.Genders.Add(oEnglishFemale);
				// **************************************************

				// **************************************************
				Gender oPersianMale = new Gender();

				oPersianMale.Name1 = "آقا";
				oPersianMale.Name2 = "آقای";
				oPersianMale.Name3 = "مرد";
				oPersianMale.Name4 = "مذکر";
				oPersianMale.IsActive = true;
				oPersianMale.Ordering = 10001;
				oPersianMale.MasterDataCode = 11002;
				oPersianMale.Code = (int)Enums.Genders.Male;
				oPersianMale.CultureLcid = oPersianCulture.Lcid;

				oPersianMale.Id =
					new System.Guid("ddd75368-1c65-43f8-b8c9-312fa44e8dd9");

				databaseContext.Genders.Add(oPersianMale);
				// **************************************************

				// **************************************************
				Gender oEnglishMale = new Gender();

				oEnglishMale.Name1 = "Male";
				oEnglishMale.Name2 = "Mr.";
				oEnglishMale.Name3 = "";
				oEnglishMale.Name4 = "";
				oEnglishMale.IsActive = true;
				oEnglishMale.Ordering = 10001;
				oEnglishMale.MasterDataCode = 11002;
				oEnglishMale.Code = (int)Enums.Genders.Male;
				oEnglishMale.CultureLcid = oEnglishCulture.Lcid;

				oEnglishMale.Id =
					new System.Guid("63f92dfa-e6c7-469f-b55d-c5e9e145d80a");

				databaseContext.Genders.Add(oEnglishMale);
				// **************************************************
				// **************************************************
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				// **************************************************
				// **************************************************
				Role oPersianProgrammerRole = new Role();

				oPersianProgrammerRole.IsActive = true;
				oPersianProgrammerRole.Name = "برنامه نويس";
				oPersianProgrammerRole.CultureLcid = oPersianCulture.Lcid;
				oPersianProgrammerRole.Code = (int)Enums.Roles.Programmer;

				oPersianProgrammerRole.Id =
					new System.Guid("731458e6-1786-446d-b9d1-eb339550b569");

				databaseContext.Roles.Add(oPersianProgrammerRole);
				// **************************************************

				// **************************************************
				Role oEnglishProgrammerRole = new Role();

				oEnglishProgrammerRole.IsActive = true;
				oEnglishProgrammerRole.Name = "Programmer";
				oEnglishProgrammerRole.CultureLcid = oEnglishCulture.Lcid;
				oEnglishProgrammerRole.Code = (int)Enums.Roles.Programmer;

				oEnglishProgrammerRole.Id =
					new System.Guid("d4b9a2b7-19f2-42b7-bf67-13fa42f5c464");

				databaseContext.Roles.Add(oEnglishProgrammerRole);
				// **************************************************

				// **************************************************
				Role oPersianOwnerRole = new Role();

				oPersianOwnerRole.IsActive = true;
				oPersianOwnerRole.Name = "مالک پايگاه";
				oPersianOwnerRole.Code = (int)Enums.Roles.Owner;
				oPersianOwnerRole.CultureLcid = oPersianCulture.Lcid;

				oPersianOwnerRole.Id =
					new System.Guid("1feb6825-c78c-4c04-b1a1-c668ca1925ed");

				databaseContext.Roles.Add(oPersianOwnerRole);
				// **************************************************

				// **************************************************
				Role oEnglishOwnerRole = new Role();

				oEnglishOwnerRole.IsActive = true;
				oEnglishOwnerRole.Name = "Owner";
				oEnglishOwnerRole.Code = (int)Enums.Roles.Owner;
				oEnglishOwnerRole.CultureLcid = oEnglishCulture.Lcid;

				oEnglishOwnerRole.Id =
					new System.Guid("2f37183c-c72f-48aa-a941-4d91e30093bc");

				databaseContext.Roles.Add(oEnglishOwnerRole);
				// **************************************************

				// **************************************************
				Role oPersianAdministratorRole = new Role();

				oPersianAdministratorRole.IsActive = true;
				oPersianAdministratorRole.Name = "مدير پايگاه";
				oPersianAdministratorRole.CultureLcid = oPersianCulture.Lcid;
				oPersianAdministratorRole.Code = (int)Enums.Roles.Administrator;

				oPersianAdministratorRole.Id =
					new System.Guid("f92831cb-ccc8-4fc0-a76f-fe925b7625a6");

				databaseContext.Roles.Add(oPersianAdministratorRole);
				// **************************************************

				// **************************************************
				Role oEnglishAdministratorRole = new Role();

				oEnglishAdministratorRole.IsActive = true;
				oEnglishAdministratorRole.Name = "Administrator";
				oEnglishAdministratorRole.CultureLcid = oEnglishCulture.Lcid;
				oEnglishAdministratorRole.Code = (int)Enums.Roles.Administrator;

				oEnglishAdministratorRole.Id =
					new System.Guid("a41789fe-17df-4b93-860d-a63d4b353052");

				databaseContext.Roles.Add(oEnglishAdministratorRole);
				// **************************************************

				// **************************************************
				Role oPersianSupervisorRole = new Role();

				oPersianSupervisorRole.IsActive = true;
				oPersianSupervisorRole.Name = "مسوول پايگاه";
				oPersianSupervisorRole.CultureLcid = oPersianCulture.Lcid;
				oPersianSupervisorRole.Code = (int)Enums.Roles.Supervisor;

				oPersianSupervisorRole.Id =
					new System.Guid("b4ae90f9-844a-42ad-b2ac-86c3c3603a3a");

				databaseContext.Roles.Add(oPersianSupervisorRole);
				// **************************************************

				// **************************************************
				Role oEnglishSupervisorRole = new Role();

				oEnglishSupervisorRole.IsActive = true;
				oEnglishSupervisorRole.Name = "Supervisor";
				oEnglishSupervisorRole.CultureLcid = oEnglishCulture.Lcid;
				oEnglishSupervisorRole.Code = (int)Enums.Roles.Supervisor;

				oEnglishSupervisorRole.Id =
					new System.Guid("879c1fa0-185c-4505-bded-5edd7bfb68c2");

				databaseContext.Roles.Add(oEnglishSupervisorRole);
				// **************************************************

				// **************************************************
				Role oPersianUserRole = new Role();

				oPersianUserRole.IsActive = true;
				oPersianUserRole.Name = "کاربر";
				oPersianUserRole.Code = (int)Enums.Roles.User;
				oPersianUserRole.CultureLcid = oPersianCulture.Lcid;

				oPersianUserRole.Id =
					new System.Guid("7cd68754-5f1b-47a9-b0af-bdbd5056a9c3");

				databaseContext.Roles.Add(oPersianUserRole);
				// **************************************************

				// **************************************************
				Role oEnglishUserRole = new Role();

				oEnglishUserRole.IsActive = true;
				oEnglishUserRole.Name = "User";
				oEnglishUserRole.Code = (int)Enums.Roles.User;
				oEnglishUserRole.CultureLcid = oEnglishCulture.Lcid;

				oEnglishUserRole.Id =
					new System.Guid("a5706e93-0410-419d-8435-315780485ff2");

				databaseContext.Roles.Add(oEnglishUserRole);
				// **************************************************
				// **************************************************
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				// **************************************************
				// **************************************************
				AccessType oPersianPublicAccessType = new AccessType();

				oPersianPublicAccessType.IsActive = true;
				oPersianPublicAccessType.Name = "همه";
				oPersianPublicAccessType.Code = (int)Enums.AccessTypes.Public;
				oPersianPublicAccessType.CultureLcid = oPersianCulture.Lcid;

				oPersianPublicAccessType.Id =
					new System.Guid("06370acb-35e4-457c-be7d-a11d681c2d85");

				databaseContext.AccessTypes.Add(oPersianPublicAccessType);
				// **************************************************

				// **************************************************
				AccessType oEnglishPublicAccessType = new AccessType();

				oEnglishPublicAccessType.IsActive = true;
				oEnglishPublicAccessType.Name = "Public";
				oEnglishPublicAccessType.Code = (int)Enums.AccessTypes.Public;
				oEnglishPublicAccessType.CultureLcid = oEnglishCulture.Lcid;

				oEnglishPublicAccessType.Id =
					new System.Guid("70eeb31a-f8ec-499a-8a33-2468afe99ceb");

				databaseContext.AccessTypes.Add(oEnglishPublicAccessType);
				// **************************************************

				// **************************************************
				AccessType oPersianRegisteredAccessType = new AccessType();

				oPersianRegisteredAccessType.IsActive = true;
				oPersianRegisteredAccessType.Name = "کاربرانی که وارد حساب کاربری خود شده‌اند";
				oPersianRegisteredAccessType.Code = (int)Enums.AccessTypes.Registered;
				oPersianRegisteredAccessType.CultureLcid = oPersianCulture.Lcid;

				oPersianRegisteredAccessType.Id =
					new System.Guid("f380804e-9f02-4448-b086-18d04ab0d658");

				databaseContext.AccessTypes.Add(oPersianRegisteredAccessType);
				// **************************************************

				// **************************************************
				AccessType oEnglishRegisteredAccessType = new AccessType();

				oEnglishRegisteredAccessType.IsActive = true;
				oEnglishRegisteredAccessType.Name = "Registered";
				oEnglishRegisteredAccessType.Code = (int)Enums.AccessTypes.Registered;
				oEnglishRegisteredAccessType.CultureLcid = oEnglishCulture.Lcid;

				oEnglishRegisteredAccessType.Id =
					new System.Guid("0578a77b-c5ec-4817-acd6-f7d31bbbf9e9");

				databaseContext.AccessTypes.Add(oEnglishRegisteredAccessType);
				// **************************************************

				// **************************************************
				AccessType oPersianSpecialAccessType = new AccessType();

				oPersianSpecialAccessType.IsActive = true;
				oPersianSpecialAccessType.Name = "کاربرانی که دارای سطح دسترسی خاص هستند";
				oPersianSpecialAccessType.Code = (int)Enums.AccessTypes.Special;
				oPersianSpecialAccessType.CultureLcid = oPersianCulture.Lcid;

				oPersianSpecialAccessType.Id =
					new System.Guid("ef1e8064-a093-4be6-9042-6089f272d42a");

				databaseContext.AccessTypes.Add(oPersianSpecialAccessType);
				// **************************************************

				// **************************************************
				AccessType oEnglishSpecialAccessType = new AccessType();

				oEnglishSpecialAccessType.IsActive = true;
				oEnglishSpecialAccessType.Name = "Special";
				oEnglishSpecialAccessType.Code = (int)Enums.AccessTypes.Special;
				oEnglishSpecialAccessType.CultureLcid = oEnglishCulture.Lcid;

				oEnglishSpecialAccessType.Id =
					new System.Guid("47f9ebae-9c3e-490d-9931-ee3edbdee3fc");

				databaseContext.AccessTypes.Add(oEnglishSpecialAccessType);
				// **************************************************
				// **************************************************
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				// **************************************************
				// **************************************************
				User oPersianProgrammerUser = new User();

				oPersianProgrammerUser.IsSystem = true;
				oPersianProgrammerUser.IsActive = true;
				oPersianProgrammerUser.IsDirectContactable = true;

				oPersianProgrammerUser.LastName = "تصديقی";
				oPersianProgrammerUser.FirstName = "داريوش";
				oPersianProgrammerUser.JobTitle = "مسوول فنی پايگاه";

				oPersianProgrammerUser.Password =
					Dtx.Security.Hashing.GetSha1("1234512345");

				oPersianProgrammerUser.IsEmailAddressVerified = true;
				oPersianProgrammerUser.IsCellPhoneNumberVerified = true;

				oPersianProgrammerUser.NationalCode = "0055894216";
				oPersianProgrammerUser.CellPhoneNumber = "00989121087461";
				oPersianProgrammerUser.EmailAddress = "T@GMail.com".ToLower();

				oPersianProgrammerUser.Gender = oPersianMale;
				oPersianProgrammerUser.Role = oPersianProgrammerRole;
				oPersianProgrammerUser.CultureLcid = oPersianCulture.Lcid;

				oPersianProgrammerUser.Id =
					new System.Guid("823669af-58d8-47f6-b567-3c43e1c37820");

				databaseContext.Users.Add(oPersianProgrammerUser);
				// **************************************************
				// **************************************************
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				// **************************************************
				// **************************************************
				Cms.Layout oPersianRssPostLayout = new Cms.Layout();

				oPersianRssPostLayout.Name = "rss_post";
				oPersianRssPostLayout.Title = "قالب مطالب RSS";
				oPersianRssPostLayout.Description = null;

				oPersianRssPostLayout.IsActive = true;
				oPersianRssPostLayout.IsSystem = true;
				oPersianRssPostLayout.CultureLcid = oPersianCulture.Lcid;

				databaseContext.Layouts.Add(oPersianRssPostLayout);
				// **************************************************

				// **************************************************
				Cms.Layout oEnglishRssPostLayout = new Cms.Layout();

				oEnglishRssPostLayout.Name = "rss_post";
				oEnglishRssPostLayout.Title = "Rss Post Layout";
				oEnglishRssPostLayout.Description = null;

				oEnglishRssPostLayout.IsActive = true;
				oEnglishRssPostLayout.IsSystem = true;
				oEnglishRssPostLayout.CultureLcid = oEnglishCulture.Lcid;

				databaseContext.Layouts.Add(oEnglishRssPostLayout);
				// **************************************************

				// **************************************************
				Cms.Layout oPersianHomeLayout = new Cms.Layout();

				oPersianHomeLayout.Name = "home";
				oPersianHomeLayout.Title = "قالب صفحه اول";
				oPersianHomeLayout.Description = null;

				oPersianHomeLayout.IsActive = true;
				oPersianHomeLayout.IsSystem = true;
				oPersianHomeLayout.CultureLcid = oPersianCulture.Lcid;

				databaseContext.Layouts.Add(oPersianHomeLayout);
				// **************************************************

				// **************************************************
				Cms.Layout oEnglishHomeLayout = new Cms.Layout();

				oEnglishHomeLayout.Name = "home";
				oEnglishHomeLayout.Title = "Home Layout";
				oEnglishHomeLayout.Description = null;

				oEnglishHomeLayout.IsActive = true;
				oEnglishHomeLayout.IsSystem = true;
				oEnglishHomeLayout.CultureLcid = oEnglishCulture.Lcid;

				databaseContext.Layouts.Add(oEnglishHomeLayout);
				// **************************************************

				// **************************************************
				Cms.Layout oPersianDefaultLayout = new Cms.Layout();

				oPersianDefaultLayout.Name = "default";
				oPersianDefaultLayout.Title = "قالب پيش‌فرض";
				oPersianDefaultLayout.Description = null;

				oPersianDefaultLayout.IsActive = true;
				oPersianDefaultLayout.IsSystem = true;
				oPersianDefaultLayout.CultureLcid = oPersianCulture.Lcid;

				databaseContext.Layouts.Add(oPersianDefaultLayout);
				// **************************************************

				// **************************************************
				Cms.Layout oEnglishDefaultLayout = new Cms.Layout();

				oEnglishDefaultLayout.Name = "default";
				oEnglishDefaultLayout.Title = "Default Layout";
				oEnglishDefaultLayout.Description = null;

				oEnglishDefaultLayout.IsActive = true;
				oEnglishDefaultLayout.IsSystem = true;
				oEnglishDefaultLayout.CultureLcid = oEnglishCulture.Lcid;

				databaseContext.Layouts.Add(oEnglishDefaultLayout);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();
				// **************************************************
				// **************************************************
				// **************************************************

				// **************************************************
				// **************************************************
				// **************************************************
				Cms.PostCategory oPersianPostCategoryNews = new Cms.PostCategory();

				oPersianPostCategoryNews.IsActive = true;
				oPersianPostCategoryNews.Name = "اخبار";
				oPersianPostCategoryNews.MasterDataCode = 1;
				oPersianPostCategoryNews.CultureLcid = oPersianCulture.Lcid;

				oPersianPostCategoryNews.RssItemCount = 10;
				oPersianPostCategoryNews.RssChannelTitle = "عنوان";
				oPersianPostCategoryNews.RssChannelLink = "http://www.IranianExperts.com";
				oPersianPostCategoryNews.RssRootCreatorUrl = "http://www.IranianExperts.com/Rss/1";
				oPersianPostCategoryNews.RssChannelDescription = "توضيحات";

				oPersianPostCategoryNews.RssImageTitle = "عنوان عکس";
				oPersianPostCategoryNews.RssImageLink = "http://www.IranianExperts.com";
				oPersianPostCategoryNews.RssImageUrl = "http://www.IranianExperts.com/Images/Rss.png";

				databaseContext.PostCategories.Add(oPersianPostCategoryNews);
				// **************************************************

				// **************************************************
				Cms.PostCategory oEnglishPostCategoryNews = new Cms.PostCategory();

				oEnglishPostCategoryNews.IsActive = true;
				oEnglishPostCategoryNews.Name = "News";
				oEnglishPostCategoryNews.MasterDataCode = 1000;
				oEnglishPostCategoryNews.CultureLcid = oEnglishCulture.Lcid;

				oEnglishPostCategoryNews.RssChannelTitle = "Title";
				oEnglishPostCategoryNews.RssChannelLink = "http://www.IranianExperts.com";
				oEnglishPostCategoryNews.RssChannelDescription = "Description";

				oEnglishPostCategoryNews.RssImageTitle = "Image Title";
				oEnglishPostCategoryNews.RssImageLink = "http://www.IranianExperts.com";
				oEnglishPostCategoryNews.RssRootCreatorUrl = "http://www.IranianExperts.com/Rss/1";
				oEnglishPostCategoryNews.RssImageUrl = "http://www.IranianExperts.com/Images/Rss.png";

				databaseContext.PostCategories.Add(oEnglishPostCategoryNews);
				// **************************************************
				// **************************************************
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				ProjectArea oAreaRoot = new ProjectArea();

				oAreaRoot.IsActive = true;
				oAreaRoot.Name = string.Empty;
				oAreaRoot.DisplayName = "زيرسيستم اصلی";
				oAreaRoot.IsVisibleJustForProgrammer = false;

				databaseContext.ProjectAreas.Add(oAreaRoot);
				// **************************************************

				// **************************************************
				ProjectArea oAreaAdministrator = new ProjectArea();

				oAreaAdministrator.IsActive = true;
				oAreaAdministrator.Name = "Administrator";
				oAreaAdministrator.DisplayName = "زيرسيستم مديريتی";
				oAreaAdministrator.IsVisibleJustForProgrammer = false;

				databaseContext.ProjectAreas.Add(oAreaAdministrator);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				ProjectAction oProjectAction = null;

				// **************************************************
				ProjectController oControllerHome = new ProjectController();

				oControllerHome.Name = "Home";
				oControllerHome.DisplayName = "بخش اصلی";

				oControllerHome.ProjectArea = oAreaRoot;

				oControllerHome.IsActive = true;
				oControllerHome.IsVisibleJustForProgrammer = false;

				databaseContext.ProjectControllers.Add(oControllerHome);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				oProjectAction = new ProjectAction();

				oProjectAction.Name = "Index";
				oProjectAction.DisplayName = "صفحه نخست";
				oProjectAction.ProjectController = oControllerHome;

				oProjectAction.IsActive = true;
				oProjectAction.IsVisibleJustForProgrammer = false;
				oProjectAction.AccessTypeId = oPersianPublicAccessType.Id;

				databaseContext.ProjectActions.Add(oProjectAction);
				// **************************************************

				// **************************************************
				oProjectAction = new ProjectAction();

				oProjectAction.Name = "ChangeCulture";
				oProjectAction.DisplayName = "تغييرزبان‌پايگاه";
				oProjectAction.ProjectController = oControllerHome;

				oProjectAction.IsActive = true;
				oProjectAction.IsVisibleJustForProgrammer = false;
				oProjectAction.AccessTypeId = oPersianPublicAccessType.Id;

				databaseContext.ProjectActions.Add(oProjectAction);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				ProjectController oControllerError = new ProjectController();

				oControllerError.Name = "Error";
				oControllerError.DisplayName = "بخش خطا";

				oControllerError.ProjectArea = oAreaRoot;

				oControllerError.IsActive = true;
				oControllerError.IsVisibleJustForProgrammer = false;

				databaseContext.ProjectControllers.Add(oControllerError);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				oProjectAction = new ProjectAction();

				oProjectAction.Name = "Display";
				oProjectAction.DisplayName = "نمايش خطا";
				oProjectAction.ProjectController = oControllerError;

				oProjectAction.IsActive = true;
				oProjectAction.IsVisibleJustForProgrammer = false;
				oProjectAction.AccessTypeId = oPersianPublicAccessType.Id;

				databaseContext.ProjectActions.Add(oProjectAction);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				ProjectController oControllerAccount = new ProjectController();

				oControllerAccount.ProjectArea = oAreaRoot;
				oControllerAccount.Name = "Account";
				oControllerAccount.DisplayName = "تنظيمات کاربری";

				oControllerAccount.IsActive = true;
				oControllerAccount.IsVisibleJustForProgrammer = false;

				databaseContext.ProjectControllers.Add(oControllerAccount);
				// **************************************************

				// برای تست
				databaseContext.SaveChanges();

				// **************************************************
				oProjectAction = new ProjectAction();

				oProjectAction.Name = "Login";
				oProjectAction.DisplayName = "ورود";
				oProjectAction.ProjectController = oControllerAccount;

				oProjectAction.IsActive = true;
				oProjectAction.IsVisibleJustForProgrammer = false;
				oProjectAction.AccessTypeId = oPersianPublicAccessType.Id;

				databaseContext.ProjectActions.Add(oProjectAction);
				// **************************************************

				// **************************************************
				oProjectAction = new ProjectAction();

				oProjectAction.Name = "Logout";
				oProjectAction.DisplayName = "خروج از پايگاه";
				oProjectAction.ProjectController = oControllerAccount;

				oProjectAction.IsActive = true;
				oProjectAction.IsVisibleJustForProgrammer = false;
				oProjectAction.AccessTypeId = oPersianPublicAccessType.Id;

				databaseContext.ProjectActions.Add(oProjectAction);
				// **************************************************

				// **************************************************
				oProjectAction = new ProjectAction();

				oProjectAction.Name = "Register";
				oProjectAction.DisplayName = "ثبت نام";
				oProjectAction.ProjectController = oControllerAccount;

				oProjectAction.IsActive = true;
				oProjectAction.IsVisibleJustForProgrammer = false;
				oProjectAction.AccessTypeId = oPersianPublicAccessType.Id;

				databaseContext.ProjectActions.Add(oProjectAction);
				// **************************************************

				//// **************************************************
				//// **************************************************
				//// **************************************************
				//Country oCountry =
				//	new Country();

				//oCountry.Name = "ايران";
				//oCountry.Ordering = 10000;
				//oCountry.MasterDataCode = 10000;

				//oCountry.CultureLcid = oPersianCulture.Lcid;
				//oCountry.SetInsertDateTime(oPersianProgrammerUser.Id, System.DateTime.Now);
				//oCountry.SetIsActive(true, oPersianProgrammerUser.Id, System.DateTime.Now);

				//databaseContext.Countries.Add(oCountry);
				//// **************************************************

				//// **************************************************
				//State oState =
				//	new State();

				//oState.Name = "تهران";
				//oState.Ordering = 10000;
				//oState.MasterDataCode = 10000;
				//oState.CountryId = oCountry.Id;

				//oState.SetInsertDateTime(oPersianProgrammerUser.Id, System.DateTime.Now);
				//oState.SetIsActive(true, oPersianProgrammerUser.Id, System.DateTime.Now);

				//databaseContext.States.Add(oState);
				//// **************************************************

				//// **************************************************
				//City oCity =
				//	new City();

				//oCity.Name = "تهران";
				//oCity.Ordering = 10000;
				//oCity.MasterDataCode = 10000;
				//oCity.StateId = oState.Id;

				//oCity.SetInsertDateTime(oPersianProgrammerUser.Id, System.DateTime.Now);
				//oCity.SetIsActive(true, oPersianProgrammerUser.Id, System.DateTime.Now);

				//databaseContext.Cities.Add(oCity);
				//// **************************************************

				//// برای تست
				//databaseContext.SaveChanges();
				//// **************************************************
				//// **************************************************
				//// **************************************************
			}
			catch (System.Exception ex)
			{
				Dtx.LogHandler.Report(GetType(), null, ex);
			}
		}

		private static void AddNewPage
			(DatabaseContext databaseContext,
			Culture culture,
			AccessType accessType,
			User user,
			Cms.Layout layout,
			string name,
			string title)
		{
			Cms.Page oPage = new Cms.Page();

			oPage.Name = name;
			oPage.Title = title;

			oPage.LayoutId = layout.Id;
			oPage.AuthorUserId = user.Id;
			oPage.VerifierUserId = user.Id;
			oPage.CultureLcid = culture.Lcid;
			oPage.AccessTypeId = accessType.Id;

			oPage.IsActive = true;
			oPage.IsSystem = true;
			oPage.DoesSearchEnginesIndexIt = true;
			oPage.DoesSearchEnginesFollowIt = true;

			oPage.VerifyDateTime = System.DateTime.Now;

			databaseContext.Pages.Add(oPage);
		}
	}
}
