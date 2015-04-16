// Spreadbot (c) 2015 Krokodev
// Spreadbot.App.Web
// WebApiConfig.cs

using System.Web.Http;

namespace Spreadbot.App.Web
{
    public static class WebApiConfig
    {
        public static void Register( HttpConfiguration config )
        {
            config.Routes.MapHttpRoute(
                name : "DefaultApi",
                routeTemplate : "api/{controller}/{id}",
                defaults : new { id = RouteParameter.Optional }
                );
        }
    }
}