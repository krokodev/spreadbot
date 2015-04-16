// Spreadbot (c) 2015 Krokodev
// Spreadbot.App.Web
// BundleConfig.cs

using System.Web.Optimization;

namespace Spreadbot.App.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles( BundleCollection bundles )
        {
            // CSS
            bundles.Add(
                new StyleBundle( "~/css" )
                    .Include( "~/Content/Common.css" )
                    .Include( "~/Content/Tasks.css" )
                );
        }
    }
}