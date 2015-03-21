﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// WebApiConfig.cs
// romak_000, 2015-03-21 2:10

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