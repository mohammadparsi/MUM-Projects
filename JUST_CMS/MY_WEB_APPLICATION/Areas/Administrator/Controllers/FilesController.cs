//using System.Linq;
//using System.Data.Entity;

//namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
//{
//	/// <summary>
//	/// Version: 1.0.1
//	/// Update Date: 1393/04/25
//	/// 
//	/// </summary>
//	[Infrastructure.ProjectControllerPermission
//		(isVisibleJustForProgrammer: false,
//		keyName: Resources.Strings.ControllersKeys.Administrator.Files)]
//	public partial class FilesController : Infrastructure.BaseControllerWithUnitOfWork
//	{
//		public FilesController()
//		{
//		}

//		//private ElFinder.Connector _connector;
//		//public ElFinder.Connector Connector
//		//{
//		//	get
//		//	{
//		//		if (_connector == null)
//		//		{
//		//			ElFinder.FileSystemDriver oFileSystemDriver =
//		//				new ElFinder.FileSystemDriver();

//		//			//System.IO.DirectoryInfo oThumbsStorage = 
//		//			//	new System.IO.DirectoryInfo(Server.MapPath("~/Files"));

//		//			ElFinder.Root oRoot = null;
//		//			string strPath = string.Empty;
//		//			string strRootRelativePath = string.Empty;
//		//			System.IO.DirectoryInfo oDirectoryInfo = null;

//		//			//strRootRelativePath = "~/Content";
//		//			strRootRelativePath = Session["RootRelativePath"].ToString();
//		//			strPath = Server.MapPath(strRootRelativePath);
//		//			oDirectoryInfo = new System.IO.DirectoryInfo(strPath);

//		//			oRoot = new ElFinder.Root(oDirectoryInfo);

//		//			//oRoot.Alias = "";
//		//			oRoot.IsLocked = false;
//		//			oRoot.IsReadOnly = false;
//		//			oRoot.IsShowOnly = false;
//		//			oRoot.MaxUploadSizeInMb = 4;
//		//			//oRoot.PicturesEditor
//		//			//oRoot.StartPath
//		//			//oRoot.ThumbnailsSize
//		//			//oRoot.ThumbnailsStorage
//		//			//DirectoryInfo thumbsStorage = new DirectoryInfo(Server.MapPath("~/Files"));
//		//			//oRoot.ThumbnailsUrl = "Thumbnails/";
//		//			oRoot.UploadOverwrite = false;
//		//			//oRoot.Url
//		//			//oRoot.VolumeId

//		//			oFileSystemDriver.AddRoot(oRoot);

//		//			//oFileSystemDriver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("~/Files")), "/Files/")
//		//			//{
//		//			//	Alias = "My documents",
//		//			//	StartPath = new DirectoryInfo(Server.MapPath("~/Files/новая папка")),
//		//			//	ThumbnailsStorage = oThumbsStorage,
//		//			//	MaxUploadSizeInMb = 2.2,
//		//			//	ThumbnailsUrl = "Thumbnails/"
//		//			//});

//		//			_connector = new ElFinder.Connector(oFileSystemDriver);
//		//		}

//		//		return (_connector);
//		//	}
//		//}

//		////public virtual System.Web.Mvc.ActionResult Display()
//		////{
//		////	Session["RootRelativePath"] = "~/Views/Shared/_Layouts";

//		////	return (View());
//		////}

//		//public virtual System.Web.Mvc.ActionResult Display(string rootRelativePath)
//		//{
//		//	if (string.IsNullOrEmpty(rootRelativePath))
//		//	{
//		//		rootRelativePath = "~/Views/Shared/_Layouts";
//		//	}
//		//	else
//		//	{
//		//		string strPath = Server.MapPath(rootRelativePath);

//		//		if(System.IO.Directory.Exists(strPath) == false)
//		//		{
//		//			rootRelativePath = "~/Views/Shared/_Layouts";
//		//		}
//		//	}

//		//	Session["RootRelativePath"] = rootRelativePath;

//		//	return (View());
//		//}

//		////public virtual System.Web.Mvc.ActionResult DisplaySpecificPath(string rootRelativePath)
//		////{
//		////	Session["RootRelativePath"] = rootRelativePath;

//		////	return (View(MVC.Administrator.Files.Views.Display));
//		////}

//		//public virtual System.Web.Mvc.ActionResult Index()
//		//{
//		//	return (Connector.Process(HttpContext.Request));
//		//}

//		//public virtual System.Web.Mvc.ActionResult SelectFile(string target)
//		//{
//		//	return (Json(Connector.GetFileByHash(target).FullName));
//		//}

//		//public virtual System.Web.Mvc.ActionResult Thumbs(string tmb)
//		//{
//		//	return (Connector.GetThumbnail(Request, Response, tmb));
//		//}
//	}
//}
