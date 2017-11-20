namespace Infrastructure
{
	public static class UserNotificationService
	{
		public static void SendCellPhoneNumberVerificationKey(Models.User user)
		{
			Sms.SendCellPhoneNumberVerificationKey(user);
		}

		public static void SendAgainEmailAddressVerificationKey(Models.User user)
		{
			Email.SendAgainEmailAddressVerificationKey(user);
		}

		public static void SendEmailAddressVerificationKey(Models.User user, string password)
		{
			Email.SendEmailAddressVerificationKey(user, password);
		}

		public static void SendNewPassword(Models.User user, string newPassword)
		{
			if (user.IsCellPhoneNumberVerified)
			{
				Sms.SendNewPassword(user, newPassword);
			}

			Email.SendNewPassword(user, newPassword);
		}
	}
}
