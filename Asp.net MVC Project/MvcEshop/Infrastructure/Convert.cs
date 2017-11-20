using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class Convert
    {
        public static string ToDisplayDate(DateTime date)
        {
            return (date.Year.ToString() + "/" + date.Month.ToString() + "/" + date.Day.ToString());
        }

        public static string GregorianToPersian(DateTime dateTime)
        {
            System.Globalization.PersianCalendar oPersianCalender =
                new System.Globalization.PersianCalendar();

            string strPersianDateTime = string.Format
                          ("{0}/{1}/{2} - {3}:{4}:{5} ",
                          oPersianCalender.GetYear(dateTime),
                          oPersianCalender.GetMonth(dateTime),
                          oPersianCalender.GetDayOfMonth(dateTime),
                          oPersianCalender.GetHour(dateTime),
                          oPersianCalender.GetMinute(dateTime),
                          oPersianCalender.GetSecond(dateTime));

            return strPersianDateTime;
        }
        public static DateTime ToMiladi(DateTime date)
        {
            System.Globalization.PersianCalendar oPersianCalendar =
                new System.Globalization.PersianCalendar();

            DateTime oMiladiDate = new DateTime(date.Year, date.Month, date.Day, oPersianCalendar);

            return oMiladiDate;
        }

        public static DateTime ToPersian(DateTime date)
        {
            System.Globalization.Calendar oPersianCalendar =
                new System.Globalization.PersianCalendar();

            int intYear = oPersianCalendar.GetYear(date);
            int intMonth = oPersianCalendar.GetMonth(date);
            int intDay = oPersianCalendar.GetDayOfMonth(date);
            int intHour = oPersianCalendar.GetHour(date);
            int intMinute = oPersianCalendar.GetMinute(date);
            int intSecond = oPersianCalendar.GetSecond(date);

            DateTime oPersianDate = new DateTime(intYear,intMonth,intDay,intHour,intMinute,intSecond);

            return oPersianDate;

        }


    }
}