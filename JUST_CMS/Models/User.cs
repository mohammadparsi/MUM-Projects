using System.Linq;
using System.Data.Entity;

namespace Models
{
	/// <summary>
	/// Version: 1.4.8
	/// Update Date: 1393/04/16
	/// 
	/// </summary>
	public class User : BaseLocalizedEntity
	{
		#region Configuration
		internal class Configuration :
			System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<User>
		{
			public Configuration()
			{
				HasRequired(current => current.Role)
					.WithMany(role => role.Users)
					.HasForeignKey(current => current.RoleId)
					.WillCascadeOnDelete(false)
					;

				HasRequired(current => current.Gender)
					.WithMany(gender => gender.Users)
					.HasForeignKey(current => current.GenderId)
					.WillCascadeOnDelete(false)
					;

				//HasOptional(current => current.VerifierUser)
				//	.WithMany(user => user.VerifiedUsers)
				//	.HasForeignKey(current => current.VerifierUserId)
				//	.WillCascadeOnDelete(false)
				//	;

				HasMany(current => current.Pages)
					.WithMany(page => page.Users)
					.Map(current =>
					{
						current.ToTable("DirectAccessesOfUsersToPages");
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("UserId");
						current.MapRightKey("PageId");
					});

				HasMany(current => current.Groups)
					.WithMany(group => group.Users)
					.Map(current =>
					{
						current.ToTable("UsersInGroups");
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("UserId");
						current.MapRightKey("GroupId");
					});

				HasMany(current => current.ProjectActions)
					.WithMany(projectAction => projectAction.Users)
					.Map(current =>
					{
						current.ToTable("ProjectActionsOfUsers");
						// MapRightKey را می نويسيم و بعد MapLeftKey اول
						// و سپس قانون دور در دور و نزديک در نزديک را رعايت می کنيم
						current.MapLeftKey("UserId");
						current.MapRightKey("ProjectActionId");
					});

				if (Utility.IsDatabaseSqlServerCompactEdition)
				{
					Property(current => current.Address)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.Description)
						.HasColumnType("ntext")
						.IsMaxLength()
						;

					Property(current => current.AdministratorDescription)
						.HasColumnType("ntext")
						.IsMaxLength()
						;
				}
			}
		}
		#endregion /Configuration

		public static User CreateUser(string emailAddress, string cellPhoneNumber)
		{
			string strPassword =
				Dtx.Security.Hashing.GetSha1(System.Guid.NewGuid().ToString());

			return (CreateUser(emailAddress, cellPhoneNumber, strPassword));
		}

		public static User CreateUser(string emailAddress, string cellPhoneNumber, string password)
		{
			DatabaseContext oDatabaseContext = null;

			try
			{
				oDatabaseContext = new DatabaseContext();

				// **************************************************
				User oTempUser = null;
				string strCellPhoneNumberVerificationKey = string.Empty;

				do
				{
					strCellPhoneNumberVerificationKey =
						System.Guid.NewGuid().ToString().ToUpper()
						.Substring(0, 6).Replace("O", "0").Replace("o", "0");

					oTempUser =
						oDatabaseContext.Users
						.Where(current =>
							string.Compare(current.CellPhoneNumberVerificationKey,
							strCellPhoneNumberVerificationKey, true) == 0)
						.FirstOrDefault();
				}
				while (oTempUser != null);
				// **************************************************

				int intCultureLcid =
					System.Threading.Thread.CurrentThread.CurrentCulture.LCID;

				Role oRole =
					oDatabaseContext.Roles
					.Where(current => current.CultureLcid == intCultureLcid)
					.Where(current => current.Code == (int)Enums.Roles.User)
					.FirstOrDefault()
					;

				Gender oGender =
					oDatabaseContext.Genders
					.Where(current => current.CultureLcid == intCultureLcid)
					.Where(current => current.Code == (int)Enums.Genders.Male)
					.FirstOrDefault()
					;

				User oUser = null;

				if ((oRole != null) && (oGender != null))
				{
					oUser = new User();

					oUser.AccountExpireDate = null;
					oUser.AccountTerminateDateTime = null;
					oUser.AdministratorDescription = null;
					oUser.AvatarExtension = null;
					oUser.BirthDate = null;
					oUser.CellPhoneNumber = cellPhoneNumber;
					oUser.CellPhoneNumberVerificationKey = strCellPhoneNumberVerificationKey;
					oUser.CultureLcid =
						System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
					oUser.DeleteDateTime = null;
					oUser.Description = null;
					oUser.EmailAddress = emailAddress;
					oUser.EmailAddressVerificationKey = System.Guid.NewGuid();
					oUser.FirstName = "-----";
					oUser.GenderId = oGender.Id;
					oUser.HasAvatar = false;
					oUser.HasResume = false;
					oUser.Hits = 0;
					oUser.Address = null;
					oUser.InsertDateTime = System.DateTime.Now;
					oUser.IsAccountExpirable = false;
					oUser.IsAccountTerminated = false;
					oUser.IsActive = true;
					oUser.IsCellPhoneNumberVerified = false;
					oUser.IsDeleted = false;
					oUser.IsDirectContactable = false;
					oUser.IsEmailAddressVerified = false;
					oUser.IsInBlackList = false;
					oUser.IsProfilePublic = false;
					oUser.IsSystem = false;
					oUser.IsVerified = false;
					oUser.LastLoginDateTime = null;
					oUser.LastName = "-----";
					oUser.LoginCount = 0;
					oUser.NationalCode = null;
					oUser.Password = Dtx.Security.Hashing.GetSha1(password);
					oUser.ProfileLevel = 0;
					oUser.ResumeExtension = null;
					oUser.RoleId = oRole.Id;
					oUser.UpdateDateTime = System.DateTime.Now;
					oUser.VerifyDateTime = null;

					var varValidationResult =
						oDatabaseContext.Entry(oUser).GetValidationResult();

					if (varValidationResult.IsValid)
					{
						return (oUser);
					}
					else
					{
						foreach (var varValidationError in varValidationResult.ValidationErrors)
						{
							string strPropertyName = varValidationError.PropertyName;
							string strErrorMessage = varValidationError.ErrorMessage;

							string strResult =
								string.Format("{0}: {1}", strPropertyName, strErrorMessage);
						}

						return (null);
					}
				}

				return (oUser);
			}
			catch (System.Exception)
			{
				return (null);
			}
			finally
			{
				if (oDatabaseContext != null)
				{
					oDatabaseContext.Dispose();
					oDatabaseContext = null;
				}
			}
		}

