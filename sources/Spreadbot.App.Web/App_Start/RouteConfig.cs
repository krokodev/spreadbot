// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// RouteConfig.cs

using System.Web.Mvc;
using System.Web.Routing;

namespace Spreadbot.App.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.MapRoute
                (
                    "Main",
                    "{controller}/{action}/{id}",
                    new {
                        controller = "Page",
                        action = "Index",
                        id = UrlParameter.Optional
                    },
                    new[] {
                        "Spreadbot.App.Web"
                    }
                );
        }
    }
}