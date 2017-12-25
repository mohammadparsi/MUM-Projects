using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1393/02/14
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.LocalizedTemplates)]
	public partial class LocalizedTemplatesController : Infrastructure.BaseController
	{
		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Index)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.PartialViewResult Edit(string directoryName, string fileName)
		{
			//System.Threading.Thread.Sleep(5000);

			string strRootRelativePath =
				string.Format("~/App_Data/LocalizedTemplates/{0}", directoryName);

			string strRootRelativePathName =
				string.Format("{0}/{1}", strRootRelativePath, fileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			string strContent =
				Dtx.IO.File.Read(strPathName);

			ViewBag.FileName = fileName;
			ViewBag.Content = strContent;
			ViewBag.DirectoryName = directoryName;

			return (PartialView());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Save)]
		public virtual System.Web.Mvc.JsonResult Save(string directoryName, string fileName, string content)
		{
			//System.Threading.Thread.Sleep(5000);

			content = content.Replace("|||||", ":");

			string strRootRelativePath =
				string.Format("~/App_Data/LocalizedTemplates/{0}", directoryName);

			string strRootRelativePathName =
				string.Format("{0}/{1}", strRootRelativePath, fileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			Dtx.IO.File.Overwrite(strPathName, content);

			ViewBag.Content = content;

			return (null);
		}
	}
}
