using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	public static class Email
	{
		private static Models.MailSettings GetMailSettings()
		{
			DAL.UnitOfWork oUnitOfWork = null;

			try
			{
				oUnitOfWork = new DAL.UnitOfWork();

				Models.ApplicationSettings oApplicationSettings =
					oUnitOfWork.ApplicationSettingsRepository.Get()
					.Where(current => current.CultureLcid == System.Threading.Thread.CurrentThread.CurrentCulture.LCID)
					.FirstOrDefault();

				if (oApplicationSettings == null)
				{
					return (null);
				}
				else
				{
					return (oApplicationSettings.MailSettings);
				}
			}
			catch
			{
				return (null);
			}
			finally
			{
				if (oUnitOfWork != null)
				{
					oUnitOfWork.Dispose();
					oUnitOfWork = null;
				}
			}
		}

		private static string EmailsBodyPath
		{
			get
			{
				return ("~/App_Data/LocalizedTemplates/Email");
			}
		}

		public static void SendAgainEmailAddressVerificationKey(Models.User user)
		{
			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			Dtx.Calendar.PersianDate oPersianDate =
				Dtx.Calendar.Convert.CivilToPersion(user.InsertDateTime);

			string strRootRelativePathName =
				string.Format("{0}/SendAgainEmailAddressVerificationKey.{1}.htm",
				EmailsBodyPath,
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strEmailBody =
				Dtx.IO.File.Read(strPathName);

			strEmailBody =
				strEmailBody
				.Replace("[DATE]", oPersianDate.Value4)
				.Replace("[EMAIL_ADDRESS]", user.EmailAddress)
				.Replace("[VERIFICATION_KEY]", user.EmailAddressVerificationKey.ToString())
				;

			System.Net.Mail.MailAddress oMailAddress =
				new System.Net.Mail.MailAddress
					(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

			Models.MailSettings oMailSettings = GetMailSettings();
			if (oMailSettings == null)
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailAddress, "Email Address Verification!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
			else
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailSettings, oMailAddress, "Email Address Verification!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
		}

		public static void SendEmailAddressVerificationKey(Models.User user, string password)
		{
			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			Dtx.Calendar.PersianDate oPersianDate =
				Dtx.Calendar.Convert.CivilToPersion(user.InsertDateTime);

			string strRootRelativePathName =
				string.Format("{0}/SendEmailAddressVerificationKey.{1}.htm",
				EmailsBodyPath,
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strEmailBody =
				Dtx.IO.File.Read(strPathName);

			strEmailBody =
				strEmailBody
				.Replace("[DATE]", oPersianDate.Value4)
				.Replace("[PASSWORD]", password)
				.Replace("[EMAIL_ADDRESS]", user.EmailAddress)
				.Replace("[VERIFICATION_KEY]",
				user.EmailAddressVerificationKey.ToString())
				;

			System.Net.Mail.MailAddress oMailAddress =
				new System.Net.Mail.MailAddress
					(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

			Models.MailSettings oMailSettings = GetMailSettings();
			if (oMailSettings == null)
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailAddress, "Email Address Verification!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
			else
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailSettings, oMailAddress, "Email Address Verification!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
		}

		public static void SendNewPassword(Models.User user, string newPassword)
		{
			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("{0}/SendNewPassword.{1}.htm",
				EmailsBodyPath,
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strEmailBody =
				Dtx.IO.File.Read(strPathName);

			strEmailBody =
				strEmailBody
				.Replace("[PASSWORD]", newPassword)
				.Replace("[EMAIL_ADDRESS]", user.EmailAddress)
				;

			System.Net.Mail.MailAddress oMailAddress =
				new System.Net.Mail.MailAddress
					(user.EmailAddress, user.EmailAddress, System.Text.Encoding.UTF8);

			Models.MailSettings oMailSettings = GetMailSettings();
			if (oMailSettings == null)
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailAddress, "New Password!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
			else
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailSettings, oMailAddress, "New Password!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
		}

		public static void NotifyUserAfterAction(Models.User user)
		{
			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("{0}/NotifyUserAfterAction.{1}.htm",
				EmailsBodyPath,
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strEmailBody =
				Dtx.IO.File.Read(strPathName);

			strEmailBody =
				strEmailBody
				.Replace("[FULL_NAME]", user.FullName)
				;

			System.Net.Mail.MailAddress oMailAddress =
				new System.Net.Mail.MailAddress
					(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

			Models.MailSettings oMailSettings = GetMailSettings();
			if (oMailSettings == null)
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailAddress, "Some Action Done For Your Resume!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
			else
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailSettings, oMailAddress, "Some Action Done For Your Resume!", strEmailBody, System.Net.Mail.MailPriority.High);
			}
		}

		public static void NotifyUserAfterAction(Models.User user, string message)
		{
			System.Net.Mail.MailAddress oMailAddress =
				new System.Net.Mail.MailAddress
					(user.EmailAddress, user.FullName, System.Text.Encoding.UTF8);

			Models.MailSettings oMailSettings = GetMailSettings();
			if (oMailSettings == null)
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailAddress, "Some Action Done For Your Resume!", message, System.Net.Mail.MailPriority.High);
			}
			else
			{
				Dtx.Net.Mail.MailMessage.Send
					(oMailSettings, oMailAddress, "Some Action Done For Your Resume!", message, System.Net.Mail.MailPriority.High);
			}
		}
	}
}
