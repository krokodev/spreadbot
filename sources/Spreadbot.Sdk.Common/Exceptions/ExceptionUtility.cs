// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ExceptionUtility.cs
// Roman, 2015-04-10 1:28 PM

using System;
using Crocodev.Common.Extensions;
using NLog;

namespace Spreadbot.Sdk.Common.Exceptions
{
    public static class ExceptionUtility
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogException( Exception exc, string source )
        {
            Logger.ErrorException( "{0}: {1}".SafeFormat( source, exc.Message ), exc );
        }
    }
}