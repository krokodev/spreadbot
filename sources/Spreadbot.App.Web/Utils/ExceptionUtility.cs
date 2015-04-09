// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// ExceptionUtility.cs
// Roman, 2015-04-09 4:53 PM

using System;
using NLog;

namespace Spreadbot.App.Web.Utils
{
    public static class ExceptionUtility
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogException( Exception exc, string source )
        {
            Logger.ErrorException( source, exc );
        }
    }
}