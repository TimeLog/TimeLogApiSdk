using System.Web.Optimization;

namespace TimeLog.Api.Documentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
            "~/Scripts/plugins.js").Include("~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/Content/Styles/Compiled").Include(
                      "~/Content/Styles/*.css"));
        }
    }
}
