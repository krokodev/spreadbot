// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// LogController.cs
// Roman, 2015-04-09 1:52 PM

using System;
using System.IO;
using System.Web.Mvc;
using Crocodev.Common.Extensions;

namespace Spreadbot.App.Web.Controllers
{
    // Here: Controller | LogController
    public class LogController : Controller
    {
        public ActionResult ErrorLog()
        {
            ViewBag.Log = GetLogPlainText( "Logs", "ExceptionUtility.txt" );
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
    }
}