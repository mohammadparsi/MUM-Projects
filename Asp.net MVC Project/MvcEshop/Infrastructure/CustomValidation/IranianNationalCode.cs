using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.CustomValidation
{
    public class IranianNationalCode:System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string nationalCode = value.ToString();
            //در صورتی که کد ملی وارد شده تهی باشد

            //if (String.IsNullOrEmpty(nationalCode))
                //throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                //throw new Exception("طول کد ملی باید ده کاراکتر باشد");
                return false;

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new System.Text.RegularExpressions.Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                //throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");
                return false;

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = System.Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = System.Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = System.Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = System.Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = System.Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = System.Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = System.Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = System.Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = System.Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = System.Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }
        
    }
}