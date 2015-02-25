using System.Web.Optimization;

namespace Spreadbot.App.Web
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/Themes/Default/Site.css"));
            // Default CSS
            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/Themes/Default/Site.css"));
        }
    }
}