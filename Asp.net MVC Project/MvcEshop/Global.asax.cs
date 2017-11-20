using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using GSD.Globalization;

namespace MvcEshop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "MyValidationMessages";

            DefaultModelBinder.ResourceClassKey = "MyValidationMessages";


            Infrastructure.Schedule.JobScheduler.Start();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //var persianCulture = new PersianCulture();
            //persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            //persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            //persianCulture.DateTimeFormat.AMDesignator = "ق.ظ";
            //persianCulture.DateTimeFormat.PMDesignator = "ب.ظ";
            //Thread.CurrentThread.CurrentCulture = persianCulture;
            //Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
    }
}
