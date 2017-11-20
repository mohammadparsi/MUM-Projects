using System.Web;
using System.Web.Optimization;

namespace MvcEshop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery*"));

            //add jquery files for datepicker
            //bundles.Add(new ScriptBundle("~/bundles/jqueryForDatePicker").Include(
                        
            //            "~/Scripts/DatePicker/jquery-1.6.2.min.js",
            //            "~/Scripts/DatePicker/jquery.ui.datepicker-cc.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-rtl").Include(
                      "~/Scripts/bootstrap-rtl.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/modal.js",
                      "~/Scripts/button.js",
                      "~/Scripts/carousel.js",
                      "~/Scripts/navbar-primary.js",
                      "~/Scripts/tooltip.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-rtl.css",
                      "~/Content/site.css",
                      "~/Content/CustomizedBootStrap.css",
                      "~/Content/ReferencesStyles.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
