// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// RouteConfig.cs
// romak_000, 2015-03-26 19:41

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
                    "Default",
                    "{controller}/{action}/{id}",
                    new {
                        controller = "Home",
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