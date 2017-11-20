using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Areas.Administrator.Controllers
{
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Administrator.User)]
	public partial class UserController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public UserController()
		{
		}

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
			keyName: Resources.Strings.ActionsKeys.GetUsers)]
		public virtual System.Web.Mvc.JsonResult GetUsers()
		{
			var varUsers =
				UnitOfWork.UserRepository.Get()
				.OrderByDescending(current => current.UpdateDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.User.IndexViewModel()
					{
						Id = current.Id,
						IsActive = current.IsActive,
						IsEmailAddressVerified = current.IsEmailAddressVerified,
						IsVerified = current.IsVerified,
						IsDirectContactable = current.IsDirectContactable,
						IsAccountTerminated = current.IsAccountTerminated,
						IsDeleted = current.IsDeleted,
						Gender = current.Gender.Name1,
						FirstName = current.FirstName,
						LastName = current.LastName,
						Role = current.Role.Name,
						HasAvatar = current.HasAvatar
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.User.IndexViewModel>(varUsers);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		private string FixText(string text)
		{
			if (text == null)
			{
				return (string.Empty);
			}

			text = text.Trim();
			if (text == string.Empty)
			{
				return (string.Empty);
			}

			while (text.Contains("  "))
			{
				text = text.Replace("  ", " ");
			}

			return (text);
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Search)]
		public virtual System.Web.Mvc.JsonResult Search
			(ViewModels.Areas.Administrator.User.SearchViewModel viewModel)
		{
			var varPeople =
				UnitOfWork.UserRepository.Get()
				;

			// **************************************************
			viewModel.FirstName =
				FixText(viewModel.FirstName);

			if (viewModel.FirstName != string.Empty)
			{
				varPeople =
					varPeople
					.Where(current => current.FirstName.Contains(viewModel.FirstName))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.LastName =
				FixText(viewModel.LastName);

			if (viewModel.LastName != string.Empty)
			{
				varPeople =
					varPeople
					.Where(current => current.LastName.Contains(viewModel.LastName))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.EmailAddress =
				FixText(viewModel.EmailAddress);

			if (viewModel.EmailAddress != string.Empty)
			{
				varPeople =
					varPeople
					.Where(current => current.EmailAddress.Contains(viewModel.EmailAddress))
					;
			}
			// **************************************************

			// **************************************************
			viewModel.CellPhoneNumber =
				FixText(viewModel.CellPhoneNumber);

			if (viewModel.CellPhoneNumber != string.Empty)
			{
				varPeople =
					varPeople
					.Where(current => current.CellPhoneNumber.Contains(viewModel.CellPhoneNumber))
					;
			}
			// **************************************************

			// **************************************************
			if (viewModel.GenderId.HasValue)
			{
				varPeople =
					varPeople
					.Where(current => current.GenderId == viewModel.GenderId.Value)
					;
			}
			// **************************************************

			var varNewPeople =
				varPeople
				.OrderByDescending(current => current.UpdateDateTime)
				.Select(current =>
					new ViewModels.Areas.Administrator.User.IndexViewModel()
					{
						Id = current.Id,
						IsActive = current.IsActive,
						IsEmailAddressVerified = current.IsEmailAddressVerified,
						IsVerified = current.IsVerified,
						IsDirectContactable = current.IsDirectContactable,
						IsAccountTerminated = current.IsAccountTerminated,
						IsDeleted = current.IsDeleted,
						Gender = current.Gender.Name1,
						FirstName = current.FirstName,
						LastName = current.LastName,
						Role = current.Role.Name,
						HasAvatar = current.HasAvatar
					})
					;

			var varResult =
				Dtx.Kendo.HtmlHelpers
				.ParseGridData<ViewModels.Areas.Administrator.User.IndexViewModel>(varNewPeople);

			return (Json(varResult, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		}

		//[System.Web.Mvc.HttpPost]
		//[Infrastructure.ProjectActionPermission
		//	(isVisibleJustForProgrammer: false,
		//	accessType: Models.Enums.AccessTypes.Special,
		//	keyName: Resources.Strings.ActionsKeys.Delete)]
		//public virtual System.Web.Mvc.JsonResult Delete(System.Guid? id)
		//{
		//	string strMessage = string.Empty;

		//	if (id.HasValue == false)
		//	{
		//		strMessage = "کد کاربر درجهت حذف مشخص نشده است!";

		//		return (Json(new { Message = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		//	}

		//	System.Guid sId;

		//	try
		//	{
		//		sId = new System.Guid(id.Value.ToString());
		//	}
		//	catch
		//	{
		//		strMessage = "کد کاربر معتبر نمی باشد!";

		//		return (Json(new { Message = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		//	}

		//	Models.User oUser =
		//		UnitOfWork.UserRepository.GetById(sId);

		//	if (oUser == null)
		//	{
		//		strMessage = "کاربری با چنين کدی وجود ندارد!";

		//		return (Json(new { Message = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		//	}

		//	if (oUser.IsSystem)
		//	{
		//		strMessage = "شما قادر به حذف کاربران سيستمی نمی‌باشيد!";

		//		return (Json(new { Message = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		//	}

		//	string strFullName = oUser.FullName;

		//	try
		//	{
		//		UnitOfWork.UserRepository.Delete(oUser);

		//		UnitOfWork.Save();

		//		strMessage =
		//			string.Format("اطلاعات {0} با موفقيت از بانک اطلاعاتی حذف گرديد.", strFullName);

		//		return (Json(new { Message = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		//	}
		//	catch //(System.Exception ex)
		//	{
		//		oUser.SetIsDeleted
		//			(true, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

		//		UnitOfWork.Save();

		//		strMessage =
		//			"از اطلاعات اين کاربر در جای ديگری استفاده شده است، لذا شما قادر به حذف فيزيکی آن نمی‌باشيد! ما صرفا وضعيت کاربر را به حالت حذف شده درمی‌آوريم.";

		//		return (Json(new { Message = strMessage }, System.Web.Mvc.JsonRequestBehavior.AllowGet));
		//	}
		//}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Details)]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Include(current => current.Role)
				.Include(current => current.Gender)
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create()
		{
			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList(varGenders, "Id", "Name1");
			// **************************************************

			// **************************************************
			var varRoles =
				UnitOfWork.RoleRepository
				.GetActiveByCultureLcidAndLessThanOrEqualToCode
				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList(varRoles, "Id", "Name");
			// **************************************************

			// **************************************************
			Models.User oUser =
				new Models.User();
			// **************************************************

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Create)]
		public virtual System.Web.Mvc.ActionResult Create(Models.User user)
		{
			Models.User oUser = null;

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByEmailAddress(user.EmailAddress)
				;

			if (oUser != null)
			{
				// نشانی پست الکترونيکی تکراری است
				// لطفا از نشانی پست الکترونيکی ديگری استفاده نماييد
				ModelState.AddModelError
					("EmailAddress", Models.Resources.User.Error001);
			}
			// **************************************************

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByCellPhoneNumber(user.CellPhoneNumber)
				;

			if (oUser != null)
			{
				// شماره تلفن همراه تکراری است
				// لطفا از شماره تلفن همراه ديگری استفاده نماييد.
				ModelState.AddModelError
					("CellPhoneNumber", Models.Resources.User.Error004);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				string strPassword = user.Password;

				user.Password =
					Dtx.Security.Hashing.GetSha1(user.Password);

				user.EmailAddress = user.EmailAddress.ToLower();

				if (user.IsAccountTerminated)
				{
					user.AccountTerminateDateTime = Infrastructure.Utility.Now;
				}

				user.CultureLcid = CultureLcid;

				user.SetInsertDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				user.SetIsActive
					(user.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				user.SetIsDeleted
					(user.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				user.SetIsVerified
					(user.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserRepository.Insert(user);

				UnitOfWork.Save();

				Infrastructure.UserNotificationService
					.SendEmailAddressVerificationKey(user, strPassword);

				return (RedirectToAction(MVC.Administrator.User.Index()));
			}

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", user.GenderId);
			// **************************************************

			// **************************************************
			var varRoles =
				UnitOfWork.RoleRepository
				.GetActiveByCultureLcidAndLessThanOrEqualToCode
				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(varRoles, "Id", "Name", user.RoleId);
			// **************************************************

			return (View(user));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid id)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", oUser.GenderId);
			// **************************************************

			// **************************************************
			var varRoles =
				UnitOfWork.RoleRepository
				.GetActiveByCultureLcidAndLessThanOrEqualToCode
				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(varRoles, "Id", "Name", oUser.RoleId);
			// **************************************************

			// **************************************************
			Models.Culture oCulture =
				UnitOfWork.CultureRepository.GetByLcid(oUser.CultureLcid);

			oUser.CultureId = oCulture.Id;

			var varCultures =
				UnitOfWork.CultureRepository.GetActive();

			ViewBag.CultureId =
				new System.Web.Mvc.SelectList
					(varCultures, "Id", "NativeName", oUser.CultureId);
			// **************************************************

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Edit)]
		public virtual System.Web.Mvc.ActionResult Edit(Models.User user)
		{
			Models.User oUser = null;

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByEmailAddressExceptId(user.EmailAddress, user.Id)
				;

			if (oUser != null)
			{
				// نشانی پست الکترونيکی تکراری است
				// لطفا از نشانی پست الکترونيکی ديگری استفاده نماييد
				ModelState.AddModelError("EmailAddress", Models.Resources.User.Error001);
			}
			// **************************************************

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByCellPhoneNumberExceptId(user.CellPhoneNumber, user.Id)
				;

			if (oUser != null)
			{
				// شماره تلفن همراه تکراری است
				// لطفا از شماره تلفن همراه ديگری استفاده نماييد.
				ModelState.AddModelError("CellPhoneNumber", Models.Resources.User.Error004);
			}
			// **************************************************

			// **************************************************
			Models.User oOriginalUser =
				UnitOfWork.UserRepository.GetById(user.Id);

			if (oOriginalUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			// **************************************************
			Models.Culture oCulture =
				UnitOfWork.CultureRepository.GetById(user.CultureId);

			if (oCulture == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				// **************************************************
				// **************************************************
				oOriginalUser.RoleId = user.RoleId;
				oOriginalUser.Address = user.Address;
				oOriginalUser.GenderId = user.GenderId;
				oOriginalUser.JobTitle = user.JobTitle;
				oOriginalUser.LastName = user.LastName;
				oOriginalUser.FirstName = user.FirstName;
				oOriginalUser.CompanyName = user.CompanyName;
				oOriginalUser.Description = user.Description;
				oOriginalUser.EmailAddress = user.EmailAddress;
				oOriginalUser.NationalCode = user.NationalCode;
				oOriginalUser.IsInBlackList = user.IsInBlackList;
				oOriginalUser.MasterDataCode = user.MasterDataCode;
				oOriginalUser.CellPhoneNumber = user.CellPhoneNumber;
				oOriginalUser.IsAccountTerminated = user.IsAccountTerminated;
				oOriginalUser.IsDirectContactable = user.IsDirectContactable;
				oOriginalUser.IsEmailAddressVerified = user.IsEmailAddressVerified;
				oOriginalUser.AdministratorDescription = user.AdministratorDescription;
				oOriginalUser.IsCellPhoneNumberVerified = user.IsCellPhoneNumberVerified;
				// **************************************************

				// **************************************************
				oOriginalUser.CultureLcid = oCulture.Lcid;

				oOriginalUser.SetUpdateDateTime
					(Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				// **************************************************
				if (user.IsAccountTerminated == false)
				{
					oOriginalUser.IsAccountTerminated = false;
					oOriginalUser.AccountTerminateDateTime = null;
				}
				else
				{
					if (oOriginalUser.IsAccountTerminated == false)
					{
						oOriginalUser.IsAccountTerminated = true;
						oOriginalUser.AccountTerminateDateTime = Infrastructure.Utility.Now;
					}
				}
				// **************************************************

				// **************************************************
				if (Infrastructure.Sessions.AuthenticatedUser.Role == Models.Enums.Roles.Programmer)
				{
					oOriginalUser.IsSystem = user.IsSystem;
				}
				// **************************************************

				// **************************************************
				oOriginalUser.SetIsActive
					(user.IsActive, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				oOriginalUser.SetIsDeleted
					(user.IsDeleted, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);

				oOriginalUser.SetIsVerified
					(user.IsVerified, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************
				// **************************************************
				// **************************************************

				UnitOfWork.UserRepository.Update(oOriginalUser);

				UnitOfWork.Save();

				return (RedirectToAction(MVC.Administrator.User.Index()));
			}

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid)
				.ToList()
				;

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", user.GenderId);
			// **************************************************

			// **************************************************
			var varRoles =
				UnitOfWork.RoleRepository
				.GetActiveByCultureLcidAndLessThanOrEqualToCode
				(CultureLcid, Infrastructure.Sessions.AuthenticatedUser.RoleCode)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(varRoles, "Id", "Name", user.RoleId);
			// **************************************************

			// **************************************************
			user.CultureId = oCulture.Id;

			var varCultures =
				UnitOfWork.CultureRepository.GetActive();

			ViewBag.CultureId =
				new System.Web.Mvc.SelectList
					(varCultures, "Id", "NativeName", user.CultureId);
			// **************************************************

			return (View(user));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.Delete)]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid id)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			UnitOfWork.UserRepository.Delete(oUser);

			UnitOfWork.Save();

			return (RedirectToAction(MVC.Administrator.User.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Groups =
				UnitOfWork.GroupRepository.Get()
				.OrderBy(current => current.Name)
				.ToList()
				;

			switch (oUser.Role.CodeEnum)
			{
				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Programmer:
				case Models.Enums.Roles.Administrator:
				{
					// حق دسترسی اين کاربر، به کل سيستم، کامل بوده
					// و نيازی به تعريف حق دسترسی برای وی وجود ندارد
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, Models.Resources.User.Warning001));

					break;
				}
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectGroups)]
		public virtual System.Web.Mvc.ActionResult SelectGroups
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oGroups =
				UnitOfWork.GroupRepository.Get()
				.OrderBy(current => current.Name)
				.ToList()
				;

			oUser.Groups.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varGroup =
						oGroups
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varGroup != null)
					{
						oUser.Groups.Add(varGroup);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Groups = oGroups;

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectLabels)]
		public virtual System.Web.Mvc.ActionResult SelectLabels(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectProjectActionsForUser)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			switch (oUser.Role.CodeEnum)
			{
				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Programmer:
				case Models.Enums.Roles.Administrator:
				{
					// حق دسترسی اين کاربر، به کل سيستم، کامل بوده
					// و نيازی به تعريف حق دسترسی برای وی وجود ندارد
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, Models.Resources.User.Warning001));

					break;
				}
			}

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/User'>{0}</a>",
				Resources.Controllers.Administrator_User);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oUser.Id,
					name: oUser.FullName,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Models.Resources.User.EntityName,
					selectedProjectActions: oUser.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectProjectActionsForUser)]
		public virtual System.Web.Mvc.ActionResult SelectProjectActions
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var oProjectActions =
				UnitOfWork.ProjectActionRepository.Get()
				.ToList()
				;

			oUser.ProjectActions.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varProjectAction =
						oProjectActions
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varProjectAction != null)
					{
						oUser.ProjectActions.Add(varProjectAction);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			// **************************************************
			var varProjectAreas =
				UnitOfWork.ProjectAreaRepository.GetByRole
				(Infrastructure.Sessions.AuthenticatedUser.Role)
				.ToList()
				;

			string strReturnLink =
				string.Format("<a href='/Administrator/User'>{0}</a>",
				Resources.Controllers.Administrator_User);

			ViewModels.Shared._Partial_SelectProjectActionsViewModel o_Partial_SelectProjectActionsViewModel =
				new ViewModels.Shared._Partial_SelectProjectActionsViewModel
					(id: oUser.Id,
					name: oUser.FullName,
					returnLink: strReturnLink,
					projectAreas: varProjectAreas,
					entityName: Models.Resources.User.EntityName,
					selectedProjectActions: oUser.ProjectActions);

			return (View(o_Partial_SelectProjectActionsViewModel));
			// **************************************************
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages(System.Guid id)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code == Models.Enums.AccessTypes.Special)
				.OrderBy(current => current.Title)
				.ToList()
				;

			switch (oUser.Role.CodeEnum)
			{
				case Models.Enums.Roles.Owner:
				case Models.Enums.Roles.Programmer:
				case Models.Enums.Roles.Administrator:
				{
					// حق دسترسی اين کاربر، به کل سيستم، کامل بوده
					// و نيازی به تعريف حق دسترسی برای وی وجود ندارد
					PageMessages.Add
						(new Infrastructure.PageMessage
							(Infrastructure.Enums.PageMessageTyps.Warning, Models.Resources.User.Warning001));

					break;
				}
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.SelectPages)]
		public virtual System.Web.Mvc.ActionResult SelectPages
			(System.Guid id, System.Guid[] checkBoxes)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			var varPages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.ToList()
				;

			oUser.Pages.Clear();

			// در صورتی که لااقل يکی از چک باکس‌ها انتخاب شده باشد
			if (checkBoxes != null)
			{
				foreach (System.Guid sCheckBoxValue in checkBoxes)
				{
					var varPage =
						varPages
						.Where(current => current.Id == sCheckBoxValue)
						.FirstOrDefault();

					if (varPage != null)
					{
						oUser.Pages.Add(varPage);
					}
				}
			}

			UnitOfWork.Save();

			PageMessages.Add
				(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information, Resources.Messages.Save));

			ViewBag.Pages =
				UnitOfWork.CmsUnitOfWork.PageRepository.Get()
				.Where(current => (Models.Enums.AccessTypes)current.AccessType.Code ==
					Models.Enums.AccessTypes.Special)
				.OrderBy(current => current.Title)
				.ToList()
				;

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			ViewModels.Areas.Administrator.User.ChangePasswordViewModel oChangePasswordViewModel =
				new ViewModels.Areas.Administrator.User.ChangePasswordViewModel();

			oChangePasswordViewModel.UserId = oUser.Id;
			// **************************************************

			return (View(oChangePasswordViewModel));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Special,
			keyName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword
			(ViewModels.Areas.Administrator.User.ChangePasswordViewModel viewModel)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.GetById(viewModel.UserId);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			string strHashOfNewPassword =
				Dtx.Security.Hashing.GetSha1(viewModel.NewPassword);

			oUser.Password = strHashOfNewPassword;

			UnitOfWork.Save();

			// گذرواژه جديد برای کاربر ثبت گرديد
			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information,
				Models.Resources.User.Information004));

			// **************************************************
			ViewModels.Areas.Administrator.User.ChangePasswordViewModel oChangePasswordViewModel =
				new ViewModels.Areas.Administrator.User.ChangePasswordViewModel();

			oChangePasswordViewModel.UserId = oUser.Id;
			// **************************************************

			return (View(oChangePasswordViewModel));
		}
	}
}
