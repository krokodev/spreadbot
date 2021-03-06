﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// ExceptionUtility.cs

using System;
using Krokodev.Common.Extensions;
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