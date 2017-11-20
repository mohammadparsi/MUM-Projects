using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpecialAttributeResources
{
    public static class Errors
    {
        public const string EmailIsNotUnique = 
            "آدرس ایمیل، قبلا توسط کاربر دیگری در سیستم ثبت شده و تکراری می باشد.";

        public const string EmailIsNotValid = "آدرس ایمیل، معتبر نمی باشد.";
        public const string PasswordPolicy_aA1__8_20 = "رمز عبور باید دارای حداقل یک حرف بزرگ انگلیسی،  حداقل یک حرف کوچک انگلیسی و حداقل یک عدد بوده و طول آن حداقل 8 کاراکتر و حد اکثر 20 کاراکتر باشد.";
    }

}