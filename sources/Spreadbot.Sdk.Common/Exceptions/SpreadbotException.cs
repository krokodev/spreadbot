// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// SpreadbotException.cs
// romak_000, 2015-03-26 19:42

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