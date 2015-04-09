// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// ExceptionUtility.cs
// Roman, 2015-04-09 4:53 PM

using System;
using Crocodev.Common.Extensions;
using NLog;

namespace Spreadbot.App.Web.Utils
{
    public static class ExceptionUtility
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogException( Exception exc, string source )
        {
            Logger.ErrorException( "{0}: {1}".SafeFormat( source, exc.Message), exc );
        }
    }
}