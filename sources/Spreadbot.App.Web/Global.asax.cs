// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// Global.asax.cs
// Roman, 2015-04-09 10:10 AM

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

        // Code: Application_Error
        private void Application_Error( object sender, EventArgs e )
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            var exc = Server.GetLastError();

            // Handle HTTP errors
            if( exc.GetType() == typeof( HttpException ) ) {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if( exc.Message.Contains( "NoCatch" ) || exc.Message.Contains( "maxUrlLength" ) ) {
                    return;
                }

                //Redirect HTTP errors to HttpError page
                Server.Transfer( "HttpErrorPage.aspx" );
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write( "<h2>Global Page Error</h2>\n" );
            Response.Write(
                "<p>" + exc.Message + "</p>\n" );
            Response.Write( "Return to the <a href='Default.aspx'>" +
                "Default Page</a>\n" );

            // Log the exception and notify system operators
            ExceptionUtility.LogException( exc, "DefaultPage" );
            ExceptionUtility.NotifySystemOps( exc );

            // Clear the error from the server
            Server.ClearError();
        }
    }
}