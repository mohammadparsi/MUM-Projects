using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure
{
    public class CustomHandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {

        public CustomHandleErrorAttribute()
        {

        }
        public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            string strRootRelativePathName =
                "~/App_Data/Logs/Application.log";

            string strPathName =
                System.Web.HttpContext.Current
                .Server.MapPath(strRootRelativePathName);

            string strErrorMessage =
                filterContext.Exception.Message;

            System.Web.HttpContext.Current.Application.Lock();

            System.IO.StreamWriter oStream = null;

            try
            {
                oStream =
                    new System.IO.StreamWriter
                        (strPathName, true, System.Text.Encoding.UTF8);

                oStream.WriteLine(strErrorMessage);
            }
            catch
            {
            }
            finally
            {
                if (oStream != null)
                {
                    //oStream.Close();
                    oStream.Dispose();
                    oStream = null;
                }
            }

            System.Web.HttpContext.Current.Application.UnLock();
        }
    }
}