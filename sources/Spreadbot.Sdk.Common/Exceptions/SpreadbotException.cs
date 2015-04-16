// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// SpreadbotException.cs

using System;

namespace Spreadbot.Sdk.Common.Exceptions
{
    public class SpreadbotException : Exception
    {
        protected SpreadbotException() {}

        public SpreadbotException( string template, params object[] args )
            : base( string.Format( template, args ) ) {}
    }
}