// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// Class1.cs
// Roman, 2015-04-09 10:35 AM

using System;
using System.IO;
using System.Web;

// Code: ExceptionUtility

namespace Spreadbot.App.Web.Utils
{
    public static class ExceptionUtility
    {
        public static void LogException( Exception exc, string source )
        {
            var logFile = "App_Data/Logs/ExceptionUtility.txt";
            logFile = HttpContext.Current.Server.MapPath( logFile );

            using( var sw = new StreamWriter( logFile, true ) ) {
                WriteHeader( sw );
                if( exc.InnerException != null ) {
                    sw.Write( "Inner Exception Type: " );
                    sw.WriteLine( exc.InnerException.GetType().ToString() );
                    sw.Write( "Inner Exception: " );
                    sw.WriteLine( exc.InnerException.Message );
                    sw.Write( "Inner Source: " );
                    sw.WriteLine( exc.InnerException.Source );
                    if( exc.InnerException.StackTrace != null ) {
                        sw.WriteLine( "Inner Stack Trace: " );
                        sw.WriteLine( exc.InnerException.StackTrace );
                    }
                }
                sw.Write( "Exception Type: " );
                sw.WriteLine( exc.GetType().ToString() );
                sw.WriteLine( "Exception: " + exc.Message );
                sw.WriteLine( "Source: " + source );
                sw.WriteLine( "Stack Trace: " );
                if( exc.StackTrace != null ) {
                    sw.WriteLine( exc.StackTrace );
                    sw.WriteLine();
                }
            }
        }

        private static void WriteHeader( StreamWriter sw )
        {
            sw.WriteLine( "********** {0} **********", DateTime.Now );
        }
    }
}