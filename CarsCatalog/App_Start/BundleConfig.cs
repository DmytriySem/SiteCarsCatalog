using System.Web;
using System.Web.Optimization;

namespace CarsCatalog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/view-dialog").Include(
                        "~/Scripts/Scripts/view-dialog.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-grid").Include(
                        "~/Scripts/jquery.jqGrid.min.js",
                         "~/Scripts/i18n/grid.locale-en.js",
                        "~/Scripts/Scripts/Grid.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsTree").Include(
                "~/Scripts/jsTree3/jstree.js",
                "~/Scripts/Scripts/Tile.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/themes/jqui/jquery-ui-1.12.1.custom/jquery-ui.min.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css"));

            bundles.Add(new StyleBundle("~/Content/jsTree").Include(
               "~/Content/jsTree/docs.css",
               "~/Content/jsTree/themes/default-dark/style.css"
               ));
        }
    }
}