		public User()
		{
			EmailAddressVerificationKey = System.Guid.NewGuid();

			CellPhoneNumberVerificationKey =
				System.Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
		}

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Role)]
		public virtual Role Role { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Role)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid RoleId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Gender)]
		public virtual Gender Gender { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Gender)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]
		public System.Guid GenderId { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.EmailAddress)]

		[System.ComponentModel.DataAnnotations.EmailAddress]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForEmail,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForEmail)]
		public string EmailAddress { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsEmailAddressPublic)]
		public bool IsEmailAddressPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsEmailAddressVerified)]
		public bool IsEmailAddressVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.EmailAddressVerificationKey)]
		public System.Guid EmailAddressVerificationKey { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Hits)]
		public int Hits { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.ProfileLevel)]
		public int ProfileLevel { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.LoginCount)]
		public int LoginCount { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsInBlackList)]
		public bool IsInBlackList { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsAccountExpirable)]
		public bool IsAccountExpirable { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsDirectContactable)]
		public bool IsDirectContactable { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.HasAvatar)]
		public bool HasAvatar { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 4,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string AvatarExtension { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.HasResume)]
		public bool HasResume { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 4,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string ResumeExtension { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsProfilePublic)]
		public bool IsProfilePublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsAccountTerminated)]
		public bool IsAccountTerminated { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.CellPhoneNumber)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 14,
			MinimumLength = 14,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.StringLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForCellPhoneNumber,
			ErrorMessageResourceType = typeof(Models.Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForCellPhoneNumber)]
		public string CellPhoneNumber { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsCellPhoneNumberPublic)]
		public bool IsCellPhoneNumberPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsCellPhoneNumberVerified)]
		public bool IsCellPhoneNumberVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.CellPhoneNumberVerificationKey)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 10,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string CellPhoneNumberVerificationKey { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.FirstName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string FirstName { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.LastName)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 50,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string LastName { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Password)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.Password)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 40,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForPassword,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForPassword)]
		public string Password { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.ResetPasswordDateTime)]
		public System.DateTime? ResetPasswordDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.ResetPasswordVerificationKey)]
		public System.Guid? ResetPasswordVerificationKey { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.BirthDate)]
		public System.DateTime? BirthDate { get; set; }
		// **********

		// **********
		public string ShamsiBirthDate
		{
			get
			{
				if (BirthDate.HasValue == false)
				{
					return ("-----");
				}
				else
				{
					Dtx.Calendar.PersianDate oPersianDate =
						Dtx.Calendar.Convert.CivilToPersion(BirthDate.Value);

					return (oPersianDate.Value4);
				}
			}
		}
		// **********

		// **********
		public string Age
		{
			get
			{
				if (BirthDate.HasValue == false)
				{
					return ("-----");
				}
				else
				{
					int intAge = System.DateTime.Now.Year - BirthDate.Value.Year;

					return (Dtx.Text.Convert.DigitsToUnicode(intAge));
				}
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.ShamsiBirthD)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public int ShamsiBirthD { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.ShamsiBirthM)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public int ShamsiBirthM { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.ShamsiBirthY)]

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public int ShamsiBirthY { get; set; }
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsBirthDatePublic)]
		public bool IsBirthDatePublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.NationalCode)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 10,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForNationalCode,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForNationalCode)]
		public string NationalCode { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Address)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Address { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.IsAddressPublic)]
		public bool IsAddressPublic { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.RegisterIP)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 15,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string RegisterIP { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.AccountExpireDate)]
		public System.DateTime? AccountExpireDate { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.RegisterDateTime)]
		public override System.DateTime InsertDateTime
		{
			get
			{
				return (base.InsertDateTime);
			}
			protected internal set
			{
				base.InsertDateTime = value;
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.UpdateProfileDateTime)]
		public override System.DateTime? UpdateDateTime
		{
			get
			{
				return (base.UpdateDateTime);
			}
			protected internal set
			{
				base.UpdateDateTime = value;
			}
		}
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.LastLoginDateTime)]
		public System.DateTime? LastLoginDateTime { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.AccountTerminateDateTime)]
		public System.DateTime? AccountTerminateDateTime { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.CompanyName)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string CompanyName { get; set; }
		// **********

		// **********
		public string CompanyNameWithMasterDataCode
		{
			get
			{
				string strResult =
					string.Format("{0} - {1}",
					Dtx.Text.Convert.DisplayFormattedNumber(MasterDataCode), CompanyName);

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.JobTitle)]

		[System.ComponentModel.DataAnnotations.StringLength
			(maximumLength: 250,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.MaxLength)]
		public string JobTitle { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.HttpReferer)]
		public string HttpReferer { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.Description)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string Description { get; set; }
		// **********

		// **********
		[System.Web.Mvc.AllowHtml]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.AdministratorDescription)]

		[System.ComponentModel.DataAnnotations.DataType
			(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
		public string AdministratorDescription { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.User),
			Name = Resources.Strings.UserKeys.FullName)]
		public string FullName
		{
			get
			{
				string strLastName = string.Empty;
				if (string.IsNullOrEmpty(LastName) == false)
				{
					strLastName = LastName.Trim();
				}

				string strFirstName = string.Empty;
				if (string.IsNullOrEmpty(FirstName) == false)
				{
					strFirstName = FirstName.Trim();
				}

				string strFullName =
					string.Format("{0} {1}", strFirstName, strLastName).Trim();

				if (strFullName == string.Empty)
				{
					strFullName = EmailAddress;
				}
				else
				{
					try
					{
						if (Gender != null)
						{
							strFullName =
								string.Format("{0} {1}",
								Gender.Name2, strFullName).Trim();
						}
					}
					catch { }
				}

				return (strFullName);
			}
		}
		// **********

		// **********
		public string FullNameWithJobTitle
		{
			get
			{
				string strResult = FullName;

				if (string.IsNullOrEmpty(JobTitle) == false)
				{
					strResult =
						string.Format("{0} ({1})", strResult, JobTitle);
				}

				return (strResult);
			}
		}
		// **********

		//public virtual System.Collections.Generic.IList<User> VerifiedUsers { get; set; }

		public virtual System.Collections.Generic.IList<Group> Groups { get; set; }
		public virtual System.Collections.Generic.IList<UserLoginLog> LoginLogs { get; set; }
		public virtual System.Collections.Generic.IList<ProjectAction> ProjectActions { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Tag> VerifiedTags { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Page> WrittenPages { get; set; }
		public virtual System.Collections.Generic.IList<Cms.Page> VerifiedPages { get; set; }

		public virtual System.Collections.Generic.IList<Cms.Post> WrittenPosts { get; set; }
		public virtual System.Collections.Generic.IList<Cms.Post> VerifiedPosts { get; set; }

		// مربوط به سطح دسترسی می‌شود
		public virtual System.Collections.Generic.IList<Cms.Page> Pages { get; set; }
		//public virtual System.Collections.Generic.IList<Cms.MenuItem> MenuItems { get; set; }
		//public virtual System.Collections.Generic.IList<Cms.PhotoAlbum> PhotoAlbums { get; set; }
		public virtual System.Collections.Generic.IList<Cms.PostComment> PostComments { get; set; }
	}
}
