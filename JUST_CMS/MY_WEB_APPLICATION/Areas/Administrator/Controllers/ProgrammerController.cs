﻿using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	/// <summary>
	/// Version: 1.1.2
	/// Update Date: 1393/05/04
	/// 
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: true,
		keyName: Resources.Strings.ControllersKeys.Administrator.Programmer)]
	public partial class ProgrammerController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public ProgrammerController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.BackupDatabase)]
		public virtual System.Web.Mvc.ViewResult BackupDatabase()
		{
			// **************************************************
			string strPath = string.Empty;

			strPath =
				Server.MapPath("~/App_Data_Public");

			Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

			strPath =
				string.Format("{0}\\DatabaseBackups", strPath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				System.IO.Directory.CreateDirectory(strPath);
			}
			// **************************************************

			ViewModels.Areas.Administrator.Programmer.BackupDatabaseViewModel oBackupDatabaseViewModel =
				new ViewModels.Areas.Administrator.Programmer.BackupDatabaseViewModel();

			return (View(oBackupDatabaseViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.BackupDatabase)]
		public virtual System.Web.Mvc.ActionResult BackupDatabase
			(ViewModels.Areas.Administrator.Programmer.BackupDatabaseViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					using (System.Transactions.TransactionScope oTransactionScope =
						new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Suppress))
					{
						string strPath =
							Server.MapPath("~/App_Data_Public/DatabaseBackups");

						string strPathName =
							string.Format("{0}\\{1}", strPath, viewModel.FileName);

						UnitOfWork.BackupDatabase(viewModel, strPathName);

						PageMessages.Add
							(new Infrastructure.PageMessage
								(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.DatabaseBackup));
					}
				}
				catch (System.Exception ex)
				{
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Error, ex.Message));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete
			(System.Web.Mvc.FormCollection formCollection)
		{
			string strRequest =
				formCollection["chkSelect"];

			if (string.IsNullOrWhiteSpace(strRequest) == false)
			{
				string strPath = string.Empty;

				strPath =
					Server.MapPath("~/App_Data_Public/DatabaseBackups");

				Dtx.IO.Directory.SetFullControlForEveryOne(strPath);

				string[] strSelects = strRequest.Split(',');

				foreach (string strSelect in strSelects)
				{
					string strPathName =
						string.Format("{0}\\{1}", strPath, strSelect);

					if (System.IO.File.Exists(strPathName))
					{
						try
						{
							System.IO.File.Delete(strPathName);
						}
						catch { }
					}
				}
			}

			return (RedirectToAction(MVC.Administrator.Programmer.BackupDatabase()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: true,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Sync)]
		public virtual System.Web.Mvc.ActionResult SyncAreasControllersActionsWithSourceCode()
		{
			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.Get();

			foreach (Models.ProjectArea oProjectArea in varProjectAreas)
			{
				oProjectArea.IsAvailableInSourceCode = false;
			}
			// **************************************************

			// **************************************************
			var varProjectControllers =
				UnitOfWork.ProjectControllerRepository.Get();

			foreach (Models.ProjectController oProjectController in varProjectControllers)
			{
				oProjectController.IsAvailableInSourceCode = false;
			}
			// **************************************************

			// **************************************************
			var varProjectActions =
				UnitOfWork.ProjectActionRepository.Get();

			foreach (Models.ProjectAction oProjectAction in varProjectActions)
			{
				oProjectAction.IsAvailableInSourceCode = false;
			}
			// **************************************************

			UnitOfWork.Save();

			SyncAreasAndControllers();

			UnitOfWork.Save();

			// **************************************************
			varProjectActions =
				UnitOfWork.ProjectActionRepository.Get();

			foreach (Models.ProjectAction oProjectAction in varProjectActions)
			{
				if (oProjectAction.IsAvailableInSourceCode == false)
				{
					UnitOfWork.ProjectActionRepository.Delete(oProjectAction);
				}
			}
			// **************************************************

			// **************************************************
			varProjectControllers =
				UnitOfWork.ProjectControllerRepository.Get();

			foreach (Models.ProjectController oProjectController in varProjectControllers)
			{
				if (oProjectController.IsAvailableInSourceCode == false)
				{
					UnitOfWork.ProjectControllerRepository.Delete(oProjectController);
				}
			}
			// **************************************************

			// **************************************************
			varProjectAreas =
				UnitOfWork.ProjectAreaRepository.Get();

			foreach (Models.ProjectArea oProjectArea in varProjectAreas)
			{
				if (oProjectArea.IsAvailableInSourceCode == false)
				{
					UnitOfWork.ProjectAreaRepository.Delete(oProjectArea);
				}
			}
			// **************************************************

			UnitOfWork.Save();

			// **************************************************
			// عمليات به‌روزرسانی اطلاعات زيرسيستم‌ها، بخش‌ها و فعاليت‌ها با موفقيت انجام گرديد
			Infrastructure.PageMessage oPageMessage =
				new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Information003);

			PageMessages.Add(oPageMessage);
			// **************************************************

			return (View());
		}

		private bool IsAValidController(System.Type type)
		{
			if ((type.IsClass) &&
				(type.Name.EndsWith("Controller", System.StringComparison.InvariantCultureIgnoreCase)) &&
				(type.Namespace.EndsWith("Controllers", System.StringComparison.InvariantCultureIgnoreCase)) &&
				(type.Name.StartsWith("T4MVC_", System.StringComparison.InvariantCultureIgnoreCase) == false))
			{
				return (true);
			}
			else
			{
				return (false);
			}
		}

		private void SyncAreasAndControllers()
		{
			System.Reflection.Assembly myAssembly =
				System.Reflection.Assembly.GetExecutingAssembly();

			foreach (System.Type oType in myAssembly.ManifestModule.FindTypes(null, null))
			{
				if (IsAValidController(oType))
				{
					if (Dtx.String.Contains(oType.Namespace, ".Controllers", ignoreCase: true))
					{
						string strAreaName = string.Empty;

						if (Dtx.String.Contains(oType.Namespace, ".Areas.", ignoreCase: true))
						{
							string[] strNamespaceParts = oType.Namespace.Split('.');
							for (int intIndex = 0; intIndex <= strNamespaceParts.Length - 1; intIndex++)
							{
								string strNamespacePart =
									strNamespaceParts[intIndex];

								if (string.Compare(strNamespacePart, "Areas", ignoreCase: true) == 0)
								{
									strAreaName =
										strNamespaceParts[intIndex + 1];
									break;
								}
							}
						}

						Models.ProjectArea oProjectArea =
							InsertOrUpdateProjectArea(strAreaName);

						// **************************************************
						System.Attribute[] oCustomAttributes =
							System.Attribute.GetCustomAttributes(oType);

						Infrastructure.ProjectControllerPermissionAttribute oProjectControllerPermissionAttribute = null;

						foreach (System.Attribute oCustomAttribute in oCustomAttributes)
						{
							if (oCustomAttribute is Infrastructure.ProjectControllerPermissionAttribute)
							{
								oProjectControllerPermissionAttribute =
									oCustomAttribute as Infrastructure.ProjectControllerPermissionAttribute;

								break;
							}
						}
						// **************************************************

						Models.ProjectController oProjectController =
							InsertOrUpdateProjectController
							(oProjectArea, oType.Name, oProjectControllerPermissionAttribute);

						SyncActions(oProjectController, oType);
					}
				}
			}
		}

		private Models.ProjectArea InsertOrUpdateProjectArea(string areaName)
		{
			Models.ProjectArea oProjectArea =
				UnitOfWork.ProjectAreaRepository.Get()
				.Where(current => current.Name == areaName)
				.FirstOrDefault();

			if (oProjectArea == null)
			{
				oProjectArea = new Models.ProjectArea();

				// **************************************************
				// **************************************************
				// **************************************************
				oProjectArea.SetIsActive
					(true, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				// **************************************************
				oProjectArea.Name = areaName;
				oProjectArea.DisplayName = areaName;

				oProjectArea.IsAvailableInSourceCode = true;
				oProjectArea.SetIsDeleted(false, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ProjectAreaRepository.Insert(oProjectArea);
			}
			else
			{
				oProjectArea.IsAvailableInSourceCode = true;
			}

			if (oProjectArea.ProjectControllers == null)
			{
				oProjectArea.ProjectControllers =
					new System.Collections.Generic.List<Models.ProjectController>();
			}

			UnitOfWork.Save();

			return (oProjectArea);
		}

		private string GetDisplayNameForController
			(Infrastructure.ProjectControllerPermissionAttribute attribute)
		{
			string strResult = string.Empty;

			if (attribute.ResourceType == null)
			{
				strResult =
					Resources.Controllers.ResourceManager.GetString
					(attribute.KeyName, Culture);
			}
			else
			{
				System.Resources.ResourceManager oResourceManager =
					new System.Resources.ResourceManager(attribute.ResourceType);

				oResourceManager.IgnoreCase = true;

				object objResult = null;

				try
				{
					objResult =
						oResourceManager.GetObject(attribute.KeyName, Culture);
				}
				catch
				{
				}

				if (objResult != null)
				{
					strResult = objResult.ToString().Trim();
				}
			}

			return (strResult);
		}

		private Models.ProjectController InsertOrUpdateProjectController
			(Models.ProjectArea projectArea, string controllerName,
			Infrastructure.ProjectControllerPermissionAttribute projectControllerPermission)
		{
			// حذف کلمه کنترلر از انتهای کلمه
			controllerName =
				controllerName.Substring(0, controllerName.Length - 10);

			Models.ProjectController oProjectController =
				UnitOfWork.ProjectControllerRepository.Get()
				.Where(current => current.Name == controllerName)
				.Where(current => current.ProjectAreaId == projectArea.Id)
				.FirstOrDefault();

			if (oProjectController == null)
			{
				oProjectController = new Models.ProjectController();

				// **************************************************
				// **************************************************
				// **************************************************
				oProjectController.SetIsActive
					(true, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				// **************************************************
				oProjectController.Name = controllerName;
				oProjectController.ProjectAreaId = projectArea.Id;
				oProjectController.IsAvailableInSourceCode = true;

				if (projectControllerPermission == null)
				{
					oProjectController.IsVisibleJustForProgrammer = true;
				}
				else
				{
					oProjectController.IsVisibleJustForProgrammer =
						projectControllerPermission.IsVisibleJustForProgrammer;

					if (string.IsNullOrEmpty(projectControllerPermission.KeyName) == false)
					{
						oProjectController.DisplayName =
							GetDisplayNameForController(projectControllerPermission);
					}
				}

				if (string.IsNullOrEmpty(oProjectController.DisplayName))
				{
					oProjectController.DisplayName = controllerName;
				}
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.ProjectControllerRepository.Insert(oProjectController);
			}
			else
			{
				oProjectController.IsAvailableInSourceCode = true;

				if (projectControllerPermission != null)
				{
					oProjectController.IsVisibleJustForProgrammer =
						projectControllerPermission.IsVisibleJustForProgrammer;

					if (string.IsNullOrEmpty(projectControllerPermission.KeyName) == false)
					{
						oProjectController.DisplayName =
							Resources.Controllers.ResourceManager.GetString
							(projectControllerPermission.KeyName, Culture);
					}
				}

				if (string.IsNullOrEmpty(oProjectController.DisplayName))
				{
					oProjectController.DisplayName = controllerName;
				}
			}

			if (oProjectController.ProjectActions == null)
			{
				oProjectController.ProjectActions =
					new System.Collections.Generic.List<Models.ProjectAction>();
			}

			UnitOfWork.Save();

			return (oProjectController);
		}

		private void SyncActions(Models.ProjectController oProjectController, System.Type oType)
		{
			var varBindingFlags =
				System.Reflection.BindingFlags.Public |
				System.Reflection.BindingFlags.Instance |
				System.Reflection.BindingFlags.DeclaredOnly;

			var varMethodInfos =
				oType.GetMethods(bindingAttr: varBindingFlags)
				.Where(current => current.IsSpecialName == false)
				.OrderBy(current => current.Name)
				;

			foreach (System.Reflection.MethodInfo oMethodInfo in varMethodInfos)
			{
				bool blnIsAction = true;
				string strActionName = oMethodInfo.Name;

				System.Attribute[] oCustomAttributes =
					System.Attribute.GetCustomAttributes(oMethodInfo);

				Infrastructure.ProjectActionPermissionAttribute oActionPermissionAttribute = null;

				foreach (System.Attribute oCustomAttribute in oCustomAttributes)
				{
					System.Web.Mvc.NonActionAttribute oNonActionAttribute =
						oCustomAttribute as System.Web.Mvc.NonActionAttribute;

					if (oNonActionAttribute != null)
					{
						blnIsAction = false;
						break;
					}

					System.Web.Mvc.ActionNameAttribute oActionNameAttribute =
						oCustomAttribute as System.Web.Mvc.ActionNameAttribute;

					if (oActionNameAttribute != null)
					{
						strActionName = oActionNameAttribute.Name;
					}

					if (oCustomAttribute is Infrastructure.ProjectActionPermissionAttribute)
					{
						oActionPermissionAttribute =
							oCustomAttribute as Infrastructure.ProjectActionPermissionAttribute;
					}
				}

				if (blnIsAction)
				{
					Models.ProjectAction oProjectAction =
						InsertOrUpdateProjectAction
						(oProjectController, strActionName, oActionPermissionAttribute);
				}
			}
		}

		private Models.AccessType _publicAccessType;
		private Models.AccessType PublicAccessType
		{
			get
			{
				if (_publicAccessType == null)
				{
					_publicAccessType =
						UnitOfWork.AccessTypeRepository.Get()
						.Where(current => current.CultureLcid == CultureLcid)
						.Where(current => current.Code == (int)Models.Enums.AccessTypes.Public)
						.FirstOrDefault();
				}
				return (_publicAccessType);
			}
		}

		private Models.AccessType _registeredAccessType;
		private Models.AccessType RegisteredAccessType
		{
			get
			{
				if (_registeredAccessType == null)
				{
					_registeredAccessType =
						UnitOfWork.AccessTypeRepository.Get()
						.Where(current => current.CultureLcid == CultureLcid)
						.Where(current => current.Code == (int)Models.Enums.AccessTypes.Registered)
						.FirstOrDefault();
				}
				return (_registeredAccessType);
			}
		}

		private Models.AccessType _specialAccessType;
		private Models.AccessType SpecialAccessType
		{
			get
			{
				if (_specialAccessType == null)
				{
					_specialAccessType =
						UnitOfWork.AccessTypeRepository.Get()
						.Where(current => current.CultureLcid == CultureLcid)
						.Where(current => current.Code == (int)Models.Enums.AccessTypes.Special)
						.FirstOrDefault();
				}
				return (_specialAccessType);
			}
		}

		private string GetDisplayNameForAction
			(Infrastructure.ProjectActionPermissionAttribute attribute)
		{
			string strResult = string.Empty;

			if (attribute.ResourceType == null)
			{
				strResult =
					Resources.Actions.ResourceManager.GetString
					(attribute.KeyName, Culture);
			}
			else
			{
				System.Resources.ResourceManager oResourceManager =
					new System.Resources.ResourceManager(attribute.ResourceType);

				oResourceManager.IgnoreCase = true;

				object objResult = null;

				try
				{
					objResult =
						oResourceManager.GetObject(attribute.KeyName, Culture);
				}
				catch
				{
				}

				if (objResult != null)
				{
					strResult = objResult.ToString().Trim();
				}
			}

			return (strResult);
		}

		private Models.ProjectAction InsertOrUpdateProjectAction
			(Models.ProjectController projectController,
			string actionName, Infrastructure.ProjectActionPermissionAttribute projectActionPermission)
		{
			Models.ProjectAction oProjectAction =
				UnitOfWork.ProjectActionRepository.Get()
				.Where(current => current.Name == actionName)
				.Where(current => current.ProjectControllerId == projectController.Id)
				.FirstOrDefault();

			// اگر چنين فعاليتی وجود نداشت
			if (oProjectAction == null)
			{
				oProjectAction = new Models.ProjectAction();

				// **************************************************
				//oProjectAction.IsActive = true;

				oProjectAction.SetIsActive
					(true, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				oProjectAction.Name = actionName;
				oProjectAction.IsAvailableInSourceCode = true;
				oProjectAction.ProjectControllerId = projectController.Id;

				if (projectActionPermission == null)
				{
					oProjectAction.DisplayName = actionName;
					oProjectAction.IsVisibleJustForProgrammer = true;
					oProjectAction.AccessTypeId = SpecialAccessType.Id;
				}
				else
				{
					oProjectAction.AccessTypeId =
						GetAccessTypeId(projectActionPermission);

					oProjectAction.IsVisibleJustForProgrammer =
						projectActionPermission.IsVisibleJustForProgrammer;

					if (string.IsNullOrEmpty(projectActionPermission.KeyName) == false)
					{
						oProjectAction.DisplayName =
							GetDisplayNameForAction(projectActionPermission);
					}
				}

				if (string.IsNullOrEmpty(oProjectAction.DisplayName))
				{
					oProjectAction.DisplayName = actionName;
				}

				UnitOfWork.ProjectActionRepository.Insert(oProjectAction);
			}
			// اگر چنين فعاليتی وجود داشت
			else
			{
				oProjectAction.IsAvailableInSourceCode = true;

				if (projectActionPermission != null)
				{
					oProjectAction.AccessTypeId =
						GetAccessTypeId(projectActionPermission);

					projectActionPermission.IsVisibleJustForProgrammer =
						projectActionPermission.IsVisibleJustForProgrammer;

					if (string.IsNullOrEmpty(projectActionPermission.KeyName) == false)
					{
						oProjectAction.DisplayName =
							Resources.Actions.ResourceManager.GetString
							(projectActionPermission.KeyName, Culture);
					}
				}

				if (string.IsNullOrEmpty(oProjectAction.DisplayName))
				{
					oProjectAction.DisplayName = actionName;
				}
			}

			UnitOfWork.Save();

			return (oProjectAction);
		}

		private System.Guid GetAccessTypeId(Infrastructure.ProjectActionPermissionAttribute projectActionPermission)
		{
			switch (projectActionPermission.AccessType)
			{
				case Models.Enums.AccessTypes.Public:
				{
					return (PublicAccessType.Id);
				}

				case Models.Enums.AccessTypes.Registered:
				{
					return (RegisteredAccessType.Id);
				}

				default:
				//case Models.Enums.AccessTypes.Special:
				{
					return (SpecialAccessType.Id);
				}
			}
		}
	}
}
