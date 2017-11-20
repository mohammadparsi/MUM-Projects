using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.0.2
	/// Update Date: 1393/01/06
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.FileManager)]
	public partial class FileManagerController : Infrastructure.BaseController
	{
		public FileManagerController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.List)]
		public virtual System.Web.Mvc.ActionResult List(string rootRelativePath = "~")
		{
			// بسيار اهميت دارد model: توجه: نوشتن عبارت
			// در نظر می‌گيرد View در غير اين صورت آنرا به عنوان نام
			return (View(model: rootRelativePath));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Download)]
		public virtual System.Web.Mvc.FileResult Download(string rootRelativePathName)
		{
			string strPathName =
				Server.MapPath(rootRelativePathName);

			string strFileName =
				System.IO.Path.GetFileName(strPathName);

			return (File(strPathName,
				contentType: "googooli/magooli", fileDownloadName: strFileName));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.JsonResult Delete(string rootRelativePath, string myArray)
		{
			if (string.IsNullOrEmpty(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strResult =
					Dtx.Resources.FileManager.Error002;

				return (Json(new { MyMessage = strResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			if (string.IsNullOrEmpty(myArray))
			{
				// فايل و/يا پوشه‌ای برای حذف انتخاب نشده است
				string strResult =
					Dtx.Resources.FileManager.Error001;

				return (Json(new { MyMessage = strResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			string[] strEntitiesName = myArray.Split(':');

			if (strEntitiesName.Length == 0)
			{
				// فايل و/يا پوشه‌ای برای حذف انتخاب نشده است
				string strResult =
					Dtx.Resources.FileManager.Error001;

				return (Json(new { MyMessage = strResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			string strPath = Server.MapPath(rootRelativePath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strResult =
					Dtx.Resources.FileManager.Error002;

				return (Json(new { MyMessage = strResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			foreach (string strEntityName in strEntitiesName)
			{
				string strPathOrPathName =
					string.Format("{0}\\{1}", strPath, strEntityName);

				if (System.IO.Directory.Exists(strPathOrPathName))
				{
					try
					{
						System.IO.Directory.Delete(strPathOrPathName, recursive: true);
					}
					catch (System.Exception ex)
					{
						string strErrorMessage = ex.Message;
					}
				}
				else
				{
					if (System.IO.File.Exists(strPathOrPathName))
					{
						try
						{
							System.IO.File.Delete(strPathOrPathName);
						}
						catch (System.Exception ex)
						{
							string strErrorMessage = ex.Message;
						}
					}
				}
			}

			return (Json(new { MyMessage = "OK" },
				System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Rename)]
		public virtual System.Web.Mvc.JsonResult Rename
			(string rootRelativePath, string oldName, string newName)
		{
			if (string.IsNullOrEmpty(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strMessage =
					Dtx.Resources.FileManager.Error002;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			if (string.IsNullOrEmpty(oldName))
			{
				// نام فايل يا پوشه‌ای برای تغيير نام دادن، مشخص نشده است
				string strMessage =
					Dtx.Resources.FileManager.Error003;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			if (string.IsNullOrEmpty(newName))
			{
				// شما نام فايل يا پوشه جديد را وارد نکرده‌ايد
				string strMessage =
					Dtx.Resources.FileManager.Error004;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			bool blnFound = false;

			string strOldRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, oldName);

			string strOldPathName =
				Server.MapPath(strOldRootRelativePathName);

			if (System.IO.File.Exists(strOldPathName))
			{
				blnFound = true;

				string strNewRootRelativePathName =
					string.Format("{0}/{1}", rootRelativePath, newName);

				string strNewPathName =
					Server.MapPath(strNewRootRelativePathName);

				try
				{
					System.IO.File.Move(strOldRootRelativePathName, strNewRootRelativePathName);

					Result oResult =
						new Result(Result.Types.OK, string.Empty);

					return (Json(new { Result = oResult },
						System.Web.Mvc.JsonRequestBehavior.AllowGet));
				}
				catch (System.Exception ex)
				{
					Result oResult =
						new Result(Result.Types.ERROR, ex.Message);

					return (Json(new { Result = oResult },
						System.Web.Mvc.JsonRequestBehavior.AllowGet));
				}
			}
			else
			{
				if (System.IO.Directory.Exists(strOldPathName))
				{
					blnFound = true;

					string strNewRootRelativePathName =
						string.Format("{0}/{1}", rootRelativePath, newName);

					string strNewPathName =
						Server.MapPath(strNewRootRelativePathName);

					try
					{
						System.IO.Directory.Move(strOldPathName, strNewPathName);

						Result oResult =
							new Result(Result.Types.OK, string.Empty);

						return (Json(new { Result = oResult },
							System.Web.Mvc.JsonRequestBehavior.AllowGet));
					}
					catch (System.Exception ex)
					{
						Result oResult =
							new Result(Result.Types.ERROR, ex.Message);

						return (Json(new { Result = oResult },
							System.Web.Mvc.JsonRequestBehavior.AllowGet));
					}
				}
			}

			if (blnFound == false)
			{
				Result oResult =
					new Result(Result.Types.ERROR, "NOT FOUND!");

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			return (null);
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.CreateFolder)]
		public virtual System.Web.Mvc.JsonResult CreateFolder(string rootRelativePath, string folderName)
		{
			if (string.IsNullOrEmpty(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strMessage =
					Dtx.Resources.FileManager.Error002;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			if (string.IsNullOrEmpty(folderName))
			{
				// نام پوشه را وارد نکرده‌ايد
				string strMessage =
					Dtx.Resources.FileManager.Error005;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			string strRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, folderName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			if (System.IO.Directory.Exists(strPathName))
			{
				// پوشه‌ای با چنين نامی قبلا ايجاد شده است! لطفا از نام ديگری استفاده نماييد
				string strMessage =
					Dtx.Resources.FileManager.Error006;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			try
			{
				System.IO.DirectoryInfo oDirectoryInfo =
					System.IO.Directory.CreateDirectory(strPathName);

				string strData = string.Empty;

				strData += "<tr>";
				strData += "	<td>";
				strData += "		<a href='#' id='xxxxx' rename='directory'><span class='glyphicon glyphicon-pencil'></span></a>";
				strData += "	</td>";
				strData += "	<td>";
				strData += "		<input type='checkbox' name='checkBoxes' value='xxxxx' />";
				strData += "	</td>";
				strData += "	<td>";
				strData += "		<span class='glyphicon glyphicon-folder-close'></span>";
				strData += "	</td>";
				strData += "	<td style='direction: ltr'>";
				strData += "		<a href='/Administrator/FileManager/List?rootRelativePath=~/Views/Shared/_Layouts/default.fa-IR/xxxxx'>xxxxx</a>";
				strData += "			</td>";
				strData += "			<td></td>";
				strData += "			<td>";
				strData += "				" +
					Dtx.Calendar.Utility.DisplayDateTime(oDirectoryInfo.CreationTime, true);
				strData += "			</td>";
				strData += "			<td>";
				strData += "				" +
					Dtx.Calendar.Utility.DisplayDateTime(oDirectoryInfo.LastWriteTime, true);
				strData += "			</td>";
				strData += "		</tr>";

				strData = strData.Replace("xxxxx", folderName);

				Result oResult =
					new Result(Result.Types.OK, string.Empty, strData);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}
			catch (System.Exception ex)
			{
				Result oResult =
					new Result(Result.Types.ERROR, ex.Message);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.UploadFile)]
		public virtual System.Web.Mvc.JsonResult UploadFile(string rootRelativePath)
		{
			if (string.IsNullOrEmpty(rootRelativePath))
			{
				// مسير پوشه جاری صحيح نمی‌باشد
				string strMessage =
					Dtx.Resources.FileManager.Error002;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			// **************************************************
			System.Web.HttpPostedFileBase oPostedFile = null;

			if (Request.Files.Count == 0)
			{
				// فايلی برای آپلود مشخص نشده است!
				string strMessage =
					Dtx.Resources.FileManager.Error007;

				Result oResult =
					new Result(Result.Types.ERROR, strMessage);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}

			oPostedFile = Request.Files[0];
			// **************************************************

			string strFileName =
				System.IO.Path.GetFileName(oPostedFile.FileName);

			string strParentPath =
				Server.MapPath(rootRelativePath);

			Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);

			string strRootRelativePathName =
				string.Format("{0}/{1}", rootRelativePath, strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			oPostedFile.SaveAs(strPathName);

			try
			{
				System.IO.FileInfo oFileInfo =
					new System.IO.FileInfo(strPathName);

				string strData = string.Empty;

				strData += "<tr>";
				strData += "	<td>";
				strData += "		<a href='#' id='xxxxx' rename='directory'><span class='glyphicon glyphicon-pencil'></span></a>";
				strData += "	</td>";
				strData += "	<td>";
				strData += "		<input type='checkbox' name='checkBoxes' value='xxxxx' />";
				strData += "	</td>";
				strData += "	<td>";
				strData += "		<span class='glyphicon glyphicon-folder-close'></span>";
				strData += "	</td>";
				strData += "	<td style='direction: ltr'>";
				strData += "		<a href='/Administrator/FileManager/List?rootRelativePath=~/Views/Shared/_Layouts/default.fa-IR/xxxxx'>xxxxx</a>";
				strData += "			</td>";
				strData += "			<td></td>";
				strData += "			<td>";
				strData += "				" +
					Dtx.Calendar.Utility.DisplayDateTime(oFileInfo.CreationTime, true);
				strData += "			</td>";
				strData += "			<td>";
				strData += "				" +
					Dtx.Calendar.Utility.DisplayDateTime(oFileInfo.LastWriteTime, true);
				strData += "			</td>";
				strData += "		</tr>";

				strData = strData.Replace("xxxxx", strFileName);

				Result oResult =
					new Result(Result.Types.OK, string.Empty, strData);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}
			catch (System.Exception ex)
			{
				Result oResult =
					new Result(Result.Types.ERROR, ex.Message);

				return (Json(new { Result = oResult },
					System.Web.Mvc.JsonRequestBehavior.AllowGet));
			}
		}
	}
}

public class Result : System.Object
{
	public enum Types
	{
		OK,
		ERROR
	}

	//public Result()
	//{
	//}

	public Result(Types type, string message)
	{
		Type = type;
		Message = message;
	}

	public Result(Types type, string message, string data)
		: this(type, message)
	{
		Data = data;
	}

	public Types Type { get; set; }

	public string Data { get; set; }
	public string Message { get; set; }
}
