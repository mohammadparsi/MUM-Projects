using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/01/27
	/// 
	/// </summary>
	public static class Sms
	{
		static Sms()
		{
		}

		public static int GetCredit(Models.GlobalApplicationSettings globalApplicationSettings = null)
		{
			int intResult = 0;
			MY_WEB_APPLICATION.ir.dorsanet.sms.Send oSend = null;

			try
			{
				if (globalApplicationSettings == null)
				{
					globalApplicationSettings =
						Infrastructure.GlobalApplicationSettings.Instance;
				}

				oSend =
					new MY_WEB_APPLICATION.ir.dorsanet.sms.Send();

				string strUsername =
					globalApplicationSettings.SmsCenterUsername;

				string strPassword =
					globalApplicationSettings.SmsCenterPassword;

				// اگر خطای ذيل را دريافت کرديم
				// The request failed with HTTP status 417: Expectation failed.
				// اين بدان معنی است که
				// Proxy
				// اجرا شده و بايد آنرا قطع نماييم
				double dblResult =
					oSend.Credit(strUsername, strPassword);

				intResult = System.Convert.ToInt32(dblResult);
			}
			catch //(System.Exception ex)
			{
				intResult = -1;
			}
			finally
			{
				if (oSend != null)
				{
					oSend.Dispose();
					oSend = null;
				}
			}

			return (intResult);
		}

		public static bool Send
			(string recipients, string text, Models.GlobalApplicationSettings globalApplicationSettings = null)
		{
			if (recipients == null)
			{
				return (false);
			}

			recipients = recipients.Replace(" ", string.Empty);
			if (recipients == string.Empty)
			{
				return (false);
			}

			MY_WEB_APPLICATION.ir.dorsanet.sms.Send oSend = null;

			try
			{
				if (globalApplicationSettings == null)
				{
					globalApplicationSettings =
						Infrastructure.GlobalApplicationSettings.Instance;
				}

				//if (globalApplicationSettings.IsSmsServiceEnabled == false)
				//{
				//	return (false);
				//}

				oSend =
					new MY_WEB_APPLICATION.ir.dorsanet.sms.Send();

				int intCredit =
					GetCredit(globalApplicationSettings);

				if (intCredit <= 0)
				{
					return (false);
				}
				else
				{
					string strUsername =
						globalApplicationSettings.SmsCenterUsername;

					string strPassword =
						globalApplicationSettings.SmsCenterPassword;

					// شماره فرستنده
					string strFrom =
						globalApplicationSettings.SmsCenterNumber;

					// شماره های گيرنده يا گيرندگان
					string[] strRecipients = recipients.Split(',', ':', ';', '|');

					System.Collections.Generic.List<string> oTo =
						new System.Collections.Generic.List<string>();

					for (int intIndex = 0; intIndex <= strRecipients.Length - 1; intIndex++)
					{
						string strRecipient = strRecipients[intIndex];

						if (strRecipient.StartsWith("0098"))
						{
							strRecipient = strRecipient.Substring(4);

							if (strRecipient.Length == 10)
							{
								oTo.Add(strRecipient);
							}
						}
						else
						{
							if (strRecipient.StartsWith("0"))
							{
								strRecipient = strRecipient.Substring(1);

								if (strRecipient.Length == 10)
								{
									oTo.Add(strRecipient);
								}
							}
							else
							{
								if (strRecipient.Length == 10)
								{
									oTo.Add(strRecipient);
								}
							}
						}
					}

					string[] strTo = new string[oTo.Count];
					for (int intIndex = 0; intIndex <= oTo.Count - 1; intIndex++)
					{
						string strRecipient = oTo[intIndex];

						strTo[intIndex] = strRecipient;
					}

					//string strText = "اين اس ام اس برای تست ارسال شده است";

					string strText = text;

					// پيام به صورت خودکار روی گوشی گيرنده (گيرندگان) باز شود؟
					bool blnFlash = false;

					long[] lngRecIds = null;
					byte[] bytStatuses = null;

					int intSendingSmsResult = oSend.SendSms
						(username: strUsername,
						password: strPassword,
						to: strTo,
						from: strFrom,
						text: strText,
						flash: blnFlash,
						status: ref bytStatuses,
						recId: ref lngRecIds);

					if ((intSendingSmsResult == 0) && (bytStatuses.Length > 0) && (bytStatuses[0] == 0))
					{
						return (true);
					}
					else
					{
						return (false);
					}
				}
			}
			catch //(System.Exception ex)
			{
				return (false);
			}
			finally
			{
				if (oSend != null)
				{
					oSend.Dispose();
					oSend = null;
				}
			}
		}

		public static bool SendNewPassword(Models.User recipientUser, string newPassword)
		{
			// **************************************************
			if (recipientUser == null)
			{
				return (false);
			}

			if (string.IsNullOrEmpty(recipientUser.CellPhoneNumber))
			{
				return (false);
			}

			if (recipientUser.IsCellPhoneNumberVerified == false)
			{
				return (false);
			}
			// **************************************************

			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedTemplates/Sms/SendNewPassword.{0}.htm",
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strSmsBody =
				Dtx.IO.File.Read(strPathName);

			strSmsBody = strSmsBody
				.Replace("[PASSWORD]", newPassword)
				.Replace("[EMAIL_ADDRESS]", recipientUser.EmailAddress)
				;

			return (Send(recipientUser.CellPhoneNumber, strSmsBody));
		}

		public static bool NotifyUserAfterExamInvitation(Models.User recipientUser)
		{
			// **************************************************
			if (recipientUser == null)
			{
				return (false);
			}

			if (string.IsNullOrEmpty(recipientUser.CellPhoneNumber))
			{
				return (false);
			}

			if (recipientUser.IsCellPhoneNumberVerified == false)
			{
				return (false);
			}
			// **************************************************

			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedTemplates/Sms/NotifyRecipientAfterContacting.{0}.htm",
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strSmsBody =
				Dtx.IO.File.Read(strPathName);

			strSmsBody = strSmsBody
				.Replace("[RECIPIENT_FULL_NAME]", recipientUser.FullName)
				;

			return (Send(recipientUser.CellPhoneNumber, strSmsBody));
		}

		public static bool SendCellPhoneNumberVerificationKey(Models.User recipientUser)
		{
			// **************************************************
			if (recipientUser == null)
			{
				return (false);
			}

			if (string.IsNullOrEmpty(recipientUser.CellPhoneNumber))
			{
				return (false);
			}

			// دستور ذيل نبايد نوشته شود
			//if (recipientUser.IsCellPhoneNumberVerified == false)
			//{
			//	return (false);
			//}
			// **************************************************

			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedTemplates/Sms/SendCellPhoneNumberVerificationKey.{0}.htm",
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strSmsBody =
				Dtx.IO.File.Read(strPathName);

			strSmsBody = strSmsBody
				.Replace("[VERIFICATION_KEY]", recipientUser.CellPhoneNumberVerificationKey)
				;

			return (Send(recipientUser.CellPhoneNumber, strSmsBody));
		}

		internal static bool NotifyRecipientAfterContacting
			(Models.User recipientUser, string senderFullName)
		{
			// **************************************************
			if (recipientUser == null)
			{
				return (false);
			}

			if (string.IsNullOrEmpty(recipientUser.CellPhoneNumber))
			{
				return (false);
			}

			if (recipientUser.IsCellPhoneNumberVerified == false)
			{
				return (false);
			}
			// **************************************************

			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedTemplates/Sms/NotifyRecipientAfterContacting.{0}.htm",
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strSmsBody =
				Dtx.IO.File.Read(strPathName);

			strSmsBody = strSmsBody
				.Replace("[SENDER_FULL_NAME]", senderFullName)
				.Replace("[RECIPIENT_FULL_NAME]", recipientUser.FullName)
				;

			return (Send(recipientUser.CellPhoneNumber, strSmsBody));
		}

		internal static bool NotifyUserAfterAction(Models.User user)
		{
			// **************************************************
			if (user == null)
			{
				return (false);
			}

			if (user.IsCellPhoneNumberVerified == false)
			{
				return (false);
			}
			// **************************************************

			System.Globalization.CultureInfo oCultureInfo =
				System.Threading.Thread.CurrentThread.CurrentCulture;

			string strRootRelativePathName =
				string.Format("~/App_Data/LocalizedTemplates/Sms/NotifyUserAfterAction.{0}.htm",
				oCultureInfo.Name);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			string strSmsBody =
				Dtx.IO.File.Read(strPathName);

			strSmsBody = strSmsBody
				.Replace("[FULL_NAME]", user.FullName)
				;

			return (Send(user.CellPhoneNumber, strSmsBody));
		}

		internal static bool NotifyUserAfterAction(Models.User user, string message)
		{
			// **************************************************
			if (user == null)
			{
				return (false);
			}

			if (user.IsCellPhoneNumberVerified == false)
			{
				return (false);
			}
			// **************************************************

			return (Send(user.CellPhoneNumber, message));
		}
	}
}
