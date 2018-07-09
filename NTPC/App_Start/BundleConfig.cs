using System.Web;
using System.Web.Optimization;

namespace NTPC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery.js",
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/metisMenu.js",
                       "~/Scripts/sb-admin-2.js",
                       "~/Scripts/jquery.validate*",
                       "~/Scripts/jquery.dataTables.min.js",
                       "~/Scripts/dataTables.bootstrap.min.js",
                       "~/Scripts/dataTables.jqueryui.min.js",
                       "~/Scripts/dataTables.responsive.js",
                       "~/Scripts/jquery.datetimepicker.js",
                       "~/Scripts/modernizr-*",
                       "~/Scripts/respond.js",
                       "~/Scripts/morris.min.js",
                       "~/Scripts/jquery-ui-1.12.1.js",
                       "~/Scripts/raphael.min.js",
                       "~/Scripts/DataTables/dataTables.jqueryui.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/css/bootstrap.css",
                       "~/Content/site.css",
                       "~/Content/css/dataTables.bootstrap.css",
                       "~/Content/css/dataTables.jqueryui.css",
                       "~/Content/css/dataTables.responsive.css",
                       "~/Content/css/font-awesome.css",
                       "~/Content/css/jquery.dataTables.css",
                       "~/Content/jquery.datetimepicker.css",
                       "~/Content/css/metisMenu.css",
                       "~/Content/css/sb-admin-2.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js"
                ));
        }
    }
}
