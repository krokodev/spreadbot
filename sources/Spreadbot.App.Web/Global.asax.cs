// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// Global.asax.cs
// Roman, 2015-04-10 1:28 PM

using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

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

        protected void Application_BeginRequest( object sender, EventArgs e )
        {
            try {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            }
            catch {
                // ignored
            }
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Error( object sender, EventArgs e )
        {
            var exc = Server.GetLastError();

            if( exc.GetType() == typeof( HttpException ) ) {
                if( exc.Message.Contains( "NoCatch" ) || exc.Message.Contains( "maxUrlLength" ) ) {
                    return;
                }
                Server.Transfer( "~/Http/ErrorPage.aspx" );
            }

            Response.Write( "<h2>SB: Global Page Error</h2>\n" );
            Response.Write( "<p>" + exc.Message + "</p>\n" );
            Response.Write( "<p><pre>" + exc.StackTrace + "</pre></p>\n" );

            Logger.ErrorException( exc.Message, exc );

            Server.ClearError();
        }
    }
}