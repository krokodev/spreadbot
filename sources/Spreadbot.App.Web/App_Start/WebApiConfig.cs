﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// WebApiConfig.cs
// Roman, 2015-03-31 1:25 PM

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