using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class MyDateTime
    {
        private static string[] formats = new string[]
    {
        "yyyy/MM/dd"   
    };

        public static DateTime ParseDate(string input)
        {
            return DateTime.ParseExact(input, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
        }
    }
}