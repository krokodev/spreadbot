// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// LogController.cs
// Roman, 2015-04-09 5:06 PM

using System;
using System.IO;
using System.Web.Mvc;
using Crocodev.Common.Extensions;
using NLog;
using NLog.Targets;

namespace Spreadbot.App.Web.Controllers
{
    // Here: Controller | LogController
    public class LogController : Controller
    {
        public ActionResult Trace()
        {
            var logName = NLogFileName( "TraceFile" );
            ViewBag.Log = GetLogPlainText( "Logs", logName );
            ViewBag.Title = logName;
            return View();
        }
        
        public ActionResult Error()
        {
            var logName = NLogFileName( "ErrorFile" );
            ViewBag.Log = GetLogPlainText( "Logs", logName );
            ViewBag.Title = logName;
            return View();
        }

        private static string GetLogPlainText( string filePath, string fileName )
        {
            try {
                var path = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "App_Data", filePath );
                return System.IO.File.ReadAllText( path + @"\" + fileName );
            }
            catch( Exception ) {
                return @"Can't read log [{0}\{1}]".SafeFormat( filePath, fileName );
            }
        }

        private static string NLogFileName(string name)
        {
            var fileTarget = ( FileTarget ) LogManager.Configuration.FindTargetByName( name );

            if( fileTarget == null ) {
                return null;
            }
            var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
            var fileName = fileTarget.FileName.Render( logEventInfo );
            return Path.GetFileName( fileName );
        }
    }
}