using System.Web;
using System.Web.Optimization;

namespace ProductProvider
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/farbtastic").Include(
                      "~/Scripts/farbtastic.js"));




            bundles.Add(new ScriptBundle("~/bundles/colorpicker").Include(
          "~/Scripts/jquery.js",
                "~/Scripts/colorpicker.js",
          "~/Scripts/eye.js",
          "~/Scripts/layout.js",
          "~/Scripts/utils.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/farbtastic.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include("~/Content/bootstrap.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/colorpicker").Include(
                      "~/Content/colorpicker.css",
                      "~/Content/layout.css"));
        }
    }
}
