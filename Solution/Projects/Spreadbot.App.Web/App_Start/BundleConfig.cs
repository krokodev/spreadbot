using System.Web.Optimization;

namespace Spreadbot.App.Web
{
    public static class BundleConfig
    {
        // >> | App_Atart | BundleConfig
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS
            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/Themes/Default/Common.css")
                .Include("~/Content/Themes/Default/Form.css")
                );
        }
    }
}