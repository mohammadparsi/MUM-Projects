using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialAttributeResources
{
    public static class DisplayName
    {
        public const string Account = "حساب کاربری";
        public const string Accounts = "حساب های کاربری";
        public const string AccountTitle = Words.Title + " " + Account;
        public const string Password = "رمز عبور";
        public const string JoinPassword = Password + " " + Account;
        public const string Email = "ایمیل";
        public const string AccountManagerEmail = Email + " " + Words.Manager + " " + Account;
        public const string Active = "فعال";
        public const string Deleted = "حذف شده";
        public const string FirstName = "نام";
        public const string LastName = "نام خانوادگی";
        public const string ConfirmPassword = "تکرار رمز عبور";
        public const string Gender = "جنسیت";

    }
}
