// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// BundleConfig.cs
// Roman, 2015-04-03 8:15 PM

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
                );
        }
    }
}