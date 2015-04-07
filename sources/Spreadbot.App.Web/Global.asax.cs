// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// Global.asax.cs
// Roman, 2015-04-07 2:57 PM

using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Spreadbot.App.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            BeginRequest += Application_BeginRequest;

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register( GlobalConfiguration.Configuration );
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            BundleConfig.RegisterBundles( BundleTable.Bundles );
        }

        private void Application_BeginRequest( object sender, EventArgs e )
        {
            try {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            }
            catch {
                // ignored
            }
        }
    }
}