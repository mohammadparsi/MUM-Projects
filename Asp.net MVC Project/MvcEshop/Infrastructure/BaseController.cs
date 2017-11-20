using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class BaseController:System.Web.Mvc.Controller
    {
        public BaseController()
        {
            //System.Globalization.CultureInfo oCultureInfo =
            //    new System.Globalization.CultureInfo("en-US");

            System.Globalization.CultureInfo oCultureInfo =
                new System.Globalization.CultureInfo("fa-IR");

            //CurrentCulture affects on the format of numbers, dates etc but CurrentUICulture builds the UI based on defined resource files.
            //as we had a problem with double numbers in "fa-IR" culture and .Net made them display with "/" instead of ".", we decided to
            //comment the following line.
            //System.Threading.Thread.CurrentThread.CurrentCulture = oCultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = oCultureInfo;

        }
    }
}