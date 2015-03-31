// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// SpreadbotException.cs
// Roman, 2015-03-31 1:27 PM

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